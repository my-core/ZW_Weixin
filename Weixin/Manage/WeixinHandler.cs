using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using Weixin.MP.Helpers;
using Weixin.MP;
using Weixin.MP.MessageHandlers;
using Weixin.MP.Entities;
using Weixin.MP.Context;
namespace Weixin.Manage
{
    public class WeixinHandler : MessageHandler<MessageContext>
    {
        /// <summary>
        /// TOKEN
        /// </summary>
        private const string TOKEN = "mutouyatou";
        /// <summary>
        /// 签名
        /// </summary>
        private const string SIGNATURE = "signature";
        /// <summary>
        /// 时间戳
        /// </summary>
        private const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 随机数
        /// </summary>
        private const string NONCE = "nonce";
        /// <summary>
        /// 返回响应的结果
        /// </summary>
        public string ResponseContent = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        public WeixinHandler(Stream inputStream, int maxRecordCount)
            : base(inputStream, maxRecordCount)
        {

        }

        /// <summary>
        /// 处理中
        /// </summary>
        public override void OnExecuting()
        {
        }

        /// <summary>
        /// 处理结束事件
        /// </summary>
        public override void OnExecuted()
        {
            switch (ResponseMessage.MsgType)
            {
                case ResponseMsgType.Text:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageText>(ResponseMessage as ResponseMessageText);
                    break;
                case ResponseMsgType.News:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageNews>(ResponseMessage as ResponseMessageNews);
                    break;
                case ResponseMsgType.Image:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageImage>(ResponseMessage as ResponseMessageImage);
                    break;
                case ResponseMsgType.Voice:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageVoice>(ResponseMessage as ResponseMessageVoice);
                    break;
                case ResponseMsgType.Video:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageVideo>(ResponseMessage as ResponseMessageVideo);
                    break;
                case ResponseMsgType.Music:
                    ResponseContent = EntityHelper.ConvertEntityToXmlString<ResponseMessageMusic>(ResponseMessage as ResponseMessageMusic);
                    break;
                default:
                    ResponseContent = "";
                    break;
            }
        }

        /// <summary>
        /// 检查签名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool AuthSignature(HttpContext hc)
        {
            string signature = hc.Request.QueryString[SIGNATURE];
            string timestamp = hc.Request.QueryString[TIMESTAMP];
            string nonce = hc.Request.QueryString[NONCE];
            string new_signature = CheckSignature.GetSignature(timestamp, nonce, TOKEN);
            //验证
            if (new_signature == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 默认消息处理
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //例如可以这样实现：
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送的消息类型暂未被识别。";
            return responseMessage;
        }
        /// <summary>
        /// 文字类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            switch (requestMessage.Content.Trim())
            {
                case "0":
                    ResponseMessageText text = new ResponseMessageText();
                    text.Content = "文本消息";
                    text.FromUserName = requestMessage.ToUserName;
                    text.ToUserName = WeixinOpenId;
                    text.FuncFlag = false;
                    text.CreateTime = DateTime.Now;
                    return text;
                case "1":
                    ResponseMessageNews news = new ResponseMessageNews();

                    List<Article> list = new List<Article>();
                    Article art = new Article();
                    art.Title = "第57届世界小姐";
                    art.Description = "第57届世界小姐总决赛上，各国佳丽在争妍斗艳，最终中国小姐张梓琳脱颖而出，荣获世界小姐桂冠。这也是中国佳丽在世界级选美大赛上首次摘得桂冠。";
                    art.PicUrl = "http://ylb.chinacloudsites.cn/img/t1.jpg";
                    art.Url = "www.baidu.com";
                    list.Add(art);

                    art.Title = "第58届世界小姐";
                    art.Description = "第57届世界小姐总决赛上，各国佳丽在争妍斗艳，最终中国小姐张梓琳脱颖而出，荣获世界小姐桂冠。这也是中国佳丽在世界级选美大赛上首次摘得桂冠。";
                    art.PicUrl = "http://ylb.chinacloudsites.cn/img/t2.jpg";
                    art.Url = "www.google.com";
                    list.Add(art);

                    news.Articles = list;
                    news.ArticleCount = list.Count;
                    news.FromUserName = requestMessage.ToUserName;
                    news.ToUserName = WeixinOpenId;
                    news.FuncFlag = false;
                    news.CreateTime = DateTime.Now;
                    return news;
                default:
                    return null;
            }
           
        }

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            ResponseMessageText text = new ResponseMessageText();
            text.Content = "地理位置纬度:" + requestMessage.Location_X + "，地理位置经度:" + requestMessage.Location_Y + "，地理位置信息：" + requestMessage.Label;
            text.FromUserName = requestMessage.ToUserName;
            text.ToUserName = WeixinOpenId;
            text.FuncFlag = false;
            text.CreateTime = DateTime.Now;
            return text;
        }

        #region
        ///// <summary>
        ///// 图片类型请求
        ///// </summary>
        //public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// 语音类型请求
        ///// </summary>
        //public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// 视频类型请求
        ///// </summary>
        //public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// 链接消息类型请求
        ///// </summary>
        //public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}
        #endregion

        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public override IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.ENTER:
                    responseMessage = OnEvent_EnterRequest(RequestMessage as RequestMessageEvent_Enter);
                    break;
                case Event.LOCATION://自动发送的用户当前位置
                    responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.subscribe://订阅
                    responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://退订
                    responseMessage = OnEvent_UnsubscribeRequest(RequestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK://菜单点击
                    responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.scan://二维码
                    responseMessage = OnEvent_ScanRequest(RequestMessage as RequestMessageEvent_Scan);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类

        ///// <summary>
        ///// Event事件类型请求之ENTER
        ///// </summary>
        //public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// Event事件类型请求之LOCATION
        ///// </summary>
        //public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        /// <summary>
        /// Event事件类型请求之subscribe
        /// </summary>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            ResponseMessageNews news = new ResponseMessageNews();

            List<Article> list = new List<Article>();
            Article art = new Article();
            art.Title = "湛网工作室";
            art.Description = "QQ:470868331-Tel:13632809657";
            art.PicUrl = "http://ylb.chinacloudsites.cn/img/b1.gif";
            art.Url = "";
            list.Add(art);

            news.Articles = list;
            news.ArticleCount = list.Count;
            news.FromUserName = requestMessage.ToUserName;
            news.ToUserName = WeixinOpenId;
            news.FuncFlag = false;
            news.CreateTime = DateTime.Now;
            return news;
        }

        ///// <summary>
        ///// Event事件类型请求之unsubscribe
        ///// </summary>
        //public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// Event事件类型请求之CLICK
        ///// </summary>
        //public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}

        ///// <summary>
        ///// Event事件类型请求之scan
        ///// </summary>
        //public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        //{
        //    return DefaultResponseMessage(requestMessage);
        //}
        #endregion
    }
}

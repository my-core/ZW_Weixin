using System;
using System.Collections.Generic;
using System.Web;
using Weixin.MP;

namespace Weixin.Manage
{
    public class WeiXinHttpHandler :  IHttpHandler 
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        private const string ECHOSTR = "echostr"; 
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext hc)
        {
            string responseStr = "";

            WeixinHandler handler = new WeixinHandler(hc.Request.InputStream, 0);
            
            //由微信服务接收请求，具体处理请求
            string method = hc.Request.HttpMethod.ToUpper();
            //验证签名
            if (method == "GET")
            {
                if (handler.AuthSignature(hc))
                {
                    responseStr= hc.Request.QueryString[ECHOSTR];
                }
                else
                {
                    responseStr = "error";
                }
            }

            //处理用户消息请求
            if (method == "POST")
            {
                handler.Execute();
                handler.OnExecuted();
                responseStr= handler.ResponseContent;
            }
            else
            {
                responseStr = "无法处理";
            }
            hc.Response.Clear();
            hc.Response.Charset = "UTF-8";
            hc.Response.Write(responseStr);
            hc.Response.End();
        }        

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
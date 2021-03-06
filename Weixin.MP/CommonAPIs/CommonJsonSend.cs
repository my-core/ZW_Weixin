using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Weixin.MP.CommonAPIs;
using Weixin.MP.Entities;
using Weixin.MP.Helpers;
using Weixin.MP.HttpUtility;

namespace Weixin.MP.CommonAPIs
{
    public enum CommonJsonSendType
    {
        GET,
        POST
    }

    public static class CommonJsonSend
    {
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <returns></returns>
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType)
        {
            return Send<WxJsonResult>(accessToken, urlFormat, data, sendType);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType)
        {

            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
            switch (sendType)
            {
                case CommonJsonSendType.GET:
                    return Get.GetJson<T>(url, null);
                case CommonJsonSendType.POST:
                    SerializerHelper serializerHelper = new SerializerHelper();
                    var jsonString = serializerHelper.GetJsonString(data);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(jsonString);
                        ms.Write(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        return Post.PostGetJson<T>(url, null, ms, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException("sendType");
            }
        }
    }
}


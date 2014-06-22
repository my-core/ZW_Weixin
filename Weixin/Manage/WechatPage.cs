using System;
using System.Collections.Generic;
using System.Web;
using Weixin.Model;
using Weixin.Common;

namespace Weixin.Web.Manage
{
    public class WechatPage : AdminPage
    {
        /// <summary>
        /// 当前管理的微信号信息
        /// </summary>
        public WechatInfo WechatInfo
        {
            get
            {
                string str = CookieHelper.GetCookie("WechatInfo");
                if (str == string.Empty)
                {
                    return null;
                }
                CookieHelper.UpdateCookie("WechatInfo", DateTime.Now.AddHours(2));
                str = DESEncrypt.Decrypt(str);
                return (WechatInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(WechatInfo));
            }
            set
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                CookieHelper.SetCookie("WechatInfo", json, DateTime.Now.AddHours(2));
            }
        }
        protected override void OnInit(EventArgs e)
        {
            if (AdminInfo == null)
            {
                RegistScript("window.parent.location.href='/Manage/index.aspx';");
            }
            base.OnInit(e);
        }
    }
}

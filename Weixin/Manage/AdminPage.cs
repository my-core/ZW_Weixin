using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weixin.Model;
using Weixin.Common;

namespace Weixin.Web.Manage
{
    public class AdminPage : BasePage
    {
        /// <summary>
        /// 当前登录管理员
        /// </summary>
        public AdminInfo AdminInfo
        {
            get
            {
                string str = CookieHelper.GetCookie("AdminInfo");
                if (str == string.Empty)
                {
                    return null;
                }
                CookieHelper.UpdateCookie("AdminInfo", DateTime.Now.AddHours(2));
                str = DESEncrypt.Decrypt(str);
                return (AdminInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(AdminInfo));
            }
            set
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                json = DESEncrypt.Encrypt(json);
                CookieHelper.SetCookie("AdminInfo", json, DateTime.Now.AddHours(2));
            }
        }
        /// <summary>
        /// 后台Title
        /// </summary>
        public string AdminTitle
        {
            get
            {
                return Utils.GetConfig("AdminTitle");
            }
        }
        protected override void OnInit(EventArgs e)
        {
            RegisterIncScriptBlock();
            IsLogin();
            base.OnInit(e);
        }
        /// <summary>
        /// 判断是否登录）
        /// </summary>
        public void IsLogin()
        {
            if (Request.Url.ToString().ToLower().IndexOf("login.aspx") == -1)
            {
                if (AdminInfo == null)
                {
                    RegistScript("alert('未登录或登录已失效！');window.parent.location.href='/Manage/Login.aspx';");
                }
            }
            return;
        }
        /// <summary>
        /// 注册INC脚本块
        /// </summary>
        public void RegisterIncScriptBlock()
        {
            this.Header.Controls.AddAt(1, RegistCSS("/Manage/Style/easyui/themes/default/easyui.css"));
            this.Header.Controls.AddAt(2, RegistCSS("/Manage/Style/easyui/themes/icon.css"));
            this.Header.Controls.AddAt(3, RegistCSS("/Manage/Style/admin.css"));

            this.Header.Controls.AddAt(4, RegistJavaScript("/Manage/Style/js/jquery-1.11.1.min.js"));
            this.Header.Controls.AddAt(5, RegistJavaScript("/Manage/Style/easyui/jquery.easyui.min.js"));
            this.Header.Controls.AddAt(6, RegistJavaScript("/Manage/Style/js/customValidate.js"));
            this.Header.Controls.AddAt(7, RegistJavaScript("/Manage/Style/js/common.js"));
            this.Header.Controls.AddAt(8, RegistJavaScript("/Manage/Style/js/msgbox.js"));
        }
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void AlertInfo(string message)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alertInfo('" + message + "')</script>");
        }
        /// <summary>
        /// 输出JavaScript
        /// </summary>
        /// <param name="strScript"></param>
        public void RegistScript(string strScript)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>" + strScript + "</script>");
        }
        
    }
}

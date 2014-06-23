﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Weixin.Model;
using Weixin.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weixin.Service;

namespace Weixin.Web.Manage
{
    public partial class Login : AdminPage
    {
        //注入
        private ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
        }
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 登录
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text;
            string password = this.txtPassword.Text;
            if (username == "" || password == "")
            {
                this.lblMsg.Text = "账号/密码不能为空！";
                return;
            }
            if (this.txtUserName.Text == "" || this.txtPassword.Text == "")
            {
                this.lblMsg.Text="账号/密码不能为空！";
                return;
            }
            int res = sysService.AdminLogin(this.txtUserName.Text, this.txtPassword.Text);
            if (res == 1)
            {
                this.lblMsg.Text = "账号不存在！";
                this.txtUserName.Text = "";
                this.txtPassword.Text = "";
                this.txtUserName.Focus();
                return;
            }
            if (res == 2)
            {
                this.lblMsg.Text = "账号已禁用，请与管理员联系！";
                return;
            }
            if (res == 3)
            {
                this.lblMsg.Text = "密码不正确，请重新输入！";
                this.txtPassword.Text = "";
                this.txtPassword.Focus();
                return;
            }
            SetLoginInfo(username);
            Response.Redirect("Index.aspx");
        }
        /// <summary>
        /// 设置登录状态及登录信息
        /// </summary>
        /// <param name="username"></param>
        public void SetLoginInfo(string username)
        {
            DataTable dt = sysService.GetDataByKey("T_Admin", "UserName", username);
            DataRow dr = dt.Rows[0];
            AdminInfo minfo = new AdminInfo();
            minfo.ID = Convert.ToInt32(dr["ID"]);
            minfo.UserName = Convert.ToString(dr["UserName"]);
            minfo.AdminName = Convert.ToString(dr["AdminName"]);
            minfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            if (string.IsNullOrEmpty(dr["MerchantID"].ToString()))
            {
                minfo.MerchantID = null;
            }
            else
            {
                minfo.MerchantID = Convert.ToInt32(dr["MerchantID"]);
            }
            AdminInfo = minfo;
        }
    }
}
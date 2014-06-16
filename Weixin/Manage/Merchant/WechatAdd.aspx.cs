using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weixin.Service;
using System.Data;
using Weixin.Model;
using Weixin.Common;

namespace Weixin.Web.Manage.Merchant
{
    public partial class WechatAdd : AdminPage
    {
        #region IOC注入
        private static IMerchantService merchantService;
        public IMerchantService MerchantService
        {
            set { merchantService = value; }
            get { return merchantService; }
        }
        #endregion

        /// <summary>
        /// 微信号ID
        /// </summary>
        public int _ID
        {
            get { return GetRequestInt("ID", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定信息
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = merchantService.GetDataByKey("T_Wechat","ID",_ID);
            DataRow dr = dt.Rows[0];
            this.txtName.Text = dr["Name"].ToString();
            this.txtWechatNo.Text = dr["WechatNo"].ToString();
            
        }
        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = merchantService.GetDataByKey("T_Wechat", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                dr["Name"] = this.txtName.Text;
                dr["UpdateBy"] = AdminInfo.ID;
                dr["UpdateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["Password"] = this.txtPassword.Text;
                dr["CreateBy"] = AdminInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            dr["Type"] = this.rblType.SelectedValue;
            dr["WechatNo"] = this.txtWechatNo.Text;
            int res = merchantService.SaveWechatInfo(dt);
            if (res > 0)
                InvokeScript("CloseWin('保存成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('保存失败！')");
        }
    }
}

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
            DataTable dt = merchantService.GetDataByKey("T_WX_WeChat", "ID", _ID);
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
            //微信号：gh_424afd1ed04b
            //appID：wx3fed1c00ef33967f
            //appsecret：fae2442723ffbf60374ec1f588a8b666

            DataTable dt = merchantService.GetDataByKey("T_WX_WeChat", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                dr["UpdateBy"] = AdminInfo.ID;
                dr["UpdateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["AppID"] = this.txtAppID.Text;
                dr["AppSecret"] = this.txtAppSecret.Text;
                string token = Utils.GetRandom(8);
                dr["Token"] = token;
                dr["ApiUrl"] = Utils.GetConfig("BaseApiUrl") + DESEncrypt.MD5(token);
                dr["CreateBy"] = AdminInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            dr["Name"] = this.txtName.Text;
            dr["Type"] = this.rblType.SelectedValue;
            dr["WechatNo"] = this.txtWechatNo.Text;
            if (!string.IsNullOrEmpty(AdminInfo.MerchantID.ToString()))
            {
                dr["MerchantID"] = AdminInfo.MerchantID;
            }
            int res = merchantService.SaveWechatInfo(dt);
            if (res == 0)
                InvokeScript("CloseWin('保存成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('保存失败！')");
        }
    }
}

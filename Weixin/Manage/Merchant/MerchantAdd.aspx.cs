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
    public partial class MerchantAdd : AdminPage
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
        /// 商户ID
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
            DataTable dt = merchantService.GetDataByKey("T_Merchant","ID",_ID);
            DataRow dr = dt.Rows[0];
            this.txtPassword.Attributes.Remove("data-options");
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.txtAccount.Text = Convert.ToString(dr["Account"]);
            this.txtAccount.Enabled = false;
            this.txtMobileNo.Text = Convert.ToString(dr["MobileNo"]);
            this.txtQQ.Text = Convert.ToString(dr["QQ"]);
            this.txtEmail.Text = Convert.ToString(dr["Email"]);
            this.txtAddress.Text = Convert.ToString(dr["Address"]);
            this.cbStatus.Checked = (Convert.ToInt16(dr["Status"]) == (int)EnumStatus.Disabled) ? true : false;
        }
        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = merchantService.GetDataByKey("T_Merchant", "ID", _ID);
            DataTable dtAdmin = merchantService.GetDataByKey("T_Admin", "UserName", this.txtAccount.Text);
            DataRow dr;
            DataRow drA;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                dr["UpdateBy"] = AdminInfo.ID;
                dr["UpdateTime"] = DateTime.Now;

                drA = dtAdmin.Rows[0];
                drA["UpdateBy"] = AdminInfo.ID;
                drA["UpdateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateBy"] = AdminInfo.ID;
                dr["CreateTime"] = DateTime.Now;

                drA = dtAdmin.NewRow();
                dtAdmin.Rows.Add(drA);
                drA = dtAdmin.Rows[0];
                dr["CreateBy"] = AdminInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            dr["Name"] = this.txtName.Text;
            dr["MobileNo"] = this.txtMobileNo.Text;
            dr["QQ"] = this.txtQQ.Text;
            dr["Email"] = this.txtEmail.Text;
            dr["Address"] = this.txtAddress.Text;
            dr["Status"] = this.cbStatus.Checked ? EnumStatus.Disabled : EnumStatus.Normal;

            drA["UserName"] = this.txtAccount.Text;
            if (this.txtPassword.Text != "")
                drA["Password"] = DESEncrypt.Encrypt(this.txtPassword.Text);
            drA["AdminName"] = this.txtName.Text;
            drA["IsMain"] = true;

            int res = merchantService.SaveMerchantInfo(dt, dtAdmin);
            if (res > 0)
                InvokeScript("CloseWin('保存成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('保存失败！')");
        }
    }
}

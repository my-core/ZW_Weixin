using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weixin.Service;
using Weixin.Common;
using System.Collections.Generic;
using Weixin.Model;

namespace Weixin.Web.Manage.Admin
{
    public partial class AdminAdd : AdminPage
    {
        //注入
        private ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
        }
        private int _ID
        {
            get { return GetRequestInt("ID", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGroup();
                if(_ID!=0)
                    BindInfo();
            }
        }
        public void BindInfo()
        {
            DataTable dt = sysService.GetDataByKey("T_Admin", "ID", _ID);
            DataRow dr=dt.Rows[0];
            this.txtUserName.Text = Convert.ToString(dr["UserName"]);
            this.txtPassword.Attributes.Remove("data-options");
            this.txtAdminName.Text = Convert.ToString(dr["AdminName"]);
            this.cbStatus.Checked = (Convert.ToInt16(dr["Status"]) == (int)EnumStatus.Disabled) ? true : false;
            DataTable dtGroup = sysService.GetDataByKey("T_AdminGroup", "AdminID", _ID);
            SetGroup(dtGroup);
        }
        /// <summary>
        /// 绑定权限组
        /// </summary>
        public void BindGroup()
        {
            DataTable dt = sysService.GetData("T_Group");
            cblGroup.DataSource = dt;
            cblGroup.DataTextField = "GroupName";
            cblGroup.DataValueField = "ID";
            cblGroup.DataBind();
        }
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Admin", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["CreateBy"] = AdminInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
                dr["UpdateBy"] = AdminInfo.ID;
                dr["UpdateTime"] = DateTime.Now;
            }
            dr["MerchantID"] = AdminInfo.MerchantID;
            dr["UserName"] = this.txtUserName.Text;
            if (this.txtPassword.Text != "")
                dr["Password"] = DESEncrypt.Encrypt(this.txtPassword.Text);
            dr["AdminName"] = this.txtAdminName.Text;
            dr["Status"] = this.cbStatus.Checked ? EnumStatus.Disabled : EnumStatus.Normal;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.SaveAdminInfo(dt, GetGroup());
            if (res > 0)
                InvokeScript("CloseWin('添加成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('添加失败！')");
        }
        /// <summary>
        /// 获取权限组
        /// </summary>
        /// <returns></returns>
        public List<string> GetGroup()
        {
            List<string> list = new List<string>();
            foreach (ListItem li in this.cblGroup.Items)
            {
                if (li.Selected)
                    list.Add(li.Value);
            }
            return list;
        }
        /// <summary>
        /// 设置权限组
        /// </summary>
        public void SetGroup(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                foreach (ListItem li in this.cblGroup.Items)
                {
                    if (li.Value == Convert.ToString(dr["GroupID"]))
                        li.Selected = true;
                }
            }
        }
    }
}

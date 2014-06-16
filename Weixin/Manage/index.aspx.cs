using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Weixin.Service;
using System.Data;
using System.Collections;
using Weixin.Common;

namespace Weixin.Web.Manage
{
    public partial class index : AdminPage
    {
        #region IOC注入
        private static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }
        #endregion
        /// <summary>
        /// 菜单
        /// </summary>
        public string StrMenu = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMenu();
            }
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        public void GetMenu()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable hs = new Hashtable();
            hs.Add("Status",1);
            DataTable dt = sysService.GetActionList(hs);
            DataTable dt1 = Utils.SelectDataTable(dt, "isnull(ParentCode,'')=''");
            foreach (DataRow dr1 in dt1.Rows)
            {
                sb.Append("<div title=\"" + Convert.ToString(dr1["Name"]) + "\" data-options=\"iconCls:'" + Convert.ToString(dr1["Icon"]) + "'\" style=\"overflow:auto;padding:10px;\">");
                sb.Append("    <ul class=\"ul\">");
                DataTable dt2 = Utils.SelectDataTable(dt, "ParentCode='"+  Convert.ToString(dr1["Code"]) +"'");
                foreach (DataRow dr2 in dt2.Rows)
                {
                    sb.Append("        <li onclick=\"add('" + Convert.ToString(dr2["Link"]) + "','" + Convert.ToString(dr2["Name"]) + "',this)\"><span>" + Convert.ToString(dr2["Name"]) + "</span></li>");
                }
                sb.Append("    </ul>");
                sb.Append("</div>");
            }
            StrMenu = sb.ToString();
        }
    }
}
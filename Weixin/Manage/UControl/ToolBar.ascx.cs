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
using Spring.Context;
using Weixin.Service;
using Weixin.Model;
using Weixin.Common;
using Spring.Context.Support;
using System.Text;

namespace Weixin.Web.Manage.UControl
{
    public partial class ToolBar : System.Web.UI.UserControl
    {
        IApplicationContext ctx;
        private ISysService sysService;
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string ActionCode
        {
            get;
            set;
        }
            /// <summary>
        /// 当前登录管理员
        /// </summary>
        public AdminInfo MasterInfo
        {
            get
            {
                if (Request.Cookies["AdminInfo"] == null)
                {
                    return null;
                }
                string str = DESEncrypt.Decrypt(Request.Cookies["AdminInfo"].Value);
                return (AdminInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(AdminInfo));
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (MasterInfo != null)
            {
                ctx = ContextRegistry.GetContext();
                sysService = (ISysService)ctx["sysService"];
                StringBuilder str = new StringBuilder();
                Hashtable hs = new Hashtable();
                hs.Add("ParentCode", ActionCode);
                hs.Add("Status", 1);
                DataTable dt = sysService.GetActionList(hs);
                DataTable dtMasterAction = sysService.GetAdminAction(MasterInfo.ID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (CheckAuth( dtMasterAction, dt.Rows[i]["Code"].ToString()) || MasterInfo.UserName.ToLower() == "admin")
                        str.Append("<a href=\"#\" class=\"easyui-linkbutton\" iconcls=\"" + Convert.ToString(dt.Rows[i]["Icon"]) + "\" plain=\"true\" onclick=\"" + Convert.ToString(dt.Rows[i]["Action"]) + "()\"> " + dt.Rows[i]["Name"].ToString() + "</a> ");
                }
                this.Literal_Tb.Text = str.ToString();
            }
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckAuth(DataTable dt, string value)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Code"].ToString() == value)
                    return true;
            }
            return false;
        }
    }
}
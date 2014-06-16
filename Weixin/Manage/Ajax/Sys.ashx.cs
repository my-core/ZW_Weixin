using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weixin.Service;
using Weixin.Common;
using System.Text;

namespace Weixin.Web.Manage.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Sys : BaseAjax, IHttpHandler
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
        /// 验证管理员用户名是否存在
        /// </summary>
        /// <param name="hc"></param>
        public void CheckUserName(HttpContext hc)
        {
            string userName = GetRequestStr("UserName", "");
            DataTable dt = sysService.GetDataByKey("T_Admin", "UserName", userName);
            ResponseWrite(hc, dt.Rows.Count > 0 ? "0" : "1");
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        public void GetAdminList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string name = GetRequestStr("Name", "");
            string status = GetRequestStr("Status", "");
            string minTime = GetRequestStr("MinTime", "");
            string maxTime = GetRequestStr("MaxTime", "");
            if (name != "")
                hs.Add("Name", name);
            if (status != "")
                hs.Add("Status", status);
            if (minTime != "")
                hs.Add("MinTime", minTime);
            if (maxTime != "")
                hs.Add("MaxTime", maxTime);
            Pager p = new Pager(PageSize, PageIndex, "CreateTime desc");
            sysService.GetAdminList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 验证资源Code是否存在
        /// </summary>
        /// <param name="hc"></param>
        public void CheckActinCode(HttpContext hc)
        {
            string code = GetRequestStr("Code", "");
            DataTable dt = sysService.GetDataByKey("T_Action", "Code", code);
            ResponseWrite(hc, dt.Rows.Count > 0 ? "0" : "1");
        }
        /// <summary>
        /// 资源列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetActionList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            DataTable dt = sysService.GetActionList(hs);
            strJson = Utils.CreateTreeJson(dt);
            ResponseWrite(hc);
        }
        /// <summary>
        /// 资源列表
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteAction(HttpContext hc)
        {
            int id = GetRequestInt("ID", 0);
            int res = sysService.DeleteAction(id);
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetGroupList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetGroupList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteGroup(HttpContext hc)
        {
            int id = GetRequestInt("ID", 0);
            int res = sysService.Delete("T_Group","ID", id.ToString());
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
    }
}

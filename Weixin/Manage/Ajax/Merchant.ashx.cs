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
    public class Merchant : BaseAjax, IHttpHandler
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
        /// 商户列表
        /// </summary>
        public void GetMerchantList(HttpContext hc)
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
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            merchantService.GetMerchantList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除商户
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteMerchant(HttpContext hc)
        {
            int id = GetRequestInt("ID", 0);
            int res = 0;
            try
            {
                res = merchantService.Delete("T_Merchant", "ID", id);
            }
            catch (Exception ex)
            {
                Utils.SaveLog("删除商户", ex.Message);
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        /// <summary>
        /// 微信号列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetWechatList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string wechantno = GetRequestStr("WechantNo", "");
            string merchantname = GetRequestStr("MerchantName", "");
            string merchantid = GetRequestStr("MerchantID", "");
            string minTime = GetRequestStr("MinTime", "");
            string maxTime = GetRequestStr("MaxTime", "");
            if (wechantno != "")
                hs.Add("WechantNo", wechantno);
            if (merchantname != "")
                hs.Add("MerchantName", merchantname);
            if (merchantid != "")
                hs.Add("MerchantID", merchantid);
            if (minTime != "")
                hs.Add("MinTime", minTime);
            if (maxTime != "")
                hs.Add("MaxTime", maxTime);
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            merchantService.GetWechatList(p, hs);
            ResponseWrite(hc, p);
        }
    }
}

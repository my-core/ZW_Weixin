using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weixin.Model;
using Weixin.Dao;
using Weixin.Common;
using System.Collections;
using Spring.Transaction.Interceptor;
using Weixin.MP.CommonAPIs;
using Weixin.MP.AdvancedAPIs;

namespace Weixin.Service
{
    public class MerchantService : BaseService, IMerchantService
    {
        #region 注入
        private IMerchantDao merchantDao;
        public IMerchantDao MerchantDao
        {
            set
            {
                merchantDao = value;
            }
        }
        #endregion

        #region 商户
        /// 商户列表
        /// </summary>
        public int GetMerchantList(Pager p, Hashtable hs)
        {
            return merchantDao.GetMerchantList(p, hs);
        }
        /// <summary>
        /// 保存商户（同时为此商户创建一个账户）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [Transaction]
        public int SaveMerchantInfo(DataTable dt, DataTable dtAdmin)
        {
            if (dt.Rows[0].RowState == DataRowState.Added)
            {
                int merchantID = Insert(dt);
                dtAdmin.Rows[0]["MerchantID"] = merchantID;
            }
            else
            {
                UpdateDataTable(dt);
                UpdateDataTable(dtAdmin);
            }
            return RESULT_SUCCESS;
        }
        #endregion 

        #region 微信号
        /// <summary>
        /// 微信号列表
        /// </summary>
        public int GetWechatList(Pager p, Hashtable hs)
        {
            return merchantDao.GetWechatList(p, hs);
        }
        /// <summary>
        /// 保存微信号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [Transaction]
        public int SaveWechatInfo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return RESULT_FAILED;
            string appId = dt.Rows[0]["AppID"].ToString();
            string appSecret = dt.Rows[0]["AppSecret"].ToString();
            try
            {
                AccessTokenContainer.TryGetToken(appId, appSecret, false);
            }
            catch (Exception ex)
            {
                Utils.SaveLog("微信接口：TryGetToken", ex.Message);
                return RESULT_FAILED;
            }
            if (merchantDao.UpdateDataTable(dt) > 0)
                return RESULT_SUCCESS;
            else
                return RESULT_FAILED;
        }
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        public DataTable GetWechatInfo(Hashtable hs )
        {
            return merchantDao.GetWechatInfo(hs);
        }
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        public WechatInfo GetWechatInfoById(int id)
        {
            DataTable dt = GetDataByKey("T_WX_Wechat", "ID", id);
            if (dt.Rows.Count == 0)
                return null;
            return ObjectHelper<WechatInfo>.FillModel(dt.Rows[0]);
        }
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        public WechatInfo GetWechatInfoByNo(string wechatNo)
        {
            DataTable dt = GetDataByKey("T_WX_Wechat", "WechatNo", wechatNo);
            if (dt.Rows.Count == 0)
                return null;
            return ObjectHelper<WechatInfo>.FillModel(dt.Rows[0]);
        }
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        public WechatInfo GetWechatInfoByAppid(string appid)
        {
            DataTable dt = GetDataByKey("T_WX_Wechat", "AppID", appid);
            if (dt.Rows.Count == 0)
                return null;
            return ObjectHelper<WechatInfo>.FillModel(dt.Rows[0]);
        }
        #endregion 
    }
}

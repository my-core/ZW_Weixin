using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Common;
using Weixin.Model;
using System.Collections;
using System.Data;

namespace Weixin.Service
{
    public interface IMerchantService : IBaseService
    {
        #region 商户
        /// <summary>
        /// 商户列表
        /// </summary>
        int GetMerchantList(Pager p, Hashtable hs);
        /// <summary>
        /// 保存商户（同时为此商户创建一个账户）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        int SaveMerchantInfo(DataTable dt, DataTable dtAdmin);
        #endregion

        #region 微信号
        /// <summary>
        /// 微信号列表
        /// </summary>
        int GetWechatList(Pager p, Hashtable hs);
        /// <summary>
        /// 保存微信号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        int SaveWechatInfo(DataTable dt);
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        DataTable GetWechatInfo(Hashtable hs);
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        WechatInfo GetWechatInfoById(int id);
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        WechatInfo GetWechatInfoByNo(string wechatNo);
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        WechatInfo GetWechatInfoByAppid(string appid);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Common;
using System.Collections;
using System.Data;

namespace Weixin.Service
{
    public interface IMerchantService : IBaseService
    {
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
    }
}

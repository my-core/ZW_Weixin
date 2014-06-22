using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Common;
using System.Collections;
using System.Data;

namespace Weixin.Dao
{
    public interface IMerchantDao : IBaseDao
    {
        #region 商户

        /// 商户列表
        /// </summary>
        int GetMerchantList(Pager p, Hashtable hs);

        #endregion 

        #region 微信号

        /// <summary>
        /// 微信号列表
        /// </summary>
        int GetWechatList(Pager p, Hashtable hs);
         /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        DataTable GetWechatInfo(Hashtable hs);

        #endregion
    }
}

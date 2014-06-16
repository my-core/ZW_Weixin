using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Common;
using System.Collections;
using System.Data;

namespace Weixin.Dao
{
    public interface ISysDao : IBaseDao
    {
        /// 用户列表
        /// </summary>
        int GetAdminList(Pager p, Hashtable hs);
        /// 用户列表
        /// </summary>
        DataTable GetActionList(Hashtable hs);
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id">资源ID</param>
        /// <returns></returns>
        int DeleteAction(int id);
        /// 用户列表
        /// </summary>
        int GetGroupList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取用户权限
        /// </summary>
        DataTable GetAdminAction(int adminID);
    }
}

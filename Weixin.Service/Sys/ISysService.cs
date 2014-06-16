using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Common;
using System.Collections;
using System.Data;

namespace Weixin.Service
{
    public interface ISysService:IBaseService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int AdminLogin(string username, string password);
        /// <summary>
        /// 用户列表
        /// </summary>
        int GetAdminList(Pager p, Hashtable hs);
        /// <summary>
        /// 添加用户
        /// </summary>
        int SaveAdminInfo(DataTable dt,List<string> group);
        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <returns></returns>
        DataTable GetActionList(Hashtable hs);
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id">资源ID</param>
        /// <returns></returns>
        int DeleteAction(int id);
        /// </summary>
        /// 角色列表
        /// </summary>
        int GetGroupList(Pager p, Hashtable hs);
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="dtGroup">角色</param>
        /// <param name="dtGroupAction">权限</param>
        /// <returns></returns>
        int SaveGroup(DataTable dtGroup, List<string> groupAction);
        /// <summary>
        /// 获取用户权限
        /// </summary>
        DataTable GetAdminAction(int MasterID);
    }
}

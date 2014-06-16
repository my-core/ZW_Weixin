﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weixin.Model;
using Weixin.Dao;
using Weixin.Common;
using System.Collections;
using Spring.Transaction.Interceptor;

namespace Weixin.Service
{
    public class SysService : BaseService, ISysService
    {
        private ISysDao sysDao;
        public ISysDao SysDao
        {
            set
            {
                sysDao = value;
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>0-成功 1-用户名不存在 2-禁用 3-密码不正确</returns>
        public int AdminLogin(string username, string password)
        {
            DataTable dt = GetDataByKey("T_Admin", "UserName", username);
            if (dt.Rows.Count == 0)
                return 1;
            DataRow dr = dt.Rows[0];
            if (dr["Status"].ToString() == "0")
                return 2;
            if (Convert.ToString(dr["Password"]) != DESEncrypt.Encrypt(password))
                return 3;
            return 0;
        }
        /// 用户列表
        /// </summary>
        public int GetAdminList(Pager p, Hashtable hs)
        {
            return sysDao.GetAdminList(p, hs);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        [Transaction]
        public int SaveAdminInfo(DataTable dtMaster, List<string> group)
        {
            int res = 0;
            int masterID = 0;
            if (dtMaster.Rows.Count == 0)
                return res;
            //添加或更新
            if (!string.IsNullOrEmpty(dtMaster.Rows[0]["ID"].ToString()))
            {
                masterID = Convert.ToInt32(dtMaster.Rows[0]["ID"].ToString());
                res = UpdateDataTable(dtMaster);
            }
            else
            {
                masterID = Insert(dtMaster);
            }
            //删除角色权限
            Delete("T_AdminGroup", "AdminID=" + masterID);
            //添加用户关系组
            DataTable dt = GetDataByKey("T_AdminGroup", "ID", 0);
            group.ForEach(g =>
            {
                DataRow dr = dt.NewRow();
                dr["GroupID"] = g;
                dr["AdminID"] = masterID;
                dt.Rows.Add(dr);
            });
            UpdateDataTable(dt);
            return res;
        }
        /// <summary>
        /// 获取网站栏目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetActionList(Hashtable hs)
        {
            DataTable dt = sysDao.GetActionList(hs);
            return dt;           
        }
        /// 角色列表
        /// </summary>
        public int GetGroupList(Pager p, Hashtable hs)
        {
            return sysDao.GetGroupList(p, hs);
        }
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id">资源ID</param>
        /// <returns></returns>
        public int DeleteAction(int id)
        {
            return sysDao.DeleteAction(id);
        }
        /// <summary>
        /// 保存角色及角色所拥有权限
        /// </summary>
        /// <param name="dtGroup">角色</param>
        /// <param name="dtGroupAction">权限</param>
        /// <returns></returns>
        [Transaction]
        public int SaveGroup(DataTable dtGroup, List<string> groupAction)
        {
            int res = 0;
            int groupID = 0;
            if (dtGroup.Rows.Count == 0)
                return res;
            //添加或更新
            if (!string.IsNullOrEmpty(dtGroup.Rows[0]["ID"].ToString()))
            {
                groupID = Convert.ToInt32(dtGroup.Rows[0]["ID"].ToString());
                res = UpdateDataTable(dtGroup);
            }
            else
            {
                groupID = Insert(dtGroup);
                res = 1;
            }
            //删除角色权限
            Delete("T_ActionGroup", "GroupID=" + groupID);
            //添加角色权限
            DataTable dt = GetDataByKey("T_ActionGroup", "ID", 0);
            groupAction.ForEach(g =>
            {
                DataRow dr = dt.NewRow();
                dr["ActionCode"] = g;
                dr["GroupID"] = groupID;
                dt.Rows.Add(dr);
            });
            UpdateDataTable(dt);
            return res;
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        public DataTable GetAdminAction(int adminID)
        {
            return sysDao.GetAdminAction(adminID);
        }
    }
}

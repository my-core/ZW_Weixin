using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Weixin.Common;
using System.Data;
using System.Collections;

namespace Weixin.Dao
{
    public class SysDao : BaseDao, ISysDao
    {
        /// 用户列表
        /// </summary>
        public int GetAdminList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as MerchantName, c.AdminName as CreateAdmin,d.AdminName as UpdateAdmin from T_Admin a 
                            left join T_Merchant b on b.ID=a.MerchantID
                            left join T_Admin c on c.ID=a.CreateBy
                            left join T_Admin d on d.ID=a.UpdateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and (a.AdminName like @Name or a.UserName like @Name) ";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("Status"))
            {
                sql += " and a.Status = @Status) ";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and  datediff(day,a.CreateTime,@MinTime )<=0";
                param.AddWithValue("MinTime", hs["MinTime"].ToString());
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day,a.CreateTime,@MaxTime )>=0 ";
                param.AddWithValue("MaxTime", hs["MaxTime"].ToString());
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// 资源列表
        /// </summary>
        public DataTable GetActionList(Hashtable hs)
        {
            string sql = "select * from T_Action where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentCode"))
            {
                sql += " and ParentCode=@ParentCode";
                param.AddWithValue("ParentCode", hs["ParentCode"]);
            }
            if (hs.Contains("Status"))
            {
                sql += " and Status=@Status ";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("ModuleID"))
            {
                sql += " and ModuleID=@ModuleID ";
                param.AddWithValue("ModuleID", hs["ModuleID"]);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id">资源ID</param>
        /// <returns></returns>
        public int DeleteAction(int id)
        {
            string sql = @"with a as
                            (
	                            select ID, Code from T_Action where ID=@ID
	                            union all
	                            select b.ID, b.Code from T_Action b,a where b.ParentCode =a.Code
                            )
                            delete from T_Action where ID in (select ID from a)";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("ID", id);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param);
        }
        /// 角色列表
        /// </summary>
        public int GetGroupList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.AdminName from T_Group a
                            left join T_Admin b on b.ID=a.CreateBy";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetAdminAction(int adminID)
        {
            string sql = @"select * from T_Action where Code in
                            (
                                select ActionCode from T_ActionGroup  where GroupID in 
                                (select GroupID from T_AdminGroup where AdminID=@AdminID )
                            )";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("AdminID", adminID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
    }
}

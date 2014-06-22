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
    public class MerchantDao : BaseDao, IMerchantDao
    {
        #region 商户

        /// 商户列表
        /// </summary>
        public int GetMerchantList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.AdminName as CreateAdmin,c.AdminName as UpdateAdmin from T_Merchant a
                            left join T_Admin b on b.ID=a.CreateBy
                            left join T_Admin c on c.ID=a.UpdateBy 
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if(hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("Status"))
            {
                sql += " and a.Status = @Status";
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

        #endregion

        #region 微信号

        /// <summary>
        /// 微信号列表
        /// </summary>
        public int GetWechatList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as MerchantName, c.AdminName as CreateAdmin,d.AdminName as UpdateAdmin from T_WX_WeChat a 
                            left join T_Merchant b on b.ID=a.MerchantID
                            left join T_Admin c on c.ID=a.CreateBy
                            left join T_Admin d on d.ID=a.UpdateBy";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MerchantName"))
            {
                sql += " and b.Name like @MerchantName";
                param.AddWithValue("MerchantName", "%" + hs["MerchantName"] + "%");
            }
            if (hs.Contains("MerchantID"))
            {
                sql += " and a.MerchantID = @MerchantID";
                param.AddWithValue("MerchantID", hs["MerchantID"]);
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
        /// <summary>
        /// 获取微信号
        /// </summary>
        /// <returns></returns>
        public DataTable GetWechatInfo(Hashtable hs)
        {
            string sql = "select * from T_WX_Wechat where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ID"))
            {
                sql += " and ID=@ID";
                param.AddWithValue("ID", hs["ID"]);
            }
            if (hs.Contains("WechatNo"))
            {
                sql += " and WechatNo=@WechatNo";
                param.AddWithValue("WechatNo", hs["WechatNo"]);
            }
            if (hs.Contains("AppID"))
            {
                sql += " and AppID=@AppID";
                param.AddWithValue("AppID", hs["AppID"]);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            dt.TableName = "T_WX_Wechat";
            return dt;
        }

        #endregion
    }
}

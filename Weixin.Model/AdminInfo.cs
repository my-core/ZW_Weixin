using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weixin.Model
{
    public class AdminInfo
    {
        public AdminInfo() { }
        
        private int _id;
        private int _merchantid;
        private string _username;
        private string _password;
        private string _adminname;
        private bool _disabled;
        private int _createby;
        private DateTime _createtime;

        /// <summary>
        ///  ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属商户
        /// </summary>
        public int MerchantID
        {
            set { _merchantid = value; }
            get { return _merchantid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled
        {
            set { _disabled = value; }
            get { return _disabled; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy
        {
            set { _createby = value; }
            get { return _createby; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
    }
}
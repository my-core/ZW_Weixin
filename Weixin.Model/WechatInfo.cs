using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weixin.Model
{
    /// <summary>
    /// 微信号
    /// </summary>
    public class WechatInfo
    {
        public int ID;
        public int MerchantID;
        public int Type;
        public string WeChatNo;
        public string Password;
        public string AppID;
        public string AppSecret;
        public string TokenL;
        public string ApiUrl;
        public DateTime ValidDate;
        public int TextLimitCount;
        public int ImageTextLimitCount;
        public int RequestLimitCount;
        public int RequestCount;
        public int CreateBy;
        public DateTime CreateTime;
        public int UpdateBy;
        public DateTime UpdateTime;
    }
}

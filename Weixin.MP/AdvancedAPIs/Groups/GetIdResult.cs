using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.MP.Entities;

namespace Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取用户分组ID返回结果
    /// </summary>
    public class GetGroupIdResult : WxJsonResult
    {
        public int groupid { get; set; }
    }
}

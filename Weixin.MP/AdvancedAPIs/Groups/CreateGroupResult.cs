﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.MP.Entities;

namespace Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 创建分组返回结果
    /// </summary>
    public class CreateGroupResult : WxJsonResult
    {
        public GroupsJson_Group group { get; set; }
    }
}

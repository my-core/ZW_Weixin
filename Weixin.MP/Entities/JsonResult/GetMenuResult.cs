using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.MP.Entities.Menu;

namespace Weixin.MP.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult
    {
        public ButtonGroup menu { get; set; }

        public GetMenuResult()
        {
            menu = new ButtonGroup();
        }
    }
}

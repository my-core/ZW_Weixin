using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weixin.Web.Manage.Admin
{
    public partial class ActionList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.ActionCode = "ActionList";
            }
        }
    }
}
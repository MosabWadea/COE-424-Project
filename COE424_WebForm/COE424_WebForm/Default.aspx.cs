using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COE424_WebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tableCounter_Tick(object sender, EventArgs e)
        {
            ScheduleGrid.DataBind();
        }
    }
}
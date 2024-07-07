using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELogisticWareHouse
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserName"] == null)
            //    Response.Redirect("Login.aspx");
            //else
            //    loginuser.Text = Convert.ToString(Session["FullName"]) + "...";
            if (Convert.ToInt32(Session["IsAdmin"]) != 0)
            {
                category.Style.Add("display", "none");
                medicine.Style.Add("display", "none");
                brand.Style.Add("display", "none");
            }
        }
        
        protected void lnkbtnLogout_Click1(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
using DALayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELogisticWareHouse
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser Dal = new clsUser();
            BLayer.User obj = InitUser();
            DataSet ds = new DataSet();
            ds = Dal.ISValid(obj);
            if (ds.Tables[0].Rows.Count == 1)
            {
                Session["IsAdmin"] = ds.Tables[0].Rows[0]["IsAdmin"];
                Session["UserName"] = ds.Tables[0].Rows[0]["UserName"];
                Session["FullName"] = ds.Tables[0].Rows[0]["FullName"];
                Session["UserId"] = ds.Tables[0].Rows[0]["UserId"];
                if (int.Parse("0" + ds.Tables[0].Rows[0]["IsAdmin"]) == 0)
                    Response.Redirect("Home.aspx");
                else
                    Response.Redirect("Default.aspx");
            }
            else
            {
                lblmessage.Text = "Invalid UserName or Password";
            }
        }
        private BLayer.User InitUser()
        {
            BLayer.User obj = new BLayer.User();
            obj.username = txtUserName.Text;
            obj.password = txtPassword.Text;
            return obj;
        }
    }
}
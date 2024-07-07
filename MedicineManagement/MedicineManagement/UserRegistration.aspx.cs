using System;
using System.Web.UI.WebControls;

using DALayer;
using System.Data;

namespace InventoryManagmentSystem
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        clsUser Dal = new clsUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearAll();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLayer.User obj = InitalizeObject();
                Dal.AddNewUser(obj);
                ClearAll();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script language='javascript'>alert('User Registration has been done successfully')</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            BLayer.User obj = new  BLayer.User();
            obj = InitalizeObject();
            Dal.UpdateUser(obj);
            ClearAll();
      
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            BLayer.User obj = InitalizeObject();
            int userId = Convert.ToInt16(ViewState["userId"]);
            Dal.DeleteUser(userId);
            ClearAll();
        }

        

        private BLayer.User InitalizeObject()
        {
            BLayer.User obj = new BLayer.User();
            obj.UserId = Convert.ToInt16(ViewState["userId"]);
            obj.username = txtUserName.Text;
            obj.email = txtEmail.Text;
            obj.FullName = txtFullName.Text;
            obj.password = txtPassword.Text;
            obj.Address = txtAddress.Text;
            obj.ContactNumber = txtContact.Text;
            obj.IsAdmin = 1;
            return obj;
        }

        public void ClearAll()
        {
            txtFullName.Text = string.Empty;
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmpassword.Text = "";
            //rdobtnUserType.SelectedValue = null;
            BtnSave.Enabled = true;
        }
       
    }
}
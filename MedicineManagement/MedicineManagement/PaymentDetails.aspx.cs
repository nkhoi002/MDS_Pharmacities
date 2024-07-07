
using DALayer;
using System;
using System.Data;
using System.Web.UI;


namespace BikesWebApp
{
    public partial class PaymentDetails : System.Web.UI.Page
    {
        clsMedicine Dal = new clsMedicine();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            txtAmount.Text = Request.QueryString["Amount"];
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                int medicineId = int.Parse("0" + Request.QueryString["MedicineId"]);
                decimal Amount = decimal.Parse("0" + Request.QueryString["Amount"]);
                int Quantity = int.Parse("0" + Request.QueryString["Quantity"]);
                decimal UnitPrice = decimal.Parse("0" + Request.QueryString["UnitPrice"]);
                int userId = int.Parse("0" + Session["UserId"]);
                string insertQuery = "insert into tbl_Order(Price,OrderDate,MedicineId,CustId,Quantity,TotalAmount) values(" + UnitPrice + ",getdate()," + int.Parse("0"+ medicineId) + "," + Convert.ToInt32(Session["UserId"]) + ","+Quantity+","+Amount+")";
                Dal.MakePayment(insertQuery);

                string orderQuery = "select Max(OrderId) From tbl_Order";
                DataTable dt = Dal.ExecuteSqlString(orderQuery).Tables[0];
                int orderId = int.Parse("0" + dt.Rows[0][0]);
                string payment = "insert into tbl_PaymentDetails(UserId,OrderId,PaymentTypeId,Amount,CreditCardNo,CVV,ExpiryDate,NameOnCard,Status)" +
                    "values(" + userId + ","+ orderId + "," + int.Parse("0" + ddlPaymentType.SelectedValue) + "," + decimal.Parse("0" + txtAmount.Text) + "," +
                    "'" + txtCardNumber.Text + "','" + txtCVV.Text + "','" + txtExpiryMonth.Text + "','" + txtNameOnCard.Text + "',1)";
                int pay = Dal.MakePayment(payment);
                if (pay > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script language='javascript'>alert('Your payment has been done successfully!!!')</script>");
                    //Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
        }
    }
}
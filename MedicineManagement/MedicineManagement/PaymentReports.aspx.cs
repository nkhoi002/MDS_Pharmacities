using DALayer;
using System;
using System.Data;

namespace BikesWebApp
{
    public partial class PaymentReports : System.Web.UI.Page
    {
        clsMedicine obj = new clsMedicine();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                string query = "select R.FullName,R.UserId,R.ContactNumber,R.Email,P.PaymentId,P.Amount,P.CreditCardNo,P.NameOnCard,P.OrderId,O.OrderNumber," +
                                " Case When P.PaymentTypeId = 1 then 'Credit Card' else 'Debit Card' end PaymentType, P.PaymentNumber ,O.OrderDate," +
                                "  CASE when P.Status = 1 Then 'Success' else 'Failed' end Status, convert(varchar(20), P.PaymentDate, 106) PaymentDate" +
                                " from tbl_PaymentDetails P Join tbl_Login R ON R.UserId = P.UserId Join tbl_Order O ON O.OrderId=P.OrderId";
                DataSet ds = new DataSet();
                ds = obj.PaymentReport(query);

                gvPaymentReport.DataSource = ds.Tables[0];
                gvPaymentReport.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void gvPayment_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            if (Session["UserId"] == null)
            {
                string query = "select R.FullName,R.UserId,R.ContactNumber,R.Email,P.PaymentId,P.Amount,P.CreditCardNo,P.NameOnCard,P.OrderId,O.OrderNumber," +
                               " Case When P.PaymentTypeId = 1 then 'Credit Card' else 'Debit Card' end PaymentType, P.PaymentNumber ,O.OrderDate," +
                               "  CASE when P.Status = 1 Then 'Success' else 'Failed' end Status, convert(varchar(20), P.PaymentDate, 106) PaymentDate" +
                               " from tbl_PaymentDetails P Join tbl_Login R ON R.UserId = P.UserId Join tbl_Order O ON O.OrderId=P.OrderId";
                DataSet ds = new DataSet();
                ds = obj.PaymentReport(query);

                gvPaymentReport.DataSource = ds.Tables[0];
                gvPaymentReport.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
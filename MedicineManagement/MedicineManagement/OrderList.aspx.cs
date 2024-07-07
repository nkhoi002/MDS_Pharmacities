using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BikesWebApp
{
    public partial class OrderList : System.Web.UI.Page
    {
        public SqlConnection conString =new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (conString.State == ConnectionState.Closed)
                conString.Open();
            string query = "select OrderId,Price,OrderDate,M.MedicineId,CustId,OrderNumber,Quantity,TotalAmount,UserId,FullName,r.ContactNumber,r.Address,MedicineName+ '-'+B.ManufacturerName+' ('+C.CategoryName+')' Medicine from tbl_Order "+
                " join tbl_Login r on r.UserId=tbl_Order.CustId JOIN tbl_MedicineDetail M ON M.MedicineId=tbl_Order.MedicineId "+
                " Join tbl_Category C ON C.CategoryId=M.CategoryId JOIN tbl_Manufacturer B ON B.ManufacturerID=M.ManufacturerId";
            DataSet dsOrder = new DataSet();
            SqlCommand cmd = new SqlCommand(query, conString);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dsOrder);
            sda.Dispose();
            gvOrderList.DataSource = dsOrder;
            gvOrderList.DataBind();
        }

        protected void gvOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvOrderList.PageIndex = e.NewPageIndex;

                string query = "select OrderId,Price,OrderDate,M.MedicineId,CustId,OrderNumber,Quantity,TotalAmount,UserId,FullName,r.ContactNumber,r.Address,MedicineName+ '-'+B.ManufacturerName+' ('+C.CategoryName+')' Medicine from tbl_Order " +
                " join tbl_Login r on r.UserId=tbl_Order.CustId JOIN tbl_MedicineDetail M ON M.MedicineId=tbl_Order.MedicineId " +
                " Join tbl_Category C ON C.CategoryId=M.CategoryId JOIN tbl_Manufacturer B ON B.ManufacturerID=M.ManufacturerId";
                if (conString.State == ConnectionState.Closed)
                    conString.Open();
                DataSet dsOrder = new DataSet();
                SqlCommand cmd = new SqlCommand(query, conString);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dsOrder);
                sda.Dispose();
                gvOrderList.DataSource = dsOrder;
                gvOrderList.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedicineManagement
{
    public partial class SearchMedicine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                BindMedicineGrid();
        }
        [WebMethod]
        public static List<MedicineDetails> GetMedicineList(string searchTerm)
        {
            List<MedicineDetails> medicineList = new List<MedicineDetails>();
            string query = string.Format("select MD.MedicineId, Md.CategoryId,MD.ManufacturerId,Md.MedicineName,M.ManufacturerName,C.CategoryName,Md.MedicineImagePath,MD.Description,MD.Amount FROM tbl_MedicineDetail MD" +
                                " JOIN tbl_Manufacturer M ON M.ManufacturerID = MD.ManufacturerId" +
                                " JOIN tbl_Category C ON C.CategoryId = MD.CategoryId" +
                                " WHERE MD.MedicineName like'%" + searchTerm + "%' OR M.ManufacturerName like'%{0}%' OR C.CategoryName like'%{0}%'", searchTerm);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ToString()))
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    sda.Dispose();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            MedicineDetails medicine = new MedicineDetails();
                            medicine.Amount = decimal.Parse("0" + dr["Amount"]);
                            medicine.MedicineId = int.Parse("0" + dr["MedicineId"]);
                            medicine.CategoryId = int.Parse("0" + dr["CategoryId"]);
                            medicine.ManufacturerId = int.Parse("0" + dr["ManufacturerId"]);
                            medicine.MedicineName = Convert.ToString(dr["MedicineName"]);
                            medicine.ManufacturerName = Convert.ToString(dr["ManufacturerName"]);
                            medicine.CategoryName = Convert.ToString(dr["CategoryName"]);
                            medicine.MedicineImagePath = Convert.ToString(dr["MedicineImagePath"]);
                            medicine.Description = Convert.ToString(dr["Description"]);
                            medicineList.Add(medicine);
                        }
                    }

                }
            }
            return medicineList;
        }
        [WebMethod]
        public static MedicineDetails GetMedicineDetails(int medicineId)
        {
            MedicineDetails medicine = new MedicineDetails();
            string query = string.Format("select MD.MedicineId, Md.CategoryId,MD.ManufacturerId,Md.MedicineName,M.ManufacturerName,C.CategoryName,Md.MedicineImagePath,MD.Description,MD.Amount FROM tbl_MedicineDetail MD" +
                                " JOIN tbl_Manufacturer M ON M.ManufacturerID = MD.ManufacturerId" +
                                " JOIN tbl_Category C ON C.CategoryId = MD.CategoryId" +
                                " WHERE MD.MedicineId ={0}", medicineId);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ToString()))
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    sda.Dispose();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            medicine.Amount = decimal.Parse("0" + dr["Amount"]);
                            medicine.MedicineId = int.Parse("0" + dr["MedicineId"]);
                            medicine.CategoryId = int.Parse("0" + dr["CategoryId"]);
                            medicine.ManufacturerId = int.Parse("0" + dr["ManufacturerId"]);
                            medicine.MedicineName = Convert.ToString(dr["MedicineName"]);
                            medicine.ManufacturerName = Convert.ToString(dr["ManufacturerName"]);
                            medicine.CategoryName = Convert.ToString(dr["CategoryName"]);
                            medicine.MedicineImagePath = Convert.ToString(dr["MedicineImagePath"]);
                            medicine.Description = Convert.ToString(dr["Description"]);
                        }
                    }

                }
            }
            return medicine;
        }
        public void BindMedicineGrid()
        {
            int categoryId = int.Parse("0" + Request.QueryString["CategoryId"]);
            int brandId = int.Parse("0" + Request.QueryString["BrandId"]);
            if (brandId > 0)
            {
                divMedicineGrid.Style.Add("display", "block");
                string query = string.Format("select MD.MedicineId, Md.CategoryId,MD.ManufacturerId,Md.MedicineName,M.ManufacturerName,C.CategoryName,Md.MedicineImagePath,MD.Description,MD.Amount FROM tbl_MedicineDetail MD" +
                                    " JOIN tbl_Manufacturer M ON M.ManufacturerID = MD.ManufacturerId" +
                                    " JOIN tbl_Category C ON C.CategoryId = MD.CategoryId" +
                                    " WHERE MD.ManufacturerID ={0}", brandId);
                //string htmlText = string.Empty;
                string htmlMedicine = string.Empty;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ToString()))
                {
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        sda.Dispose();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                /*htmlText += "<tr><td>" + Convert.ToString(dr["MedicineName"]) + "</td>" +
                                            "<td>" + Convert.ToString(dr["ManufacturerName"]) + "</td>"+
                                            "<td>" + Convert.ToString(dr["CategoryName"]) + "</td>"+
                                            "<td>" + Convert.ToString(dr["Amount"]) + "</td>"+
                                            "<td><a id='btnSaveGrid' onclick ='MakeOrderByGrid(" + int.Parse("0" + dr["MedicineId"]) + ");' class='btn btn-info'>Make Order</a></td>" +
                                            "</tr>";*/
                                htmlMedicine += "<div class='col-lg-3'>" +
                                                "<div style='min-height: 350px;' class='panel panel-danger'>" +
                                                "<div class='panel-heading'>" + Convert.ToString(dr["MedicineName"]) +" - "+ Convert.ToString(dr["ManufacturerName"]) + " (" + Convert.ToString(dr["CategoryName"]) + ") </div>" +
                                                "<div class='panel-body'>" +
                                                    "<img style='width:100%;max-height:170px;min-height:170px;' src=' /images/ProductImages/" + Convert.ToString(dr["MedicineImagePath"]) + "' /><br /><strong>Description :</strong>"+ Convert.ToString(dr["Description"]) + " <br /> <strong>Price :</strong>" + Convert.ToString(dr["Amount"]) +
                                                    "<br /> <a id='btnSaveGrid' onclick ='MakeOrderByGrid(" + int.Parse("0" + dr["MedicineId"]) + ");' class='btn btn-info btn-block'>Make Order</a>" +
                                                "</div></div></div> ";
                            }
                            //ltrlMedicine.Text = htmlText;
                            ltrlMedical.Text = htmlMedicine;
                        }
                    }
                }
            }
            else if (categoryId > 0)
            {
                divMedicineGrid.Style.Add("display", "block");
                string query = string.Format("select MD.MedicineId, Md.CategoryId,MD.ManufacturerId,Md.MedicineName,M.ManufacturerName,C.CategoryName,Md.MedicineImagePath,MD.Description,MD.Amount FROM tbl_MedicineDetail MD" +
                                   " JOIN tbl_Manufacturer M ON M.ManufacturerID = MD.ManufacturerId" +
                                   " JOIN tbl_Category C ON C.CategoryId = MD.CategoryId" +
                                   " WHERE MD.CategoryId ={0}", categoryId);
                //string htmlText = string.Empty;
                string htmlMedicine = string.Empty;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ToString()))
                {
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        sda.Dispose();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                /*htmlText += "<tr><td>" + Convert.ToString(dr["MedicineName"]) + "</td>" +
                                            "<td>" + Convert.ToString(dr["ManufacturerName"]) + "</td>" +
                                            "<td>" + Convert.ToString(dr["CategoryName"]) + "</td>" +
                                            "<td>" + Convert.ToString(dr["Amount"]) + "</td>" +
                                            "<td><a id='btnSaveGrid' onclick ='MakeOrderByGrid("+int.Parse("0"+dr["MedicineId"])+");' class='btn btn-info'>Make Order</a></td>" +
                                            "</tr>";*/
                                htmlMedicine += "<div class='col-lg-3'>" +
                                                "<div style='min-height:350px;' class='panel panel-danger'>" +
                                                "<div class='panel-heading'>" + Convert.ToString(dr["MedicineName"]) + " - " + Convert.ToString(dr["ManufacturerName"]) + " (" + Convert.ToString(dr["CategoryName"]) + ") </div>" +
                                                "<div class='panel-body'>" +
                                                    "<img style='width:100%;max-height:170px;min-height:170px;' src = '/images/ProductImages/" + Convert.ToString(dr["MedicineImagePath"]) + "'/><br /><strong>Description :</strong>" + Convert.ToString(dr["Description"]) + " <br /> <strong>Price :</strong>" + Convert.ToString(dr["Amount"]) +
                                                    "<br /> <a id='btnSaveGrid' onclick ='MakeOrderByGrid(" + int.Parse("0" + dr["MedicineId"]) + ");' class='btn btn-info btn-block'>Make Order</a>" +
                                                "</div></div></div> ";
                            }
                            //ltrlMedicine.Text = htmlText;
                            ltrlMedical.Text = htmlMedicine;
                        }
                    }
                }
            }
            else
            {
                divMedicineGrid.Style.Add("display", "none");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentDetails.aspx?Amount=" + lblTotalPrice.Text + "&MedicineId=" + hdnMedicineId.Value+"&Quantity="+txtQuantity.Text+"&UnitPrice="+lblUnitPrice.Text);
        }
    }

    public class MedicineDetails
    {
        public int MedicineId { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string MedicineName { get; set; }
        public string ManufacturerName { get; set; }
        public string CategoryName    { get; set; }
        public string MedicineImagePath { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
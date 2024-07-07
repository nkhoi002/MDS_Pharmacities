using DALayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedicineManagement
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LoadCategory();
                LoadBrand();
            }
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        private void LoadCategory()
        {
            clsCategory Dal = new clsCategory();
            DataSet dsCategory = Dal.LoadCategory();
            string htmlCategory = string.Empty;
            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCategory.Tables[0].Rows)
                {
                    htmlCategory += "<li><a href='SearchMedicine.aspx?CategoryId="+int.Parse("0"+dr["CategoryId"])+"'>"+Convert.ToString(dr["CategoryName"])+"</a></li>";
                }
                ltrlCategory.Text = htmlCategory;
            }
            else
            {
                htmlCategory = "<li><a href='#'>No Category Found</a></li>";
                ltrlCategory.Text = htmlCategory;
            }
        }
        private void LoadBrand()
        {
            clsManufacturer Dal = new clsManufacturer();
            DataSet dsBrand = Dal.LoadManufacturer();
            string htmlBrand = string.Empty;
            if (dsBrand != null && dsBrand.Tables.Count > 0 && dsBrand.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBrand.Tables[0].Rows)
                {
                    htmlBrand += "<li><a href='SearchMedicine.aspx?BrandId="+int.Parse("0"+ dr["ManufacturerID"])+ "'>"+Convert.ToString(dr["ManufacturerName"])+"</a></li>";
                }
                ltrlBrand.Text = htmlBrand;
            }
            else
            {
                htmlBrand = "<li><a href='#'>No Brand Found</a></li>";
                ltrlBrand.Text = htmlBrand;
            }
        }
    }
}
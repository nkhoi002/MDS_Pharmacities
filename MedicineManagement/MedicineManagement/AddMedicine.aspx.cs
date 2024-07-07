using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLayer;
using DALayer;
using System.Data;

namespace MedicineManagement
{
    public partial class AddMedicine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadManuf();
                LoadCategory();
                ClearAll();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            clsMedicine Dal = new clsMedicine();
            BLayer.Medicine obj = InitalizeObject();
            Dal.AddNewMedicine(obj);
            LoadGrid();

            ClearAll();

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            clsMedicine Dal = new clsMedicine();
            BLayer.Medicine obj = InitalizeObject();

            Dal.UpdateMedicineDetail(obj);
            LoadGrid();
            LoadCategory();
            ClearAll();
        }

        public void ClearAll()
        {
            txtMedicineName.Text = "";
            txtMedicineDescription.Text = "";
            txtPrice.Text = "";
            ddlBrand.SelectedIndex = 0;
            BtnSave.Enabled = true;

        }

        private void LoadManuf()
        {
            clsMedicine Dal = new clsMedicine();
            DataSet Manuf = Dal.FillManufacturer();
            ddlBrand.DataTextField = "ManufacturerName";
            ddlBrand.DataValueField = "ManufacturerID";
            ddlBrand.DataSource = Manuf.Tables[0];
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("--Select Brand--", string.Empty));
        }
        private void LoadCategory()
        {
            clsCategory Dal = new clsCategory();
            DataSet Manuf = Dal.LoadCategory();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataSource = Manuf.Tables[0];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--", string.Empty));
        }
        private BLayer.Medicine InitalizeObject()
        {
            string filename = string.Empty;
            BLayer.Medicine obj = new BLayer.Medicine();
            obj.CreatedBy = int.Parse("0" + Session["UserId"]);
            obj.MedicineName = txtMedicineName.Text;
            obj.MedicineDescription = txtMedicineDescription.Text;
            obj.Price = Convert.ToDecimal(txtPrice.Text);
            obj.ManufId = int.Parse("0" + ddlBrand.SelectedValue);
            obj.MedicineId = int.Parse("0"+hdnMedicine.Value);
            obj.CategoryId = int.Parse("0" + ddlCategory.SelectedValue);
            if (fileUpload.HasFile)
            {
                fileUpload.SaveAs(MapPath("~/images/ProductImages/") + fileUpload.FileName);
                hdnFileUpload.Value = fileUpload.FileName;
            }
            else
            {
                hdnFileUpload.Value = string.IsNullOrEmpty(hdnFileUpload.Value) ? "300px - No_image_available.svg" : hdnFileUpload.Value;
            }
            obj.MedicineImage = hdnFileUpload.Value;
            return obj;
        }

        private void LoadGrid()
        {
            clsMedicine Dal = new clsMedicine();
            DataSet Medicines = Dal.LoadMedicine(int.Parse("0"));
            GridView1.DataSource = Medicines.Tables[0];
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            ddlBrand.Items.Clear();
            ddlCategory.Items.Clear();
            LoadManuf();
            LoadCategory();
            txtMedicineName.Text = row.Cells[1].Text;
            txtMedicineDescription.Text = row.Cells[2].Text;
            txtPrice.Text = row.Cells[3].Text;
            HiddenField field = (HiddenField)row.FindControl("hdnManufacturerId");
            HiddenField image = (HiddenField)row.FindControl("hdnProductImage");
            HiddenField MedicineId = (HiddenField)row.FindControl("hdnMedicineId");
            HiddenField categoryId = (HiddenField)row.FindControl("hdnCategoryId");
            hdnFileUpload.Value = image.Value;
            ddlBrand.SelectedValue = field.Value;
            ddlCategory.SelectedValue = categoryId.Value;
            hdnMedicine.Value = MedicineId.Value;
            BtnSave.Enabled = false;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}
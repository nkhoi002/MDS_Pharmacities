using DALayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvontryManagmentSystem
{
    public partial class Manufacturer : System.Web.UI.Page
    {
        clsManufacturer Dal = new clsManufacturer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                ClearAll();
            }
        }
        private void LoadGrid()
        {

            DataSet suppliers = Dal.LoadManufacturer();
            GridView1.DataSource = suppliers.Tables[0];
            GridView1.DataBind();
        }
        public void ClearAll()
        {

            txtAddress.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            BtnSave.Enabled = true;

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            BLayer.Manufacturer obj = InitalizeObject();
            Dal.AddNewManufacturer(obj);
            LoadGrid();
            ClearAll();
        }
        private BLayer.Manufacturer InitalizeObject()
        {


            BLayer.Manufacturer obj = new BLayer.Manufacturer();
            obj.ManufacturerName = txtManufacturerName.Text;
            obj.Address = txtAddress.Text;
            obj.PhoneNumber = txtPhoneNumber.Text;
            obj.Email = txtEmail.Text;
            obj.ManufacturerID = int.Parse("0" + hdnManufacturerId.Value);
            return obj;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            BLayer.Manufacturer obj = new BLayer.Manufacturer();
            obj = InitalizeObject();
            Dal.UpdateManufacturer(obj);
            LoadGrid();
            ClearAll();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            BLayer.Manufacturer obj = InitalizeObject();
            string userId = hdnManufacturerId.Value;
            Dal.DeleteManufacturer(userId);
            LoadGrid();
            ClearAll();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            hdnManufacturerId.Value = row.Cells[1].Text;
            txtManufacturerName.Text = row.Cells[2].Text;
            txtAddress.Text = row.Cells[3].Text;
            txtPhoneNumber.Text = row.Cells[4].Text;
            txtEmail.Text = row.Cells[5].Text;
            BtnSave.Enabled = false;

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}
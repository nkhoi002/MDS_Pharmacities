using DALayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedicineManagement
{
    public partial class CategoryManagement : Page
    {
        clsCategory Dal = new clsCategory();
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

            DataSet suppliers = Dal.LoadCategory();
            GridView1.DataSource = suppliers.Tables[0];
            GridView1.DataBind();
        }
        public void ClearAll()
        {
            txtCategoryName.Text = "";
            txtDescription.Text = "";
            BtnSave.Enabled = true;
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            BLayer.Category obj = InitalizeObject();
            Dal.AddNewCategory(obj);
            LoadGrid();
            ClearAll();
        }
        private BLayer.Category InitalizeObject()
        {
            BLayer.Category obj = new BLayer.Category();
            obj.CategoryName = txtCategoryName.Text;
            obj.Description = txtDescription.Text;
            obj.CategoryId = int.Parse("0"+hdnCategoryId.Value);

            return obj;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            BLayer.Category obj = new BLayer.Category();
            obj = InitalizeObject();
            Dal.UpdateCategory(obj);
            LoadGrid();
            ClearAll();
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            BLayer.Category obj = InitalizeObject();
            string userId = hdnCategoryId.Value;
            Dal.DeleteCategory(userId);
            LoadGrid();
            ClearAll();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            hdnCategoryId.Value = row.Cells[1].Text;
            txtCategoryName.Text = row.Cells[2].Text;
            txtDescription.Text = row.Cells[3].Text;
            BtnSave.Enabled = false;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}
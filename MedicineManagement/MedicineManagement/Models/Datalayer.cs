using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BLayer;


namespace DALayer
{
    public class clsDataLayer
    {
        public string conString = ConfigurationManager.ConnectionStrings["DBcon"].ToString();
        public DataSet ExecuteSqlString(string sqlstring)
        {
            try
            {
                SqlConnection objsqlconn = new SqlConnection(conString);
                objsqlconn.Open();

                DataSet Ds = new DataSet();
                SqlCommand objcmd = new SqlCommand(sqlstring, objsqlconn);
                SqlDataAdapter objAdp = new SqlDataAdapter(objcmd);
                objAdp.Fill(Ds);
                return Ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertUpdateDeleteSQLString(string sqlstring)
        {

            try
            {
                SqlConnection objsqlconn = new SqlConnection(conString);
                objsqlconn.Open();
                SqlCommand objcmd = new SqlCommand(sqlstring, objsqlconn);
                objcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet FillManufacturer()
        {
            DataSet obj = new DataSet();
            string sql = "SELECT * from tbl_Manufacturer";
            obj = ExecuteSqlString(sql);
            return obj;

        }
    }
    // User class
    public class clsUser : clsDataLayer
    {

        // Validate Username and Password
        public DataSet ISValid(User user)
        {
            DataSet obj = new DataSet();
            string sql = "SELECT *  from tbl_Login where username='" + user.username + "' and password= '" + user.password + "'";
            obj = ExecuteSqlString(sql);
            return obj;

        }

        // Add new user on tlb_Login
        public void AddNewUser(User obj)
        {

            string sql = "INSERT INTO tbl_Login (FullName,Username,Password ,Email,IsAdmin,ContactNumber,Address)"
                        + "VALUES('" + obj.FullName + "','" + obj.username + "', '" + obj.password + "' , '" + obj.email + "', '" + obj.IsAdmin + "','"+obj.ContactNumber+"','"+obj.Address+"' )";

            InsertUpdateDeleteSQLString(sql);


        }
        // Update existing user
        public void UpdateUser(User obj)
        {

            string sql = " UPDATE  tbl_Login" +
                         " SET UserName='" + obj.username + "'," +
                         " Password='" + obj.password + "'," +
                         " Email='" + obj.email + "'," +
                         " IsAdmin='" + obj.IsAdmin + "'" +
                         "Where UserId='" + obj.UserId + "'";

            InsertUpdateDeleteSQLString(sql);

        }
        //Delete existing user
        public void DeleteUser(int userId)
        {
            User obj = new User();
            string sql = "Delete from tbl_Login where userId='" + userId + "'";
            InsertUpdateDeleteSQLString(sql);

        }
        // Load all users to DataGrid
        public DataSet LoadUser()
        {
            DataSet obj = new DataSet();
            string sql = "SELECT *,Case when IsAdmin=2 then 'Manager' when IsAdmin=1 then 'Employee' else 'Admin' end as UserType from tbl_Login order by userId";
            obj = ExecuteSqlString(sql);
            return obj;
        }

    }
    // Customer class
    public class clsCustomer : clsDataLayer
    {
        // Add new customer on tlb_Customer
        public void AddNewCustomer(Customer obj)
        {

            string sql = "INSERT INTO tbl_Customer (CustomerName,ContactNumber,Email ,Address)"
                        + "VALUES('" + obj.CustomerName + "','" + obj.ContactNumber + "', '" + obj.Email + "' , '" + obj.Address + "')";

            InsertUpdateDeleteSQLString(sql);
        }
        // Update existing customer
        public void UpdateCustomer(Customer obj)
        {

            string sql = " UPDATE  tbl_Customer" +
                         " SET CustomerName='" + obj.CustomerName + "'," +
                         " ContactNumber='" + obj.ContactNumber + "'," +
                         " Email='" + obj.Email + "'," +
                         " Address='" + obj.Address + "'" +
                         "Where CustomerId=" + obj.CustomerId + "";

            InsertUpdateDeleteSQLString(sql);

        }
        //Delete existing Customer
        public void DeleteCustomer(int userId)
        {
            User obj = new User();
            string sql = "Delete from tbl_Customer where CustomerId='" + userId + "'";
            InsertUpdateDeleteSQLString(sql);

        }
        // Load all Customers to DataGrid
        public DataSet LoadCustomer()
        {
            DataSet obj = new DataSet();
            string sql = "SELECT * from tbl_Customer order by CustomerId";
            obj = ExecuteSqlString(sql);
            return obj;
        }
    }
    //  Sales Class
    public class clsOrderes : clsDataLayer
    {

        // Add new Order to OrderDetails table.
        public void AddNewOrder(OrderDetails obj)
        {

            string sql = "INSERT INTO OrderDetails" +
                         " (ProductId,Quantity ,UnitPrice,TotalPrice,CustomerId,OrderBy )" +
                         " VALUES( '" + obj.ProductId + "' , "
                                     + obj.Quantity + ", " + obj.UnitPrice + ","
                                     + obj.TotalPrice + ", " + obj.CustomerId + "," + obj.OrderBy + " )";

            InsertUpdateDeleteSQLString(sql);
            UpdateProductDetail(obj);
        }
        // add new sales information to productDetail tables.
        public void AddNewProductDetail(Product obj)
        {

            string sql = "INSERT INTO tbl_ProductDetail" +
                         " (ProductName,Descreption,ProductImage ,ManufId,Quantity,UnitCost,CurrentCost)" +
                         " VALUES('" + obj.ProductName + "', '" + obj.Descreption + "' , '"
                         + obj.ProductImage + "', " + obj.ManufId + "," + obj.Quantity + ", " + obj.UnitCost + "," + obj.TotalCost + " )";
            DataTable dt = ExecuteSqlString("select Max(ProductId) ProductId FROM tbl_ProductDetail").Tables[0];
            string transaction = "insert into tbl_Transaction(TransactionType,CreatedBy,ProductId,Quantity,UnitPrice,TotalPrice)" +
                "values(2," + obj.CreatedBy + "," + int.Parse("0" + dt.Rows[0]["ProductId"]) + "," + obj.Quantity + "," + obj.UnitCost + "," + obj.TotalCost + ")";

            InsertUpdateDeleteSQLString(sql);
            InsertUpdateDeleteSQLString(transaction);
        }
        // update Product information in product detail table
        public void UpdateProductDetail(OrderDetails obj)
        {
            string sql = " Update tbl_ProductDetail set Quantity=(Quantity-" + obj.Quantity + "),CurrentCost=(UnitCost*(Quantity-" + obj.Quantity + ")),ModifiedDate=GETDATE() where ProductId=" + obj.ProductId;
            string transaction = "insert into tbl_Transaction(TransactionType,CreatedBy,ProductId,Quantity,UnitPrice,TotalPrice)" +
                "values(1," + obj.OrderBy + "," + obj.ProductId + "," + obj.Quantity + "," + obj.UnitPrice + "," + obj.TotalPrice + ")";
            InsertUpdateDeleteSQLString(sql);
            InsertUpdateDeleteSQLString(transaction);
        }

        // Load Order List  to DataGrid
        public DataSet LoadOrderList(int userId)
        {
            DataSet obj = new DataSet();
            string sql = string.Empty;
            if (userId > 0)
            {
                sql = "select O.ProductId,ProductName,Descreption,ProductImage,O.Quantity,O.UnitPrice,O.TotalPrice,C.CustomerName," +
                    " O.OrderId,O.UniqueId OrderNo, Convert(varchar, O.OrderDate,106) OrderDate,L.FullName UserName from OrderDetails O" +
                    " JOIN tbl_ProductDetail P  ON P.ProductId = O.ProductId" +
                    " JOIN tbl_Customer C ON C.CustomerId = O.CustomerId" +
                    " JOIN tbl_Login L ON L.UserId = O.OrderBy" +
                    " where O.OrderBy = " + userId;
            }
            else
            {
                sql = "select O.ProductId,ProductName,Descreption,ProductImage,O.Quantity,O.UnitPrice,O.TotalPrice,C.CustomerName," +
                    " O.OrderId,O.UniqueId OrderNo, Convert(varchar, O.OrderDate,106) OrderDate,L.FullName UserName from OrderDetails O" +
                    " JOIN tbl_ProductDetail P  ON P.ProductId = O.ProductId" +
                    " JOIN tbl_Customer C ON C.CustomerId = O.CustomerId" +
                    " JOIN tbl_Login L ON L.UserId = O.OrderBy";
            }
            obj = ExecuteSqlString(sql);
            return obj;
        }
        // Load LoadTransactionList List  to DataGrid
        public DataSet LoadTransactionList(int userId)
        {
            DataSet obj = new DataSet();
            string sql = string.Empty;
            if (userId > 0)
            {
                sql = "select T.TransactionId,ProductName,Descreption,ProductImage,T.Quantity,T.UnitPrice,T.TotalPrice," +
                      " case When T.TransactionType = 1 then 'Credit' else 'Debit' end TransactionType, T.TransactionNumber," +
                      " Convert(varchar, T.CreatedDate,106) TransactionDate,L.FullName UserName from tbl_Transaction T" +
                      " JOIN tbl_ProductDetail P  ON P.ProductId = T.ProductId" +
                      " JOIN tbl_Login L ON L.UserId = T.CreatedBy where T.CreatedBy = " + userId;
            }
            else
            {
                sql = "select T.TransactionId,ProductName,Descreption,ProductImage,T.Quantity,T.UnitPrice,T.TotalPrice," +
                      " case When T.TransactionType = 1 then 'Credit' else 'Debit' end TransactionType, T.TransactionNumber," +
                      " Convert(varchar, T.CreatedDate,106) TransactionDate,L.FullName UserName from tbl_Transaction T" +
                      " JOIN tbl_ProductDetail P  ON P.ProductId = T.ProductId" +
                      " JOIN tbl_Login L ON L.UserId = T.CreatedBy";
            }
            obj = ExecuteSqlString(sql);
            return obj;
        }
        public DataSet LoadProduct(int productId)
        {
            DataSet obj = new DataSet();
            string sql = string.Empty;
            if (productId > 0)
            {
                sql = "select ProductId,ProductName,Descreption,ProductImage,ManufId,Quantity,UnitCost,CurrentCost,CreatedDate,ModifiedDate," +
                    "M.ManufacturerName from  tbl_ProductDetail P JOIN tbl_Manufacturer M ON M.ManufacturerID = P.ManufId where ProductId =" + productId;
            }
            else
            {
                sql = "select ProductId,ProductName,Descreption,ProductImage,ManufId,Quantity,UnitCost,CurrentCost,CreatedDate,ModifiedDate," +
                    "M.ManufacturerName from  tbl_ProductDetail P JOIN tbl_Manufacturer M ON M.ManufacturerID = P.ManufId ";
            }
            obj = ExecuteSqlString(sql);
            return obj;
        }
        public void DeleteProduct(Product obj)
        {
            InsertUpdateDeleteSQLString("delete from tbl_ProductDetail where ProductId=" + obj.ProductId);
        }
        public void UpdateProduct(Product obj)
        {
            InsertUpdateDeleteSQLString("update tbl_ProductDetail set ProductName='" + obj.ProductName + "',ManufId=" + obj.ManufId + ",Descreption='" + obj.Descreption + "'" +
                ",ProductImage='" + obj.ProductImage + "',Quantity=" + obj.Quantity + ",UnitCost=" + obj.UnitCost + ",CurrentCost=" + obj.TotalCost + ",ModifiedDate=getdate()  where ProductId=" + obj.ProductId);
            string transaction = "insert into tbl_Transaction(TransactionType,CreatedBy,ProductId,Quantity,UnitPrice,TotalPrice)" +
                "values(2," + obj.CreatedBy + "," + obj.ProductId + "," + obj.Quantity + "," + obj.UnitCost + "," + obj.TotalCost + ")";
            InsertUpdateDeleteSQLString(transaction);
        }
    }

    public class clsManufacturer : clsDataLayer
    {
        // Add new Manufacturer to sales table.
        public void AddNewManufacturer(Manufacturer obj)
        {

            string sql = "INSERT INTO tbl_Manufacturer" +
                         " (ManufacturerName,Email ,PhoneNumber,Address)" +
                         " VALUES('" + obj.ManufacturerName + "' , '" + obj.Email + "', '" + obj.PhoneNumber + "','" + obj.Address + "')";

            InsertUpdateDeleteSQLString(sql);

        }

        // Update existing Manufacturer
        public void UpdateManufacturer(Manufacturer obj)
        {

            string sql = " UPDATE  tbl_Manufacturer" +
                         " SET ManufacturerName='" + obj.ManufacturerName + "'," +
                         " Address='" + obj.Address + "'," +
                         " PhoneNumber='" + obj.PhoneNumber + "'," +
                         " Email='" + obj.Email + "'" +
                         " Where ManufacturerID='" + obj.ManufacturerID + "'";

            InsertUpdateDeleteSQLString(sql);

        }

        //Delete existing Manufacturer
        public void DeleteManufacturer(string manufacturerId)
        {
            Manufacturer obj = new Manufacturer();
            string sql = "Delete from tbl_Manufacturer where ManufacturerID='" + manufacturerId + "'";
            InsertUpdateDeleteSQLString(sql);

        }

        // Load all Manufacturer to DataGrid
        public DataSet LoadManufacturer()
        {
            DataSet obj = new DataSet();
            string sql = "SELECT * from tbl_Manufacturer order by ManufacturerName";
            obj = ExecuteSqlString(sql);
            return obj;
        }
    }

    public class clsMedicine : clsDataLayer
    {
        public void AddNewMedicine(Medicine obj)
        {

            string sql = "INSERT INTO tbl_MedicineDetail" +
                         " (MedicineName,Description,ManufacturerId,Amount,MedicineImagePath,CreatedDate,CategoryId)" +
                         " VALUES('" + obj.MedicineName + "', '" + obj.MedicineDescription + "' , " + obj.ManufId + "," + obj.Price + ", '" +obj.MedicineImage+"' , " +"GETDATE(),"+int.Parse("0"+obj.CategoryId)+" )";
            
            InsertUpdateDeleteSQLString(sql);
        }

        public void UpdateMedicineDetail(Medicine obj)
        {

            string sql = " UPDATE  tbl_MedicineDetail" +
                         " SET MedicineName='" + obj.MedicineName + "'," +
                         " Description='" + obj.MedicineDescription + "'," +
                         " ManufacturerId='" + obj.ManufId + "'," +
                         " CategoryId='" + obj.CategoryId + "'," +
                         " Amount='" + obj.Price + "'," +
                         " MedicineImagePath='" + obj.MedicineImage + "'," +
                         " ModifiedDate= GETDATE()" +
                         "Where MedicineId=" + obj.MedicineId + "";

            InsertUpdateDeleteSQLString(sql);

        }

        public DataSet LoadMedicine(int medicineId)
        {
            DataSet obj = new DataSet();
            string sql = string.Empty;
            if (medicineId > 0)
            {
                sql = "select P.*,C.*," +
                    "M.ManufacturerName from  tbl_MedicineDetail P JOIN tbl_Manufacturer M ON M.ManufacturerID = P.ManufacturerId Join tbl_Category C ON C.CategoryId=P.CategoryId where MedicineId =" + medicineId;
            }
            else
            {
                sql = "select P.*,C.*," +
                    "M.ManufacturerName from  tbl_MedicineDetail P JOIN tbl_Manufacturer M ON M.ManufacturerID = P.ManufacturerId Join tbl_Category C ON C.CategoryId=P.CategoryId  ";
            }
            obj = ExecuteSqlString(sql);
            return obj;
        }

        public int MakePayment(string query)
        {
            SqlConnection objsqlconn = new SqlConnection(conString);
            objsqlconn.Open();
            SqlCommand objcmd = new SqlCommand(query, objsqlconn);
            int k=objcmd.ExecuteNonQuery();
            return k;
        }
        public DataSet PaymentReport(string query)
        {
            SqlConnection objsqlconn = new SqlConnection(conString);
            objsqlconn.Open();

            DataSet Ds = new DataSet();
            SqlCommand objcmd = new SqlCommand(query, objsqlconn);
            SqlDataAdapter objAdp = new SqlDataAdapter(objcmd);
            objAdp.Fill(Ds);
            return Ds;
        }
    }

    public class clsCategory : clsDataLayer
    {
        // Add new Category.
        public void AddNewCategory(Category obj)
        {

            string sql = "INSERT INTO tbl_Category" +
                         " (CategoryName,Description)" +
                         " VALUES('" + obj.CategoryName + "' , '" + obj.Description + "')";

            InsertUpdateDeleteSQLString(sql);

        }

        // Update existing Category
        public void UpdateCategory(Category obj)
        {

            string sql = " UPDATE  tbl_Category" +
                         " SET CategoryName='" + obj.CategoryName + "'," +
                         " Description='" + obj.Description + "'" +
                         " Where CategoryId='" + obj.CategoryId + "'";

            InsertUpdateDeleteSQLString(sql);

        }

        //Delete existing Category
        public void DeleteCategory(string categoryId)
        {
            Category obj = new Category();
            string sql = "Delete from tbl_Category where CategoryId='" + categoryId + "'";
            InsertUpdateDeleteSQLString(sql);

        }

        // Load all Manufacturer to DataGrid
        public DataSet LoadCategory()
        {
            DataSet obj = new DataSet();
            string sql = "SELECT * from tbl_Category order by CategoryName";
            obj = ExecuteSqlString(sql);
            return obj;
        }
    }
}

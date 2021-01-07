using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1
{
    class QueryForSQLServer
    {
        #region product     
        public static DataTable GetProducts()
        {
            //const string GetProductsQuery = "sp_GetProducts";
            const string GetProductsQuery = "select * from Product join Category on Product.catid = Category.id";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery);
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static DataTable GetProductsByImage(string image)
        {
            //const string GetProductsQuery = "sp_GetProducts";
            const string GetProductsQuery = "select id from Product where cast(Image as nvarchar) = @image";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery,
                    new SqlParameter { ParameterName = "@image", Value = image });
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static int InsertProduct(Product product)
        {
            const string Query = "insert into Product(CatId,Author,Name,Price,Quantity,Description,Image) " +
                "values(@catid,@Author,@name,@price,@quantity,@des,@image)";
            DataTable dt = null;
            int id = 0;
            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@catid", Value = product.CatId },
                    new SqlParameter { ParameterName = "@Author", Value = product.Author },
                    new SqlParameter { ParameterName = "@name", Value = product.Name },
                    new SqlParameter { ParameterName = "@price", Value = product.Price },
                    new SqlParameter { ParameterName = "@quantity", Value = product.Quantity },
                    new SqlParameter { ParameterName = "@des", Value = product.Description },
                    new SqlParameter { ParameterName = "@image", Value = product.Image }
                    );
                dt = p.ExcecuteQuery(CommandType.Text, "select  cast(@@IDENTITY as int)");
                id = (int)dt.Rows[0].ItemArray[0];
                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return id;
        }
        public static DataTable DeleteProduct(int id)
        {
            const string Query = "delete Product where id = @id";
            DataTable dt = null;
            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@id", Value = id }
                    );
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static DataTable UpdateProduct(Product product)
        {
            string Query = "update Product " +
                "set CatId = "+ product.CatId + ",Author = N'" + product.Author + "',Name = N'" + product.Name + 
                "',Price = " + product.Price + ",Quantity=" + product.Quantity + ",Description=N'" + product.Description + "',Image='" + product.Image+ "' " +
                "where Id = " + product.Id ;
            DataTable dt = null;
            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query);
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }
        #endregion

        #region Category
        public static DataTable GetCategory()
        {
            const string GetProductsQuery = "select * from Category ";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery);
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static DataTable GetCategoryByName(string name)
        {
            const string GetProductsQuery = "select * from Category where cast(name as nvarchar(50))= @name";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery,
                    new SqlParameter { ParameterName = "@name", Value = name });
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static int InsertCategory(Category product)
        {
            const string Query = "insert into Category(Name) " +
                "values(@name)";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, "select * from Category where cast(name as nvarchar(50))= @name",
                    new SqlParameter { ParameterName = "@name", Value = product.Name }
                    );
                if(dt.Rows.Count !=0)
                    id = (int)dt.Rows[0].ItemArray[0];
                if(id == -1)
                {
                    p.ExcecuteQuery(CommandType.Text, Query,
                        new SqlParameter { ParameterName = "@name", Value = product.Name }
                        );
                    dt = p.ExcecuteQuery(CommandType.Text, "select * from Category where cast(name as nvarchar(50))= @name",
                    new SqlParameter { ParameterName = "@name", Value = product.Name }
                    );
                    if (dt.Rows.Count != 0)
                        id = (int)dt.Rows[0].ItemArray[0];
                }   
                    
                
                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }
        public static void DeleteCategory(string name)
        {
            const string Query = "delete Category where cast(name as nvarchar(50)) = @name";
            Provider p = new Provider();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@name", Value = name }
                    );
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
        }
        public static void UpdateCategory(Category Category)
        {
            string Query = "update Category " +
                "set Name = N'" + Category.Name+"' where id = "+ Category.Id ;
             
            Provider p = new Provider();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query);
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
        }
        #endregion

        #region Product_image
        public static DataTable GetProducts_Image(int id)
        {
            const string GetProductsQuery = "select * from Product_Images where Productid = @id";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery,
                    new SqlParameter { ParameterName = "@id", Value = id });
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static int GetProducts_ImageMaxId(int id)
        {
            const string GetProductsQuery = "select IsNULL(max(id),-2) from Product_Images where ProductId = @id";
            DataTable dt = null;
            int index;
            Provider p = new Provider();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery,
                    new SqlParameter { ParameterName = "@id", Value = id });
                if ((int)dt.Rows[0].ItemArray[0] == -2)
                {
                    return 0;
                }
                index = (int) dt.Rows[0][0];
                return index;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }

        public static int InsertProduct_Image(Product_Images product_Images)
        {
            const string Query = "insert into Product_Images(Id,ProductId,Name) values(@Id,@ProductId,@Name)";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();

                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@Id", Value = product_Images.id },
                    new SqlParameter { ParameterName = "@ProductId", Value = product_Images.ProductId },
                    new SqlParameter { ParameterName = "@Name", Value = product_Images.Name }
                    );
                dt = p.ExcecuteQuery(CommandType.Text, "select  @@IDENTITY");
                //id = (int)dt.Rows[0].ItemArray[0];


                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }

        public static void DeleteProduct_Image(int id)
        {
            const string Query = "delete Product_Images where ProductId = @id";
            Provider p = new Provider();
            try
            {
                //DataTable dt;
                p.Connect();

                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@id", Value = id }
                    );
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return ;
        }
        #endregion

        #region PurchaseDetail
        public static DataTable GetPurchaseDetail(int p_id)
        {
            const string GetProductsQuery = "select pd.Product_ID, p.Name, pd.Quantity, pd.Price, pd.Total, pd.PurchaseDetail_ID from PurchaseDetail pd join Product p on pd.Product_ID = p.Id where Purchase_ID=@p_id";
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery,
                    new SqlParameter { ParameterName = "@p_id" , Value = p_id });
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }
        public static int InsertPurchaseDetail(PurchaseDetail purchaseDetail)
        {
            const string Query = "insert into PurchaseDetail(Purchase_ID,Product_ID,Price,Quantity,Total) " +
                "values(@Purchase_ID, @Product_ID, @Price, @Quantity, @Total)";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@Purchase_ID", Value = purchaseDetail.Purchase_ID },
                    new SqlParameter { ParameterName = "@Product_ID", Value = purchaseDetail.Product_ID },
                    new SqlParameter { ParameterName = "@Price", Value = purchaseDetail.Price },
                    new SqlParameter { ParameterName = "@Quantity", Value = purchaseDetail.Quantity },
                    new SqlParameter { ParameterName = "@Total", Value = purchaseDetail.Total }
                    );

                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }

        public static int UpdatePurchaseDetail(PurchaseDetail purchaseDetail)
        {
            const string Query = 
                "update PurchaseDetail set Quantity = @Quantity, Total = @Total where PurchaseDetail_ID = @PurchaseDetail_ID";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@PurchaseDetail_ID", Value = purchaseDetail.PurchaseDetail_ID },
                    new SqlParameter { ParameterName = "@Quantity", Value = purchaseDetail.Quantity },
                    new SqlParameter { ParameterName = "@Total", Value = purchaseDetail.Total }
                    );

                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }

        public static int DeletePurchaseDetail(int PurchaseDetail_ID)
        {
            const string Query =
                "delete PurchaseDetail where PurchaseDetail_ID = @PurchaseDetail_ID";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@PurchaseDetail_ID", Value = PurchaseDetail_ID }
                    );

                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return 0;
        }
        #endregion

        #region Purchase
        public static DataTable GetPurchase()
        {
            const string GetProductsQuery = "select * from Purchase p join Customer c on p.Customer_Tel = c.Tel";// join PurchaseDetail pd on pd.Purchase_ID = p.Purchase_ID"; p.Purchase_ID,p.Total,Created_At,Customer_Name,Tel
            DataTable dt = null;

            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, GetProductsQuery);
                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return null;
        }

        public static int InsertPurchase(Purchase purchase)
        {
            const string Query = "sp_InsertPurchase";
            Provider p = new Provider();
            int? id = -1;
            try
            {
                p.Connect();
                id = p.ExcecuteNonQuery(CommandType.StoredProcedure, Query,
                    new SqlParameter { ParameterName = "@Customer_Tel", Value = purchase.Customer_Tel },
                    new SqlParameter { ParameterName = "@Created_At", Value = purchase.Created_At },
                    new SqlParameter { ParameterName = "@Total", Value = purchase.Total },
                    new SqlParameter { ParameterName = "@Status", Value = purchase.Status },
                    new SqlParameter { ParameterName = "@id", Value = id,Direction = ParameterDirection.Output }

                    );
                if (id != null)
                {
                    return (int)id;
                }
                return -1;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return -1;
        }
        public static void DeletePurchase(int id)
        {
            const string Query = "delete Purchase where  Purchase_ID = @id";
            Provider p = new Provider();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@id", Value = id }
                    );
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
        }
        public static void UpdatePurchase(Category Category)
        {
            string Query = "update Category " +
                "set Name = N'" + Category.Name + "' where id = " + Category.Id;

            Provider p = new Provider();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query);
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
        }
        #endregion

        #region Customer   
        public static DataTable GetCustomerByTel(string tel)
        {
            const string Query = "select * from Customer where tel = @tel";
            Provider p = new Provider();
            DataTable dt=null;
            try
            {
                
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@tel", Value = tel }
                    );

                return dt;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return dt;
        }
        public static int InsertCustomer(Customer customer)
        {
            const string Query = "insert into Customer(Customer_Name,Tel) " +
                "values(@name,@tel)";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@name", Value = customer.Customer_Name },
                    new SqlParameter { ParameterName = "@Tel", Value = customer.Tel }
                    );

                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return -1;
        }

        public static int UpdateCustomer(Customer customer)
        {
            const string Query = "update Customer set Customer_Name = @name, Address = @Address, Email = @Email where Tel = @Tel";
            Provider p = new Provider();
            int id = -1;
            try
            {
                DataTable dt;
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@name", Value = customer.Customer_Name },
                    new SqlParameter { ParameterName = "@Tel", Value = customer.Tel },
                    new SqlParameter { ParameterName = "@Address", Value = customer.Address },
                    new SqlParameter { ParameterName = "@Email", Value = customer.Email }
                    );

                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            finally
            {
                p.DisConnect();
            }
            return -1;
        }
        #endregion

        #region logic
        public static ObservableCollection<Product> GetProductFromDb(int catId = 0)
        {
            DataTable data = null;
            var products = new ObservableCollection<Product>();


            data = QueryForSQLServer.GetProducts();
            foreach (DataRow row in data.Rows)
            {
                var product = new Product();
                product.Id = (int)row.ItemArray[0];
                product.CatId = (int)row.ItemArray[1];
                product.Name = (string)row.ItemArray[2];
                product.Price = (Decimal)row.ItemArray[3];
                product.Quantity = (int)row.ItemArray[4];
                product.Description = (string)row.ItemArray[5];
                product.Image = (string)row.ItemArray[6];
                product.Author = (string)row.ItemArray[7];
                product.Product_Images = new List<Product_Images>();
                products.Add(product);
            }
            if (catId != 0)
            {
                var productFillered = from product in products
                                      where product.CatId == catId
                                      select product;
                products = new ObservableCollection<Product>(productFillered);
            }

            return products;
        }

        public static void UpdateListPurchaseDetail(ObservableCollection<object> list_Update)
        {
            
            for (int i = 0; i < list_Update.Count; i++)
            {
                dynamic p = list_Update[i];
                Type typeOfDynamic = p.GetType();
                bool exist = typeOfDynamic.GetProperties().Where(x => x.Name.Equals("PurchaseDetail_ID")).Any();
                if (exist)
                {
                    var update_item = new PurchaseDetail
                    {
                        Total = p.Quantity * p.Unit_Price,
                        Quantity = p.Quantity,
                        PurchaseDetail_ID = p.PurchaseDetail_ID
                    };
                    UpdatePurchaseDetail(update_item);
                }
            }

        }

        public static void InsertListPurchaseDetail(ObservableCollection<object> list_New,int current_id = 1)
        {
            //List<PurchaseDetail> listPurchaseDetails = new List<PurchaseDetail>();
            for (int i = 0; i < list_New.Count; i++)
            {
                dynamic p = list_New[i];
                Type typeOfDynamic = p.GetType();
                bool exist = typeOfDynamic.GetProperties().Where(x => x.Name.Equals("PurchaseDetail_ID")).Any();
                if (!exist)
                {
                    var new_item = new PurchaseDetail
                    {
                        Purchase_ID = current_id,
                        Total = p.SubTotal,
                        Quantity = p.Quantity,
                        Product_ID = p.Product_ID,
                        Price = p.Unit_Price
                    };
                    InsertPurchaseDetail(new_item);
                }
            }

        }

        public static void DeleteListPurchaseDetail(ObservableCollection<object> list_Delete, int current_id = 1)
        {
            for (int i = 0; i < list_Delete.Count; i++)
            {
                dynamic p = list_Delete[i];
                Type typeOfDynamic = p.GetType();
                bool exist = typeOfDynamic.GetProperties().Where(x => x.Name.Equals("PurchaseDetail_ID")).Any();
                if (exist)
                {
                    DeletePurchaseDetail((int)p.PurchaseDetail_ID);
                }
            }
        }
        #endregion
    }
}

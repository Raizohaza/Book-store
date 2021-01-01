using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.Provider
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
                DataTable dt;
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

        #endregion

        #region Purchase

        #endregion
    }
}

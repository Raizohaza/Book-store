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

        public static DataTable InsertProduct(Product product)
        {
            const string Query = "insert into Product(CatId,SKU,Name,Price,Quantity,Description,Image) " +
                "values(@catid,@sku,@name,@price,@quantity,@des,@image)";
            DataTable dt = null;
            Provider p = new Provider();
            var products = new ObservableCollection<Product>();
            try
            {
                p.Connect();
                dt = p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@catid", Value = product.CatId },
                    new SqlParameter { ParameterName = "@sku", Value = product.SKU },
                    new SqlParameter { ParameterName = "@name", Value = product.Name },
                    new SqlParameter { ParameterName = "@price", Value = product.Price },
                    new SqlParameter { ParameterName = "@quantity", Value = product.Quantity },
                    new SqlParameter { ParameterName = "@des", Value = product.Description },
                    new SqlParameter { ParameterName = "@image", Value = product.Image }
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
                "set CatId = "+ product.CatId + ",SKU = '"+ product.SKU + "',Name = '" + product.Name + 
                "',Price = " + product.Price + ",Quantity=" + product.Quantity + ",Description='" + product.Description + "',Image='be1abedd-c60c-409b-ac31-38d62f696a68' " +
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
        public static void InsertCategory(Category product)
        {
            const string Query = "insert into Category(Name) " +
                "values(@name)";
            Provider p = new Provider();
            try
            {
                p.Connect();
                p.ExcecuteQuery(CommandType.Text, Query,
                    new SqlParameter { ParameterName = "@name", Value = product.Name }
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
        public static void DeleteCategory(int id)
        {
            const string Query = "delete Category where id = @id";
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
        public static void UpdateCategory(Category Category)
        {
            string Query = "update Category " +
                "set Name = '" + Category.Name+"' where id = "+ Category.Id ;
             
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
    }
}

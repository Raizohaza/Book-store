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
    class Provider
    {
        string ConnectionString { get; set; }
        Provider(string str)
        {
            ConnectionString = str;
        }
        SqlConnection Connection { get; set; }
        public void Connect()
        {
            try
            {
                if (Connection == null)
                    Connection = new SqlConnection(ConnectionString);
                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();
                Connection.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void DisConnect()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public int ExcecuteNonQuery(CommandType cmtType, string sqlSQL,
                        params SqlParameter[] parameters)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = sqlSQL;
                command.CommandType = cmtType;
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                int nRow = command.ExecuteNonQuery();
                return nRow;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable ExcecuteQuery(CommandType cmtType, string strSQL,
                    params SqlParameter[] parameters)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = cmtType;
                command.CommandText = strSQL;
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public static DataTable GetProducts(string ConnectionString)
        {
            //const string GetProductsQuery = "sp_GetProducts";
            const string GetProductsQuery = "select * from Product join Category on Product.catid = Category.id";
            DataTable dt = null;
            Provider p = new Provider(ConnectionString);

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
    }
}

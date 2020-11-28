using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using provider = DoAn1.Provider;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageHome : Page
    {
        public PageHome()
        {
            this.InitializeComponent();
            DataTable data = null;
            var products = new ObservableCollection<Product>();

            data = provider::QueryForSQLServer.GetProducts();
            foreach (DataRow row in data.Rows)
            {
                var product = new Product();
                product.Id = (int)row.ItemArray[0];
                product.CatId = (int)row.ItemArray[1];
                product.SKU = (string)row.ItemArray[2];
                product.Name = (string)row.ItemArray[3];
                product.Price = (Decimal)row.ItemArray[4];
                product.Quantity = (int)row.ItemArray[5];
                product.Description = (string)row.ItemArray[6];
                product.Image = (string)row.ItemArray[7];

                products.Add(product);
            }
            //var cat = new Category() { Id = 8, Name = "ThinkPad" };
            //provider::QueryForSQLServer.DeleteCategory(7);
            //provider::QueryForSQLServer.UpdateCategory(cat);
            test_data.ItemsSource = products;
        }
    }
}

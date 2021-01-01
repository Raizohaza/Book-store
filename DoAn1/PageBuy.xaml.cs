using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using provider = DoAn1.Provider;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageBuy : Page
    {
        ObservableCollection<Product> ProductData = new ObservableCollection<Product>();
        Purchase p = new Purchase();
        public ObservableCollection<Product> GetProductFromDb(int catId = 0)
        {
            DataTable data = null;
            var products = new ObservableCollection<Product>();

            data = provider::QueryForSQLServer.GetProducts();
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
        public PageBuy()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductData = GetProductFromDb();
            productsListView.ItemsSource = ProductData;
        }
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        ObservableCollection<object> list = new ObservableCollection<object>();
        private void productsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = productsListView.SelectedItem as Product;
                list.Add(item);
                //totalTextBlock
                selectedProductsListView.ItemsSource = list;
            }
            catch (Exception ex)
            {

                Debug.WriteLine( ex.ToString());
            }
        }

        private void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
        }
    }
}

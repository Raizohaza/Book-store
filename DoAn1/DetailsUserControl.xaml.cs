using System;
using System.Collections.Generic;
using System.Data;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DoAn1
{
    public sealed partial class DetailsUserControl : UserControl
    {
        public delegate void Save(Product productRef);
        public event Save Handler;

        public DetailsUserControl(Product product)
        {
            this.InitializeComponent();
            this.DataContext = product;

            List<Product_Images> img = new List<Product_Images>();
            DataTable images =  provider::QueryForSQLServer.GetProducts_Image(product.Id);
            
            foreach (DataRow item in images.Rows)
            {
                var Product_Images = new Product_Images()
                {
                    id = (int)item.ItemArray[0],
                    ProductId = (int)item.ItemArray[1],
                    Name = (string)item.ItemArray[2]
                };
                img.Add(Product_Images);
            }

            lvManyImg.ItemsSource = img;
            Handler?.Invoke(product);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

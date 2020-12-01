using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        public DetailsUserControl(Product product)
        {
            if (product !=null)
            {
                this.InitializeComponent();
                this.DataContext = product;             

                List<Product_Images> img = new List<Product_Images>();
                DataTable images = provider::QueryForSQLServer.GetProducts_Image(product.Id);

              
              
                

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

                //back
                SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
                manager.BackRequested += DetailPage_BackRequested;
                manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }

        #region//back


        private void DetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled.
            e.Handled = true;
            SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
            manager.BackRequested -= DetailPage_BackRequested;
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            // Use the "drill out" animation when navigating to the master page.
            this.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}

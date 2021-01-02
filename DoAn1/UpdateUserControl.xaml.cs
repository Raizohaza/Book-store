using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DoAn1
{
    public sealed partial class UpdateUserControl : UserControl
    {
        public delegate void Save(Product productRef);
        public event Save Handler;
        Product Product { get; set; }
        public UpdateUserControl(Product product)
        {
            this.InitializeComponent();
            Product = product;

            try
            {
                //Load Product_Images
                List<Product_Images> img = new List<Product_Images>();
                DataTable images = QueryForSQLServer.GetProducts_Image(Product.Id);

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
                Product.Product_Images = img;
                lvManyImg.ItemsSource = img;
            }
            catch (Exception ex)
            {

                Debug.WriteLine("ex: " + ex.Message);
            }
            
            

            this.DataContext = Product;
            var categoriesList = PageHome.GetCategoriesFromDb();
            cbbListType.ItemsSource = categoriesList;
            cbbListType.SelectedIndex = (int)product.CatId - 1;


            //back
            SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
            manager.BackRequested += DetailPage_BackRequested;
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        
        private async void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            //FileData 
            StorageFile openFile = await openPicker.PickSingleFileAsync();
            if (openFile != null)
            {
                var test = new Product { Image = openFile.Path };
                Product.Image = openFile.Path;
                this.DataContext = Product;
                avatarImg.DataContext = test;
            }
        }
        private async void btnAddProducImgs_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            //FileData 
            var openFile = await openPicker.PickMultipleFilesAsync();
            var test = new List<Product_Images>();

            if (openFile != null)
            {
                foreach (var item in openFile)
                {
                    var Product_Images = new Product_Images()
                    {
                        Name = item.Path
                    };
                    Product.Product_Images.Add(Product_Images);
                }
                lvManyImg.ItemsSource = Product.Product_Images;
            }
        }
        private void cbbListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbb = sender as ComboBox;
            var pd = cbb.SelectedItem as Category;
            Product.CatId = pd.Id;
        }

        private async void btnDone_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Save your change ???");

            messageDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            messageDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            var result = await messageDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                Product.Price = Decimal.Parse(addGia.Text);
                Product.Quantity = int.Parse(addSoLuong.Text);
                QueryForSQLServer.UpdateProduct(Product);

                //delete then insert
                QueryForSQLServer.DeleteProduct_Image(Product.Id);
                int id = 1;
                foreach (var item in Product.Product_Images)
                {
                    item.id = id;
                    item.ProductId = Product.Id;
                    QueryForSQLServer.InsertProduct_Image(item);
                    id++;
                }
                Handler?.Invoke(Product);
                //e.Handled = true;
                //SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
                //manager.BackRequested -= DetailPage_BackRequested;
                //manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                //this.Visibility = Visibility.Collapsed;
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

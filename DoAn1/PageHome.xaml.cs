using Syncfusion.XlsIO;
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
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
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
            if (catId !=0)
            {
                var productFillered = from product in products
                                      where product.CatId == catId
                                      select product;
                products = new ObservableCollection<Product>(productFillered);
            }
            
            return products;
        }
        public static ObservableCollection<Category> GetCategoriesFromDb()
        {
            var categoriesList = new ObservableCollection<Category>();
            DataTable q = provider::QueryForSQLServer.GetCategory();
            foreach (DataRow row in q.Rows)
            {
                var category = new Category { Id = (int)row.ItemArray[0], Name = (string)row.ItemArray[1] };
                categoriesList.Add(category);
            }
            return categoriesList;
        }
        public PageHome()
        {
            this.InitializeComponent();
            var categoriesList = GetCategoriesFromDb();
            
            test_data.ItemsSource = GetProductFromDb();
            cbbListType.ItemsSource = categoriesList;
        }
        #region//back
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as ObservableCollection<Product> != null)
            {
                var products = new ObservableCollection<Product>();
                products = (e.Parameter as ObservableCollection<Product>);
                searchFillter(products);
                base.OnNavigatedTo(e);

                var backStack = Frame.BackStack;
                SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
                manager.BackRequested += DetailPage_BackRequested;
                manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            base.OnNavigatedFrom(args);
 
            SystemNavigationManager manager = SystemNavigationManager.GetForCurrentView();
            manager.BackRequested -= DetailPage_BackRequested;
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
 
        private void DetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled.
            e.Handled = true;
 
            // Use the "drill out" animation when navigating to the master page.
            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }
        #endregion
        public void searchFillter(ObservableCollection<Product> products)
        {
            this.InitializeComponent();
            var categoriesList = new ObservableCollection<Category>();
            DataTable q = provider::QueryForSQLServer.GetCategory();
            foreach (DataRow row in q.Rows)
            {
                var category = new Category { Id = (int)row.ItemArray[0], Name = (string)row.ItemArray[1] };
                categoriesList.Add(category);
            }

            test_data.ItemsSource = products;
            cbbListType.ItemsSource = categoriesList;
        }

        public void Refresh()
        {
            cbbListType.SelectedIndex = -1;
            var categoriesList = GetCategoriesFromDb();
            cbbListType.ItemsSource = categoriesList;
            test_data.ItemsSource = GetProductFromDb();
        }

        //lay product tu rightapped
        private FrameworkElement originalSource;
        private void test_data_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            originalSource = (FrameworkElement)e.OriginalSource;

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = originalSource.DataContext as Product;

            var screen = new UpdateUserControl(item);
            screen.Handler += GetProductFromUC;
            GridHome.Children.Add(screen);
            
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var item = originalSource.DataContext as Product;
            provider::QueryForSQLServer.DeleteProduct(item.Id);

        }

        private void test_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = test_data.SelectedItem as Product;
            var index = test_data.SelectedIndex;
            var screen = new DetailsUserControl(item);
            GridHome.Children.Add(screen);
            //this.Visibility = Visibility.Collapsed;
        }

        private void GetProductFromUC(Product product)
        {        
            Refresh();
        }


        private void cbbListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // code khi chuyển thể loại here
            var test = sender as ComboBox;
            if (test.SelectedIndex !=-1)
            {
                Category category1 = (Category)test.SelectedItem;


                test_data.ItemsSource = GetProductFromDb(category1.Id);
            }
            
        }

        private async void cbbEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = originalSource.DataContext as Category;
            DataTable dt = provider::QueryForSQLServer.GetCategoryByName(item.Name);
            int id = (int)dt.Rows[0].ItemArray[0];
            TextBox input = new TextBox()
            {
                Height = (double)App.Current.Resources["TextControlThemeMinHeight"],
                PlaceholderText = "Display Text"
            };
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Change Category's Name",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "OK",
                SecondaryButtonText = "Cancel",
                Content = input
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                input = (TextBox)dialog.Content;
                if (input.Text != "")
                {
                    var cat = new Category() { Id=item.Id, Name = input.Text };
                    provider::QueryForSQLServer.UpdateCategory(cat);
                    await new Windows.UI.Popups.MessageDialog("Updated!").ShowAsync();
                    Refresh();
                }
                else
                    await new Windows.UI.Popups.MessageDialog("Nothing Change!").ShowAsync();
            }
        }
        private void cbbListType_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            originalSource = (FrameworkElement)e.OriginalSource;
        }

        private void cbbRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = originalSource.DataContext as Category;
            provider::QueryForSQLServer.DeleteCategory(item.Name);
            //reset cat
            
            Refresh();
        }

        private async void addCat_Click(object sender, RoutedEventArgs e)
        {
            TextBox input = new TextBox()
            {
                Height = (double)App.Current.Resources["TextControlThemeMinHeight"],
                PlaceholderText = "Display Text"
            };
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Change Category's Name",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "OK",
                SecondaryButtonText = "Cancel",
                Content = input
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                input = (TextBox)dialog.Content;
                if (input.Text != "")
                {
                    var cat = new Category() { Name = input.Text };
                    provider::QueryForSQLServer.InsertCategory(cat);
                    await new Windows.UI.Popups.MessageDialog("Success!").ShowAsync();
                    Refresh();
                }
                else
                    await new Windows.UI.Popups.MessageDialog("Noting change!").ShowAsync();

            }
        }
    }
}

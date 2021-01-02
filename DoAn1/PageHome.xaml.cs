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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageHome : Page
    {
        ObservableCollection<Product> ProductData = new ObservableCollection<Product>();

        //paging
        private ObservableCollection<Product> displayList = new ObservableCollection<Product>(); //List to be displayed in ListView
        int pageIndex = -1;
        int pageSize = 9; //Set the size of the page
        int totalPage = 1;
        string CurrentPage;


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex + 1 <= totalPage)
            {
                pageIndex++;
                var filter = ProductData.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                displayList = new ObservableCollection<Product>(filter);
                CurrentPage = (pageIndex + 1).ToString() + "/" + (totalPage + 1).ToString();
                pageInfo.DataContext = CurrentPage;
                test_data.ItemsSource = displayList;
            }
            
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex - 1 >= 0)
            {
                pageIndex--;
                var filter = ProductData.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                displayList = new ObservableCollection<Product>(filter);
                CurrentPage = (pageIndex+1).ToString() + "/" + (totalPage + 1).ToString();
                pageInfo.DataContext = CurrentPage;
                test_data.ItemsSource = displayList;
            }
        }

        private void PagingPage()
        {
            pageIndex = -1;
            totalPage = ProductData.Count() / pageSize;
            NextButton_Click(null, null);
            productTotalTb.DataContext = ProductData.Count();
        }

        public ObservableCollection<Product> GetProductFromDb(int catId = 0)
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
            DataTable q = QueryForSQLServer.GetCategory();
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

            ProductData = GetProductFromDb();
            PagingPage();
            cbbListType.ItemsSource = categoriesList;

            //Call NextButton_Click in page Constructor to show defalut 10 items

            var filter = new List<String>() { "A-Z", "Z-A", "Price ↓", "Price ↑" };
            Filter.ItemsSource = filter;
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
            DataTable q = QueryForSQLServer.GetCategory();
            foreach (DataRow row in q.Rows)
            {
                var category = new Category { Id = (int)row.ItemArray[0], Name = (string)row.ItemArray[1] };
                categoriesList.Add(category);
            }

            ProductData = products;
            PagingPage();
            cbbListType.ItemsSource = categoriesList;
        }

        public void Refresh()
        {
            cbbListType.SelectedIndex = -1;
            var categoriesList = GetCategoriesFromDb();
            cbbListType.ItemsSource = categoriesList;
            ProductData = GetProductFromDb();
            PagingPage();
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
            QueryForSQLServer.DeleteProduct(item.Id);
            Refresh();
        }

        private void test_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = test_data.SelectedItem as Product;
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

                ProductData = GetProductFromDb(category1.Id);
                PagingPage();
            }
            
        }

        private async void cbbEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = originalSource.DataContext as Category;
            DataTable dt = QueryForSQLServer.GetCategoryByName(item.Name);
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
                    QueryForSQLServer.UpdateCategory(cat);
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
            QueryForSQLServer.DeleteCategory(item.Name);
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
                    QueryForSQLServer.InsertCategory(cat);
                    await new Windows.UI.Popups.MessageDialog("Success!").ShowAsync();
                    Refresh();
                }
                else
                    await new Windows.UI.Popups.MessageDialog("Noting change!").ShowAsync();

            }
        }

        private void option_Click(object sender, RoutedEventArgs e)
        {
            var input = (FrameworkElement)e.OriginalSource;
            if (input.DataContext != null)
            {
                var choose = input.DataContext.ToString();
                //first option
                var empFiltered = ProductData.OrderBy(x => x.Name).ToList();

                switch (choose)
                {
                    case "A-Z":
                        break;
                    case "Z-A":
                        empFiltered = ProductData.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "Price ↓":
                        empFiltered = ProductData.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "Price ↑":
                        empFiltered = ProductData.OrderBy(x => x.Price).ToList();
                        break;
                    default:
                        break;
                }
                ProductData = new ObservableCollection<Product>(empFiltered);
                PagingPage();
            }
        }

        
        
    }
}                          
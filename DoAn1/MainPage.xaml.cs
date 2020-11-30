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
using muxcs = Microsoft.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Connection string for using Windows Authentication.
        //private string connectionString = @"Data Source=admin;Initial Catalog=MyStore;Integrated Security=True";

        // This is an example connection string for using SQL Server Authentication.
        // private string connectionString =
        //     @"Data Source=YourServerName\YourInstanceName;Initial Catalog=DatabaseName; User Id=XXXXX; Password=XXXXX";

        //public string ConnectionString { get => connectionString; set => connectionString = value; }

        public static Frame CFrame { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            CF.Navigate(typeof(PageHome));
        }
        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.SelectedItem = Menu.MenuItems[0];
        }

        private void Menu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            // dau tien check setting cho app
            if (args.IsSettingsSelected == true)
            {
                // neu co setting gi cho app thi code o day
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "iconHome":
                        CF.Navigate(typeof(PageHome));
                        break;
                    case "iconAdd":
                        CF.Navigate(typeof(PageAdd));
                        //CF.Navigated += On_Navigated;
                        break;
                    case "iconFav":
                        CF.Navigate(typeof(PageFav));
                        break;
                    case "iconBag":
                        CF.Navigate(typeof(PageBuy));
                        break;
                }
            }
        }

        private void NavViewSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // code tim kiem here
        }
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
        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var products = GetProductFromDb();
            var txtOrig = (sender as AutoSuggestBox).Text;
            string upper = txtOrig.ToUpper();
            var empFiltered = from Emp in products
                              let ename = Emp.Name.ToUpper()
                              where
                               ename.StartsWith(upper)
                               || ename.StartsWith(upper)
                               || ename.Contains(txtOrig.ToUpper())
                              select Emp;
            var tmp = empFiltered.ToList();
            products = new ObservableCollection<Product>(empFiltered);
            CF.Navigate(typeof(PageHome), products);
        }

        
    }
}

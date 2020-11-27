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
        public MainPage()
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
            var cat = new Category() { Id=8, Name = "ThinkPad" };
            //provider::QueryForSQLServer.DeleteCategory(7);
            provider::QueryForSQLServer.UpdateCategory(cat);
            test_data.ItemsSource = products;
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("iconHome", typeof(MainPage)),
            ("iconFav", typeof(PageFav)),
            ("iconAdd", typeof(PageAdd)),
            ("iconBag", typeof(PageBuy)),
        };

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.MenuItems.Add(new NavigationViewItemSeparator());
            Menu.MenuItems.Add(new NavigationViewItem
            {
                Content = "My content",
                Icon = new SymbolIcon((Symbol)0xF1AD),
                Tag = "content"
            });
            _pages.Add(("content", typeof(PageContent)));          
            
            ContentFrame.Navigated += On_Navigated;

           
            Menu.SelectedItem = Menu.MenuItems[0];
            
            NavView_Navigate("iconHome", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());

            var goBack = new KeyboardAccelerator { Key = Windows.System.VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

           
            var altLeft = new KeyboardAccelerator
            {
                Key = Windows.System.VirtualKey.Left,
                Modifiers = Windows.System.VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void Menu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void Menu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(
        string navItemTag,
        Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(PageSettings);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void BackInvoked(KeyboardAccelerator sender,
                         KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (Menu.IsPaneOpen &&
                (Menu.DisplayMode == NavigationViewDisplayMode.Compact ||
                 Menu.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }



        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            Menu.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(PageSettings))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                Menu.SelectedItem = (NavigationViewItem)Menu.SettingsItem;
                Menu.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                Menu.SelectedItem = Menu.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                Menu.Header =
                    ((NavigationViewItem)Menu.SelectedItem)?.Content?.ToString();
            }
        }
    }
}

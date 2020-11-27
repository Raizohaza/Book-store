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

           
        }

      
        private void Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(PageSettings));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "iconHome":
                    ContentFrame.Navigate(typeof(PageHome));
                    break;

                case "iconAdd":
                    ContentFrame.Navigate(typeof(PageAdd));
                    break;

                case "iconFav":
                    ContentFrame.Navigate(typeof(PageFav));
                    break;

                case "iconBag":
                    ContentFrame.Navigate(typeof(PageBuy));
                    break;            
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(PageHome));
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
                switch(item.Tag.ToString())
                {
                    case "iconHome":
                        ContentFrame.Navigate(typeof(PageHome));
                        break;
                    case "iconAdd":
                        ContentFrame.Navigate(typeof(PageAdd));
                        break;
                    case "iconFav":
                        ContentFrame.Navigate(typeof(PageFav));
                        break;
                    case "iconBag":
                        ContentFrame.Navigate(typeof(PageBuy));
                        break;
                }    

            }    
        }
    }
}

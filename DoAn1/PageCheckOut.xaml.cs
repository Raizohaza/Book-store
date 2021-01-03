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
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageCheckOut : Page
    {
        public class Purchase_ALL
        {
            public string Customer_Name;
            public string Tel;
            public decimal Total;
            public DateTime Created_At;
        };
        public PageCheckOut()
        {
            this.InitializeComponent();
            var dt = QueryForSQLServer.GetPurchase();



          
            ObservableCollection<object> allPurchase = new ObservableCollection<object>();
            foreach (DataRow item in dt.Rows)
            {
                var _p = new 
                {
                    Purchase_ID =(int) item["Purchase_ID"],
                    Customer_Name = item["Customer_Name"] as string,
                    Tel = item["Tel"] as string,
                    Total = (decimal)item["Total"],
                    Created_At = (DateTime)(item["Created_At"])
                };
                allPurchase.Add(_p);
            }
            purchaseDataGrid.ItemsSource = allPurchase;
        }

        private void editPurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void purchaseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (sender as DataGrid).SelectedItem;
            ObservableCollection<object> allPurchaseDetail = new ObservableCollection<object>();
            int id = (int)test?.GetType().GetProperty("Purchase_ID")?.GetValue(test, null);
            var dt = QueryForSQLServer.GetPurchaseDetail(id);
            foreach (DataRow item in dt.Rows)
            {
                var _p = new
                {
                    Name = item["Name"],
                    Price = item["Price"],
                    Quantity = item["Quantity"],
                    Total = item["Total"]
                };
                allPurchaseDetail.Add(_p);
            }
            DataGridDetailPurchase.ItemsSource = allPurchaseDetail;
        }
    }
}

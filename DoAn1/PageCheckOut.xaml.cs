using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            public int Purchase_ID;
            public string Customer_Name;
            public string Tel;
            public decimal Total;
            public string Created_At;
            public PurchaseStatus Status;
        };
        ObservableCollection<object> allPurchase = new ObservableCollection<object>();
        void reLoadData()
        {
            var dt = QueryForSQLServer.GetPurchase();
            allPurchase = new ObservableCollection<object>();
            //purchase
            foreach (DataRow item in dt.Rows)
            {
                string formatted = ((DateTime)item["Created_At"]).ToString("dd/MM/yyyy");
                var _p = new
                {
                    Purchase_ID = (int)item["Purchase_ID"],
                    Customer_Name = item["Customer_Name"] as string,
                    Tel = item["Tel"] as string,
                    Total = (decimal)item["Total"],
                    Created_At = formatted,
                    Status = (PurchaseStatus)(item["Status"])
                };
                allPurchase.Add(_p);
            }
            purchaseDataGrid.ItemsSource = allPurchase;
        }
        public PageCheckOut()
        {
            this.InitializeComponent();


            //date
            fromDatePicker.Date = DateTime.Today.AddDays(-3).Date;
            toDatePicker.Date = DateTime.Today.Date;

            var _enumval = Enum.GetValues(typeof(PurchaseStatus)).Cast<PurchaseStatus>();
            purchaseStatesComboBox.ItemsSource = _enumval.ToList();
            purchaseStatesComboBox.SelectedIndex = 4;

            reLoadData();
        }

        private void editPurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var Selecteditem = (sender as FrameworkElement).DataContext;
            ObservableCollection<object> allPurchaseDetail = new ObservableCollection<object>();
            int id = (int)Selecteditem?.GetType().GetProperty("Purchase_ID")?.GetValue(Selecteditem, null);
            var dt = QueryForSQLServer.GetPurchaseDetail(id);
            foreach (DataRow item in dt.Rows)
            {
                var _p = new
                {
                    Name = item["Name"],
                    //Price = item["Price"],
                    Quantity = item["Quantity"],
                    Total = item["Total"]
                };
                allPurchaseDetail.Add(_p);
            }
            DataGridDetailPurchase.ItemsSource = allPurchaseDetail;
        }

        private void purchaseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var test = (sender as DataGrid).SelectedItem;
                ObservableCollection<object> allPurchaseDetail = new ObservableCollection<object>();
                int? id = (int?)test?.GetType().GetProperty("Purchase_ID")?.GetValue(test, null);

                if (id != null)
                {
                    int index = (int)id;
                    var dt = QueryForSQLServer.GetPurchaseDetail(index);
                    foreach (DataRow item in dt.Rows)
                    {
                        var _p = new
                        {
                            Name = item["Name"],
                            //Price = item["Price"],
                            Quantity = item["Quantity"],
                            Total = item["Total"]
                        };
                        allPurchaseDetail.Add(_p);
                    }
                    DataGridDetailPurchase.ItemsSource = allPurchaseDetail;
                }
                else
                {
                    purchaseDataGrid.SelectedIndex = -1;
                    allPurchaseDetail.Clear();
                    DataGridDetailPurchase.ItemsSource = allPurchaseDetail;
                }
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message.ToString());
            }
            
        }

        private void deletePurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var Selecteditem = (sender as FrameworkElement).DataContext;
            int id = (int)Selecteditem?.GetType().GetProperty("Purchase_ID")?.GetValue(Selecteditem, null);
            QueryForSQLServer.DeletePurchase(id);
            reLoadData();
        }

        void FillByStates(PurchaseStatus? Selecteditem)
        {
            var list = allPurchase.ToList();
            if (Selecteditem != null && Selecteditem != PurchaseStatus.All)
            {
                allPurchase = new ObservableCollection<object>();
                for (int i = 0; i < list.Count(); i++)
                {
                    var status = list[i]?.GetType().GetProperty("Status")?.GetValue(list[i], null) as PurchaseStatus?;
                    if (status == Selecteditem)
                    {
                        allPurchase.Add(list[i]);
                    }
                }
                purchaseDataGrid.ItemsSource = allPurchase;
            }
        }
        private void purchaseStatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Selecteditem = (sender as ComboBox).SelectedItem as PurchaseStatus?;
            reLoadData();
            FillByStates(Selecteditem);
        }
        void FillByDate(DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            var list = allPurchase.ToList();
            if (fromDate != null && toDate != null)
            {
                allPurchase = new ObservableCollection<object>();
                for (int i = 0; i < list.Count(); i++)
                {
                    var Created_At = list[i]?.GetType().GetProperty("Created_At")?.GetValue(list[i], null)?.ToString();
                    var date = DateTime.Parse(Created_At);
                    if (fromDate <= date && date <= toDate)
                    {
                        allPurchase.Add(list[i]);
                    }
                }
                purchaseDataGrid.ItemsSource = allPurchase;
            }
        }
        private void fromDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var fromDate = fromDatePicker.Date;
            var toDate = toDatePicker.Date ;
            reLoadData();
            purchaseStatesComboBox.SelectedIndex = 4;
            FillByDate(fromDate, toDate);
        }

        private void toDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var fromDate = fromDatePicker.Date;
            var toDate = toDatePicker.Date;
            reLoadData();
            purchaseStatesComboBox.SelectedIndex = 4;
            FillByDate(fromDate, toDate);
        }

        private void btnComplexFilter_Click(object sender, RoutedEventArgs e)
        {
            var Selecteditem = purchaseStatesComboBox.SelectedItem as PurchaseStatus?;
            reLoadData();
            FillByStates(Selecteditem);
            var fromDate = fromDatePicker.Date;
            var toDate = toDatePicker.Date;
            FillByDate(fromDate, toDate);

        }

        (ObservableCollection<object>, ObservableCollection<object>) search(string txtOrig,int kt = -1)
        {
            //Set the ItemsSource to be your filtered dataset
            var dt = QueryForSQLServer.GetPurchase();
            var searchText = new ObservableCollection<object>();
            var searchPurchase = new ObservableCollection<object>();
            //purchase
            foreach (DataRow item in dt.Rows)
            {
                string formatted = ((DateTime)item["Created_At"]).ToString("dd/MM/yyyy");
                var _p = new
                {
                    Purchase_ID = (int)item["Purchase_ID"],
                    Customer_Name = item["Customer_Name"] as string,
                    Tel = item["Tel"] as string,
                    Total = (decimal)item["Total"],
                    Created_At = formatted,
                    Status = (PurchaseStatus)(item["Status"])
                };
                searchPurchase.Add(_p);
            }
            var list = searchPurchase.ToList();
            if (true)
            {
                searchPurchase = new ObservableCollection<object>();
                searchText = new ObservableCollection<object>();
                for (int i = 0; i < list.Count(); i++)
                {
                    var Customer_Name = list[i]?.GetType().GetProperty("Customer_Name")?.GetValue(list[i], null)?.ToString();
                    if (Customer_Name.ToUpper().Contains(txtOrig.ToUpper()) && kt == -1)
                    {
                        searchText.Add(Customer_Name);
                        searchPurchase.Add(list[i]);
                    }
                    else if (Customer_Name.ToUpper().Equals(txtOrig.ToUpper()) && kt != -1)
                    {
                        searchPurchase.Add(list[i]);
                    }
                }
            }
            return (searchPurchase,searchText);
        }
        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var txtOrig = (sender as AutoSuggestBox).Text.ToUpper();

                
                //sender.ItemsSource = dataset;
                sender.ItemsSource = search(txtOrig).Item2;
            }
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
                purchaseDataGrid.ItemsSource = search(args.ChosenSuggestion.ToString(),1).Item1;
            }
            else
            {
                // Use args.QueryText to determine what to do.
                purchaseDataGrid.ItemsSource = search(args.QueryText).Item1;
            }
        }
    }
}

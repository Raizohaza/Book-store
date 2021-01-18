using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageCheckOut : Page
    {
        ObservableCollection<object> allPurchase = new ObservableCollection<object>();
        object customerDetail = new object();
        //editGridDetailPurchase
        public static ObservableCollection<object> collection { get; set; }
        int current_id;
        void reLoadData()
        {

            var dt = QueryForSQLServer.GetPurchase();
            allPurchase = new ObservableCollection<object>();
            //purchase
            foreach (DataRow item in dt.Rows)
            {
                string formatted = ((DateTime)item["Created_At"]).ToString();
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
            PagingPage();
            //list_product.Clear();
        }

        #region paging
        private ObservableCollection<object> displayList = new ObservableCollection<object>(); //List to be displayed in ListView
        int pageIndex = 1;
        int pageSize = 4; //Set the size of the page
        int totalPage = 1;
        string CurrentPage;


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex + 1 < totalPage)
            {
                pageIndex++;
                var filter = allPurchase.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                displayList = new ObservableCollection<object>(filter);
                CurrentPage = (pageIndex + 1).ToString() + "/" + (totalPage).ToString();
                pageInfo.DataContext = CurrentPage;
                purchaseDataGrid.ItemsSource = displayList;
            }

        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex - 1 >= 0)
            {
                pageIndex--;
                var filter = allPurchase.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                displayList = new ObservableCollection<object>(filter);
                CurrentPage = (pageIndex + 1).ToString() + "/" + (totalPage).ToString();
                pageInfo.DataContext = CurrentPage;
                purchaseDataGrid.ItemsSource = displayList;
            }
        }

        private void PagingPage()
        {
            pageIndex = -1;
            totalPage = (int)Math.Ceiling((double)allPurchase.Count() / pageSize);
            NextButton_Click(null, null);
            //productTotalTb.DataContext = allPurchase.Count();
        }
        #endregion

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
            productsListView.ItemsSource = QueryForSQLServer.GetProductFromDb();
        }


        void getCustomer(string tel, bool uneditable = true)
        {
            if (tel != null)
            {
                var customerDt = QueryForSQLServer.GetCustomerByTel(tel);
                foreach (DataRow row in customerDt.Rows)
                {
                    customerDetail = new
                    {
                        Customer_Name = row.ItemArray[0],
                        Tel = row.ItemArray[1],
                        Customer_Address = row.ItemArray[2],
                        Customer_Email = row.ItemArray[3]
                    };
                }
                CustomerStackPanel.DataContext = customerDetail;

                customerEmailTextBox.IsReadOnly = uneditable;
                customerNameTextBox.IsReadOnly = uneditable;
                customerAddressTextBox.IsReadOnly = uneditable;
            }
            else
                CustomerStackPanel.DataContext = null;
        }

        private void editPurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            btnAddItem.Visibility = Visibility.Visible;
            btnDeleteItem.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Visible;
            btnDone.Visibility = Visibility.Visible;


            var Selecteditem = (sender as FrameworkElement).DataContext;
            current_id = (int)Selecteditem?.GetType().GetProperty("Purchase_ID")?.GetValue(Selecteditem, null);
            DataGridDetailPurchase.AutoGenerateColumns = false;

            var dt = QueryForSQLServer.GetPurchaseDetail(current_id);
            DataGridDetailPurchase.Columns.Clear();

            //customer
            string tel = Selecteditem?.GetType().GetProperty("Tel")?.GetValue(Selecteditem, null).ToString();
            getCustomer(tel, false);

            //DataGridDetailPurchase
            DataGridDetailPurchase.Columns.Add(new DataGridTextColumn()
            {
                Header = dt.Columns[1].ColumnName,
                Binding = new Binding { Path = new PropertyPath("Product_Name") }
            });

            DataGridDetailPurchase.Columns.Add(new DataGridTextColumn()
            {
                Header = dt.Columns[2].ColumnName,
                Binding = new Binding
                {
                    Path = new PropertyPath("Quantity"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                }
            });
            DataGridDetailPurchase.Columns[1].IsReadOnly = false;


            DataGridDetailPurchase.Columns.Add(new DataGridTextColumn()
            {
                Header = dt.Columns[4].ColumnName,
                Binding = new Binding { Path = new PropertyPath("SubTotal") }
            });

            collection = new ObservableCollection<object>();


            foreach (DataRow row in dt.Rows)
            {
                //collection.Add(row.ItemArray);
                collection.Add(new
                {
                    Product_ID = row.ItemArray[0],
                    Product_Name = row.ItemArray[1],
                    Quantity = row.ItemArray[2],
                    Unit_Price = row.ItemArray[3],
                    SubTotal =row.ItemArray[4],
                    PurchaseDetail_ID = row.ItemArray[5]
                });
            }
            DataGridDetailPurchase.ItemsSource = collection;

        }

        private void purchaseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var test = (sender as DataGrid).SelectedItem;

                ObservableCollection<object> allPurchaseDetail = new ObservableCollection<object>();

                int? id = (int?)test?.GetType().GetProperty("Purchase_ID")?.GetValue(test, null);
                string tel = test?.GetType().GetProperty("Tel")?.GetValue(test, null).ToString();

                getCustomer(tel);

                #region PurchaseDetail
                DataGridDetailPurchase.Columns.Clear();
                DataGridDetailPurchase.AutoGenerateColumns = true;
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
                #endregion

                btnAddItem.Visibility = Visibility.Collapsed;
                btnDeleteItem.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Collapsed;
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
        
        #region filter
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
            PagingPage();
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
            PagingPage();
        }
        private void fromDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var fromDate = fromDatePicker.Date;
            var toDate = toDatePicker.Date;
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
        #endregion

        #region search
        (ObservableCollection<object>, ObservableCollection<object>) search(string txtOrig, int kt = -1)
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
            return (searchPurchase, searchText);
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
                allPurchase = search(args.ChosenSuggestion.ToString(), 1).Item1;
                purchaseDataGrid.ItemsSource = allPurchase;
                PagingPage();
            }
            else
            {
                // Use args.QueryText to determine what to do.
                allPurchase = search(args.QueryText).Item1;
                purchaseDataGrid.ItemsSource = allPurchase;
                PagingPage();
            }
        }
        #endregion


        
        ObservableCollection<object> list_product = new ObservableCollection<object>();
        ObservableCollection<object> list_Delete = new ObservableCollection<object>();

        private async void productsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var kt = productsListView.SelectedIndex;
                if (kt != -1)
                {
                    var item = productsListView.SelectedItem as Product;

                    // Kiểm tra sản phẩm đã có sẵn hay chưa
                    var foundIndex = -1;
                    for (int i = 0; i < list_product.Count; i++)
                    {
                        dynamic p = list_product[i];
                        Debug.WriteLine(item.Id.ToString() + item.Quantity.ToString());
                        var test = p.Product_ID.ToString() + p.Quantity.ToString();
                        var test2 = p.Product_ID == item.Id;
                        var test3 = p.Quantity + 1 <= item.Quantity;

                        if (test2)
                        {
                            Debug.WriteLine("true1");
                        }

                        if (test3)
                        {
                            Debug.WriteLine("true2");
                        }

                        if (p.Product_ID == item.Id && p.Quantity + 1 <= item.Quantity)
                        {
                            
                            var updatedProduct = new
                            {
                                Product_ID = item.Id,
                                Product_Name = item.Name,
                                SubTotal = (p.Quantity + 1) * p.Unit_Price,
                                Quantity = p.Quantity + 1,
                                Unit_Price = p.Unit_Price
                            };
                            list_product.RemoveAt(i);
                            list_product.Insert(i, updatedProduct);

                            foundIndex = i; // báo hiệu đã tìm thấy
                            break;
                        }
                        if (p.Product_ID == item.Id && p.Quantity + 1 > item.Quantity)
                        {
                            var messageDialog2 = await new MessageDialog("The product was sold out!", "Confirm").ShowAsync();
                            foundIndex = i; // báo hiệu đã tìm thấy
                            break;
                        }
                    }

                    if (foundIndex == -1 && item.Quantity > 0) // Chưa cập nhật
                    {
                        list_product.Add(new
                        {
                            Product_ID = item.Id,
                            Product_Name = item.Name,
                            Quantity = 1,
                            Unit_Price = item.Price,
                            SubTotal = item.Price                            
                        });
                    }

                    productsListView.SelectedIndex = -1;
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());
            }
        }
        async void addNewItemsToCollection ()
        {
            // Kiểm tra sản phẩm đã có sẵn hay chưa
            foreach (dynamic item in list_product)
            {
                var foundIndex = -1;
                for (int i = 0; i < collection.Count; i++)
                {
                    dynamic p = collection[i];
                    if (p.Product_ID == item.Product_ID)
                    {
                        var updatedProduct = new
                        {
                            Product_ID = item.Product_ID,
                            Product_Name = item.Product_Name,
                            SubTotal = (p.Quantity + item.Quantity) * p.Unit_Price,
                            Quantity = p.Quantity + item.Quantity,
                            Unit_Price = p.Unit_Price,
                            PurchaseDetail_ID = p.PurchaseDetail_ID
                        };
                        collection.RemoveAt(i);
                        collection.Insert(i, updatedProduct);

                        foundIndex = i; // báo hiệu đã tìm thấy
                        break;
                    }
                    if (p.Product_ID == item.Product_ID && p.Quantity + 1 > item.Quantity)
                    {
                        var messageDialog2 = await new MessageDialog("The product was sold out!", "Confirm").ShowAsync();
                        foundIndex = i; // báo hiệu đã tìm thấy
                        break;
                    }
                }

                if (foundIndex == -1 ) // Chưa cập nhật
                {
                    var newProduct = new
                    {
                        Product_ID = item.Product_ID,
                        Product_Name = item.Product_Name,
                        Quantity = item.Quantity,
                        Unit_Price = item.Unit_Price,
                        SubTotal = item.Quantity * item.Unit_Price
                    };
                    collection.Add(newProduct);
                }                
            }
        }
        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string allAddProduct = "";
            for (int i = 0; i < list_product.Count; i++)
            {
                dynamic p = list_product[i];

                allAddProduct += p.Product_Name + " x " + p.Quantity + "\n";
            }
            TextBlock input = new TextBlock()
            {
                Height = this.ActualHeight,
                Text = allAddProduct,
                TextWrapping = TextWrapping.WrapWholeWords
            };
            var item = collection;
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
                input = (TextBlock)dialog.Content;
                if (input.Text != "")
                {
                    addNewItemsToCollection();
                    DataGridDetailPurchase.ItemsSource = collection;
                    await new Windows.UI.Popups.MessageDialog("Updated!").ShowAsync();
                }
                else
                    await new Windows.UI.Popups.MessageDialog("Nothing Change!").ShowAsync();
            }
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            addItemPopup.IsOpen = true;
            list_product.Clear();
            productsListView.SelectedIndex = -1;
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var id = DataGridDetailPurchase.SelectedIndex;
            if (id != -1)
            {
                dynamic p = collection[id];
                int updatequantity = p.Quantity;

                //update popup
                TextBox input = new TextBox()
                {
                    Height = (double)App.Current.Resources["TextControlThemeMinHeight"],
                    PlaceholderText = p.Quantity.ToString()
                };
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Change Item's Quantity",
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
                        updatequantity = int.Parse(input.Text);
                        await new MessageDialog("Updated!").ShowAsync();
                    }
                    else
                        await new MessageDialog("Nothing Change!").ShowAsync();
                }

                Type typeOfDynamic = p.GetType();
                bool exist = typeOfDynamic.GetProperties().Where(x => x.Name.Equals("PurchaseDetail_ID")).Any();

                if (exist)
                {
                    var updatedProduct = new
                    {
                        Product_ID = p.Product_ID,
                        Product_Name = p.Product_Name,
                        SubTotal = updatequantity * p.Unit_Price,
                        Quantity = updatequantity,
                        Unit_Price = p.Unit_Price,
                        PurchaseDetail_ID = p.PurchaseDetail_ID
                    };
                    collection.RemoveAt(id);
                    collection.Insert(id, updatedProduct);
                }
                else
                {
                    var updatedProduct = new
                    {
                        Product_ID = p.Product_ID,
                        Product_Name = p.Product_Name,
                        SubTotal = updatequantity * p.Unit_Price,
                        Quantity = updatequantity,
                        Unit_Price = p.Unit_Price                        
                    };
                    collection.RemoveAt(id);
                    collection.Insert(id, updatedProduct);
                }
                
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("Please choose a item!").ShowAsync();
            }
        }

        private async void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var id = DataGridDetailPurchase.SelectedIndex;
            if (id != -1)
            {
                dynamic p = collection[id];
                await new MessageDialog($"Delete {p.Product_Name} item!").ShowAsync();

                Type typeOfDynamic = p.GetType();
                bool exist = typeOfDynamic.GetProperties().Where(x => x.Name.Equals("PurchaseDetail_ID")).Any();
                if (exist)
                {
                    list_Delete.Add(p);
                }
                collection.RemoveAt(id);
            }
            else
            {
                await new MessageDialog("Please choose a item!").ShowAsync();
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            var customerName = customerNameTextBox.Text.ToString();
            var customerEmail = customerEmailTextBox.Text.ToString();
            var customerAddress = customerAddressTextBox.Text.ToString();
            var customerTel = customerTelTextBox.Text.ToString();
            var customerDetailUpdate = new Customer
            {
                Customer_Name = customerName,
                Tel = customerTel,
                Address = customerAddress,
                Email = customerEmail
            };
            QueryForSQLServer.UpdateCustomer(customerDetailUpdate);

            //update + insert purchasedetail
            QueryForSQLServer.UpdateListPurchaseDetail(collection);
            QueryForSQLServer.DeleteListPurchaseDetail(list_Delete);
            QueryForSQLServer.InsertListPurchaseDetail(collection, current_id);


            list_Delete.Clear();

            btnAddItem.Visibility = Visibility.Collapsed;
            btnDeleteItem.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnDone.Visibility = Visibility.Collapsed;

            reLoadData();
        }
    }
}
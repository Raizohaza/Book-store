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
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageBuy : Page
    {
        ObservableCollection<Product> ProductData = new ObservableCollection<Product>();
        Purchase p = new Purchase();
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
            if (catId != 0)
            {
                var productFillered = from product in products
                                      where product.CatId == catId
                                      select product;
                products = new ObservableCollection<Product>(productFillered);
            }

            return products;
        }
        public PageBuy()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductData = GetProductFromDb();
            productsListView.ItemsSource = ProductData;
        }

        ObservableCollection<object> list = new ObservableCollection<object>();
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
                    for (int i = 0; i < list.Count; i++)
                    {
                        dynamic p = list[i];
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
                            list.RemoveAt(i);
                            list.Insert(i, updatedProduct);

                            foundIndex = i; // báo hiệu đã tìm thấy
                            //break;
                        }
                        if (p.Product_ID == item.Id && p.Quantity + 1 > item.Quantity)
                        {
                            var messageDialog2 = await new MessageDialog("The product was sold out!", "Confirm").ShowAsync();
                            foundIndex = i; // báo hiệu đã tìm thấy
                            break;
                        }    
                    }

                    if (foundIndex == -1) // Chưa cập nhật
                    {
                        list.Add(new
                        {
                            Product_ID = item.Id,
                            Product_Name = item.Name,
                            Quantity = 1,
                            Unit_Price = item.Price,
                            SubTotal = item.Price
                        });
                    }
                    productsListView.SelectedIndex = -1;
                    selectedProductsListView.ItemsSource = list;
                }
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine( ex.ToString());
            }
        }

        private async void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Customer
                var dt = QueryForSQLServer.GetCustomerByTel(customerTelTextBox.Text.ToString());
                if (dt.Rows.Count == 0)
                {
                    var _newCustomer = new Customer
                    {
                        Tel = customerTelTextBox.Text.ToString(),
                        Customer_Name = customerNameTextBox.Text.ToString()
                    };
                    QueryForSQLServer.InsertCustomer(_newCustomer);
                }
                else
                {
                    customerNameTextBox.Text = dt.Rows[0].ItemArray[0].ToString();
                }
                

                //Purchase
                var purchase = new Purchase()
                {
                    Created_At = DateTime.Now,
                    Total = list.Sum((dynamic p) => p.SubTotal as Nullable<decimal>),
                    Customer_Tel = customerTelTextBox.Text.ToString(),
                    Status = 1
                };
                var p_id = QueryForSQLServer.InsertPurchase(purchase);

                //PurchaseDetail
                if (p_id == -1)
                {
                    var messageDialog = await new MessageDialog("Failed", "Confirm").ShowAsync();
                }
                else
                {
                    foreach (dynamic item in list)
                    {
                        purchase.PurchaseDetails.Add(new PurchaseDetail()
                        {
                            Purchase_ID = p_id,
                            Product_ID = item.Product_ID,
                            Price = item.Unit_Price,
                            Quantity = item.Quantity,
                            Total = item.SubTotal
                        });
                        
                    }
                    foreach (dynamic item in purchase.PurchaseDetails)
                    {
                        QueryForSQLServer.InsertPurchaseDetail(item);
                    }
                }
                

                var messageDialog2 = await new MessageDialog("Success", "Confirm").ShowAsync();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
        }
    }
}

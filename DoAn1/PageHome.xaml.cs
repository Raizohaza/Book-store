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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        
        public PageHome()
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
            //var cat = new Category() { Id = 8, Name = "ThinkPad" };
            //provider::QueryForSQLServer.DeleteCategory(7);
            //provider::QueryForSQLServer.UpdateCategory(cat);
            test_data.ItemsSource = products;
        }
        public void Refresh()
        {
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
            //var cat = new Category() { Id = 8, Name = "ThinkPad" };
            //provider::QueryForSQLServer.DeleteCategory(7);
            //provider::QueryForSQLServer.UpdateCategory(cat);
            test_data.ItemsSource = products;
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
            screen.Handler += GetProductFromUC;
            GridHome.Children.Add(screen);
            //this.Visibility = Visibility.Collapsed;
        }

        private void GetProductFromUC(Product product)
        {
            provider::QueryForSQLServer.UpdateProduct(product);
            Refresh();
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;

            //Instantiates the File Picker.



            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".xlsx");
            openPicker.FileTypeFilter.Add(".xls");
            StorageFile openFile = await openPicker.PickSingleFileAsync();

            //Opens the workbook. 
            IWorkbook workbook = await application.Workbooks.OpenAsync(openFile);

            //Access first worksheet from the workbook.
            var tabs = workbook.Worksheets;

            //Set Text in cell A3.


            //Sets workbook version.
            workbook.Version = ExcelVersion.Excel2016;

            //Initializes FileSavePicker.
            FileSavePicker savePicker = new FileSavePicker();

            List<Category> list = new List<Category>();

            foreach (var tab in tabs)
            {
                Debug.WriteLine(tab.Name);
                var row = 3;
                var category = new Category()
                {
                    Name = tab.Name
                };
                category.Id = provider::QueryForSQLServer.InsertCategory(category);

                //db.Categories.Add(category);
                //db.SaveChanges();
                tab.UsedRangeIncludesFormatting = false;
                var cell = tab.Range[$"C3"];

                while (cell.Value != null && !cell.IsBlank)
                {
                    var sku = tab.Range[$"C{row}"].Text;
                    var name = tab.Range[$"D{row}"].Text;
                    var price = Convert.ToDecimal(tab.Range[$"E{row}"].Number);
                    var quantity = (int)(tab.Range[$"F{row}"].Number);
                    var description = tab.Range[$"G{row}"].Text;
                    var image = tab.Range[$"H{row}"].Text;

                    var product = new Product()
                    {
                        SKU = sku,
                        Name = name,
                        CatId = category.Id,
                        Price = price,
                        Quantity = quantity,
                        Description = description,
                        Image = image
                    };

                    category.Products.Add(product);


                    Debug.WriteLine($"{sku}{name}{price}{quantity}{description}");

                    // Đi qua dòng kế
                    row++;
                    cell = tab.Range[$"C{row}"];
                }
                list.Add(category);


            }
            var tes = list;

            workbook.Close();
            excelEngine.Dispose();
        }

    }
}

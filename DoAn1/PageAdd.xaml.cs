using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
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
    public sealed partial class PageAdd : Page
    {
        Product Product { get; set; }
        public delegate void Save(Product productRef);
        public event Save Handler;
        public PageAdd()
        {
            this.InitializeComponent();


            Product = new Product()
            {
                Name = "Tên",
                Description = "Nội dung",
                Price = 0,
                Quantity = 0,
                CatId = 1,
                Image = "x",
                Author = "x"             
            };
            this.DataContext = Product;
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
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

                    tab.UsedRangeIncludesFormatting = false;
                    var cell = tab.Range[$"C3"];

                    while (cell.Value != null && !cell.IsBlank)
                    {
                        var author = tab.Range[$"J{row}"].Text;
                        var name = tab.Range[$"C{row}"].Text;
                        var price = Convert.ToDecimal(tab.Range[$"D{row}"].Number);
                        var quantity = (int)(tab.Range[$"E{row}"].Number);
                        var description = tab.Range[$"F{row}"].Text;
                        var image = tab.Range[$"I{row}"].Text;

                        var product = new Product()
                        {
                            Author = author,
                            Name = name,
                            CatId = category.Id,
                            Price = price,
                            Quantity = quantity,
                            Description = description,
                            Image = image
                        };

                        category.Products.Add(product);


                        Debug.WriteLine($"{author}{name}{price}{quantity}{description}");

                        // Đi qua dòng kế
                        row++;
                        cell = tab.Range[$"C{row}"];
                    }
                    list.Add(category);


                }
                var tes = list;

                workbook.Close();
                excelEngine.Dispose();

                var messageDialog = new MessageDialog("Import", "Confirm");

                messageDialog.Commands.Add(new UICommand("Yes")
                {
                    Id = 0
                });
                messageDialog.Commands.Add(new UICommand("No")
                {
                    Id = 1
                });
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;

                var result = await messageDialog.ShowAsync();
                if ((int)result.Id == 0)
                {
                    foreach (var cat in list)
                    {
                        cat.Id = provider::QueryForSQLServer.InsertCategory(cat);
                        foreach (var product in cat.Products)
                        {
                            var index = provider::QueryForSQLServer.InsertProduct(product);
                        }
                    }
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception: " + ex.Message);
            }
            
        }


        private async void btnDone_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Add new produt", "Confirm");

            messageDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            messageDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            var result = await messageDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                provider::QueryForSQLServer.InsertProduct(Product);
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;

            //Instantiates the File Picker.
            try
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                openPicker.FileTypeFilter.Add(".xlsx");
                openPicker.FileTypeFilter.Add(".xls");
                StorageFile openFile = await openPicker.PickSingleFileAsync();

                //Opens the workbook. 
                IWorkbook workbook = await application.Workbooks.OpenAsync(openFile);

                //Access first worksheet from the workbook.
                var tabs = workbook.Worksheets;

                //Sets workbook version.
                workbook.Version = ExcelVersion.Excel2016;

                //Initializes FileSavePicker.
                FileSavePicker savePicker = new FileSavePicker();
                List<Product_Images> list = new List<Product_Images>();
                foreach (var tab in tabs)
                {
                    Debug.WriteLine(tab.Name);
                    var row = 3;
                    var product = new Product()
                    {
                        Name = tab.Name
                    };

                    tab.UsedRangeIncludesFormatting = false;
                    var cell = tab.Range[$"C3"];

                    while (cell.Value != null && !cell.IsBlank)
                    {
                        var image = tab.Range[$"B{row}"].Text;
                        var name = tab.Range[$"C{row}"].Text;

                        var ImageId = (int)provider::QueryForSQLServer.GetProductsByImage(image).Rows[0].ItemArray[0];

                        var product_image = new Product_Images()
                        {
                            ProductId = ImageId,
                            Name = name
                        };


                        Debug.WriteLine($"{image}{name}");
                        list.Add(product_image);
                        // Đi qua dòng kế
                        row++;
                        cell = tab.Range[$"C{row}"];
                    }
                }
                workbook.Close();
                excelEngine.Dispose();

                var messageDialog = new MessageDialog("Import", "Confirm");

                messageDialog.Commands.Add(new UICommand("Yes")
                {
                    Id = 0
                });
                messageDialog.Commands.Add(new UICommand("No")
                {
                    Id = 1
                });
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;

                var result = await messageDialog.ShowAsync();
                if ((int)result.Id == 0)
                {
                    foreach (var img in list)
                    {
                        provider::QueryForSQLServer.InsertProduct_Image(img);
                    }
                    var messageDialog2 = new MessageDialog("Success", "Confirm");
                    await messageDialog2.ShowAsync();
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception: " + ex.Message);
            }
            
        }

        
    }
}

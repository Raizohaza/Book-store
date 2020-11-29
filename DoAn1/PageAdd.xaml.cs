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
                    var author = tab.Range[$"C{row}"].Text;
                    var name = tab.Range[$"D{row}"].Text;
                    var price = Convert.ToDecimal(tab.Range[$"E{row}"].Number);
                    var quantity = (int)(tab.Range[$"F{row}"].Number);
                    var description = tab.Range[$"G{row}"].Text;
                    var image = tab.Range[$"H{row}"].Text;

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
                var item = Product;
                Handler?.Invoke(Product);
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
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

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Connection string for using Windows Authentication.
        private string connectionString = @"Data Source=DESKTOP-F8PIA3B\SQLEXPRESS;Initial Catalog=MyStore;Integrated Security=True";

        // This is an example connection string for using SQL Server Authentication.
        // private string connectionString =
        //     @"Data Source=YourServerName\YourInstanceName;Initial Catalog=DatabaseName; User Id=XXXXX; Password=XXXXX";

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public MainPage()
        {
            this.InitializeComponent();

            DataTable data = null;
            var products = new ObservableCollection<Product>();

            data = Provider.GetProducts(ConnectionString);
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
            //var product = new Product();
            //product.Id = 2;
            //product.CatId = 2;
            //product.SKU = "bc";
            //product.Name = "bc";
            //product.Price = (Decimal) 2;
            //product.Quantity = 2;
            //product.Description = "cbncx";
            //product.Image = "ssss";

            //products.Add(product);

            test_data.ItemsSource = products;

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
            IWorksheet worksheet = workbook.Worksheets[0];
            
            ////Set Text in cell A3.
            //worksheet.Range["A3"].Text = "Hello World";

            ////Sets workbook version.
            //workbook.Version = ExcelVersion.Excel2016;

            ////Initializes FileSavePicker.
            //FileSavePicker savePicker = new FileSavePicker();
            //savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            //savePicker.SuggestedFileName = "CreateSpreadsheet";
            //savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx" });

            ////Creates a storage file from FileSavePicker.
            //StorageFile storageFile = await savePicker.PickSaveFileAsync();

            ////Saves changes to the specified storage file.
            //await workbook.SaveAsAsync(storageFile);

            workbook.Close();
            excelEngine.Dispose();
        }
    //    private async void ImportButton2_Click(object sender, RoutedEventArgs e)
    //    {
    //        var dialog = new OpenFileDialog();

    //        if (dialog.ShowDialog() == true)
    //        {
    //            var filename = dialog.FileName;
    //            var info = new FileInfo(filename);
    //            var imagesFolder = info.Directory + "\\images";

    //            var workbook = new Workbook(filename);
    //            var sheets = workbook.Worksheets;

    //            var db = new MyStoreEntities();

    //            foreach (var sheet in sheets)
    //            {
    //                // Mỗi tab của file excel là một loại sản phẩm
    //                var category = new Category()
    //                {
    //                    Name = sheet.Name
    //                };
    //                db.Categories.Add(category);
    //                await db.SaveChangesAsync();

    //                // Lấy thông tin đường dẫn
    //                var baseFolder = AppDomain.CurrentDomain.BaseDirectory; // Có dấu \ ở cuối

    //                // Kiểm tra thư mục Images có tồn tại hay chưa
    //                if (!Directory.Exists(baseFolder + "Images\\"))
    //                {
    //                    Directory.CreateDirectory(baseFolder + "Images\\");
    //                }

    //                //  Sau khi có được thông tin id của loại sản phẩm
    //                var cell = sheet.Cells[$"B3"];
    //                var row = 3;

    //                while (cell.Value != null)
    //                {
    //                    // Xác định tên file ảnh trước
    //                    var image = sheet.Cells[$"H{row}"].StringValue;
    //                    var extension = image.Substring(image.Length - 4, 4);
    //                    var newName = Guid.NewGuid().ToString();

    //                    // Copy ảnh vào thư mục chứa ảnh cùng file exe
    //                    File.Copy(imagesFolder + "\\" + image, baseFolder + "Images\\" + newName + extension);

    //                    var product = new Product()
    //                    {
    //                        CatId = category.Id,
    //                        SKU = sheet.Cells[$"C{row}"].StringValue,
    //                        Name = sheet.Cells[$"D{row}"].StringValue,
    //                        Price = sheet.Cells[$"E{row}"].IntValue,
    //                        Quantity = sheet.Cells[$"F{row}"].IntValue,
    //                        Description = sheet.Cells[$"G{row}"].StringValue,
    //                        Image = newName,
    //                    };

    //                    row++;
    //                    cell = sheet.Cells[$"B{row}"];
    //                }
    //            }

    //            MessageBox.Show("Đã import xong!");

    //        }
    //    }
    }
}

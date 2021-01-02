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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAdd : Page
    {
        Product Product { get; set; }
        List<Product_Images> test = new List<Product_Images>();

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
                Image = "default.jpg",
                Author = "Chưa cập nhật",
                
                
            };
            Product.Product_Images = new List<Product_Images>();
            this.DataContext = Product;
            var categoriesList = PageHome.GetCategoriesFromDb();
            cbbListType.ItemsSource = categoriesList;
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
                    category.Id = QueryForSQLServer.InsertCategory(category);

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
                        cat.Id = QueryForSQLServer.InsertCategory(cat);
                        foreach (var product in cat.Products)
                        {
                            var index = QueryForSQLServer.InsertProduct(product);
                        }
                    }
                    var messageDialog2 = await new MessageDialog("Success", "Confirm").ShowAsync();
                }
                else
                {
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
                Product.Price = Decimal.Parse(addGia.Text);
                Product.Quantity = int.Parse(addSoLuong.Text);
                var productid = QueryForSQLServer.InsertProduct(Product);
                int id = 1;
                foreach (var item in Product.Product_Images)
                {
                    item.id = id;
                    item.ProductId = productid;
                    QueryForSQLServer.InsertProduct_Image(item);
                    id++;
                }
                Frame.GoBack();
            }
            else
            {
                Frame.GoBack();
            }
        }

        private async void Import_Detail_Images_Click(object sender, RoutedEventArgs e)
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

                        var ImageId = (int)QueryForSQLServer.GetProductsByImage(image).Rows[0].ItemArray[0];

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
                    var i = QueryForSQLServer.GetProducts_ImageMaxId(list[0].ProductId) + 1;
                    var preProduct = list[0].ProductId;
                    foreach (var img in list)
                    {
                        if (img.ProductId != preProduct)
                        {
                            i = QueryForSQLServer.GetProducts_ImageMaxId(img.ProductId) + 1;
                        }
                        img.id = i;
                        preProduct = img.ProductId;
                        QueryForSQLServer.InsertProduct_Image(img);
                        Debug.WriteLine(img.id + img.Name + img.ProductId);
                        i++;
                    }
                    var messageDialog2 = await new  MessageDialog("Success", "Confirm").ShowAsync();
                }

                else
                {
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception: " + ex.Message);
            }
            
        }

        private async void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            //FileData 
            StorageFile openFile = await openPicker.PickSingleFileAsync();
            if (openFile != null)
            {
                var test = new Product { Image = openFile.Path };
                Product.Image = openFile.Path;
                this.DataContext = Product;
                avatarImg.DataContext = test;
            }
            
        }

        private async void btnAddProducImgs_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            //FileData 
            var openFile = await openPicker.PickMultipleFilesAsync();

            if (openFile != null)
            {
                foreach (var item in openFile)
                {
                    var Product_Images = new Product_Images()
                    {
                        Name = item.Path
                    };
                    Product.Product_Images.Add(Product_Images);
                    test.Add(Product_Images);
                }
                this.DataContext = Product;
                lvManyImg.ItemsSource = null;
                lvManyImg.ItemsSource = test;


            }
        }

        private void cbbListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbb = sender as ComboBox;
            var pd = cbb.SelectedItem as Category;
            Product.CatId = pd.Id;
        }
    }
}

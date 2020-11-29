using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            }
            else
            {

            }
        }
    }
}

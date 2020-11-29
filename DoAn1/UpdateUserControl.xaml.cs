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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DoAn1
{
    public sealed partial class UpdateUserControl : UserControl
    {
        public delegate void Save(Product productRef);
        public event Save Handler;
        Product Product { get; set; }
        public UpdateUserControl(Product item)
        {
            this.InitializeComponent();
            Product = item;

            this.DataContext = Product;
           
        }

        private async void btnDone_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Uppdate", "Confirm");

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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

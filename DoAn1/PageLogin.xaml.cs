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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoAn1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageLogin : Page
    {
        public PageLogin()
        {
            this.InitializeComponent();
        }
        public static Account acc { get; set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            acc = new Account() { username = userNametb.Text.ToString(), password = userNamepwd.Password.ToString() };
            Provider p = new Provider();
            if (p.Connect())
            {
                rootFrame.Navigate(typeof(MainPage), acc);
            }
            else
            {
                var messageDialog = await new MessageDialog("Failed to login", "Confirm").ShowAsync();
            }
        }
    }
}

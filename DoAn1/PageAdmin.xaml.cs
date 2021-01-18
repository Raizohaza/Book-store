using System;
using System.Collections.Generic;
using System.IO;
using LiveCharts;
using LiveCharts.Uwp;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PageAdmin : Page
    {
        
        public PageAdmin()
        {
            InitializeComponent();
            var data = QueryForSQLServer.GetTotalByMonth(2020);

            PieChartView.Series = new SeriesCollection();

            foreach (var item in data)
            {
                var tmp = new PieSeries() { Values = new ChartValues<decimal>() { item.Item2 }, Title = item.Item1.ToString() };
                PieChartView.Series.Add(
                    new PieSeries()
                    {
                        Values = new ChartValues<decimal> { item.Item2 },
                        Title = item.Item1.ToString()
                    }
                );
            }
            PieChartView.DataContext = this;
        }


        //private void UserControl_Initialized_2(object sender, EventArgs e)
        //{
        //    this.DataContext = this;
        //}

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            //var chart = chartPoint.ChartView as PieChart;
            //foreach (PieSeries pie in chart.Series)
            //{
            //    pie.PushOut = 0;
            //}

            //var neo = chartPoint.SeriesView as PieSeries;
            //neo.PushOut = 30;
        }



        //private void Done_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Visibility = Visibility.Collapsed;
        //}

        //private void Menu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{

        //}

        //private void Menu_Loaded(object sender, RoutedEventArgs e)
        //{

        //}
    }
}

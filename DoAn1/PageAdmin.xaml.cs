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
        List<Tuple<int, decimal>> data { get; set; }
        public PageAdmin()
        {
            InitializeComponent();
            //ngay thang nam
            fromDatePicker.Date = DateTime.Today.AddMonths(-12).Date;
            toDatePicker.Date = DateTime.Today.AddMonths(-1).Date;

            data = new List<Tuple<int, decimal>>();
            data = QueryForSQLServer.GetTotalByMonth(2020);

            var dataSeller = QueryForSQLServer.GetListBestSellerPurchases();

            string []Labels = new []{ "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul","Aug","Sep","Oct","Nov","Dec" };
            //LineChartViewX.DataContext = Labels;
            PieChartView.Series = new SeriesCollection();

            foreach (var item in data)
            {
                var tmp = new PieSeries() { Values = new ChartValues<decimal>() { item.Item2 }, Title = item.Item1.ToString() };
                PieChartView.Series.Add(
                    new PieSeries()
                    {
                        Values = new ChartValues<decimal> { item.Item2 },
                        Title = item.Item1.ToString(),
                        DataLabels = true,
                        
                    }
                ) ;
            }
            PieChartView.LegendLocation = LegendLocation.Bottom;
            PieChartView.DataContext = this;

            var productdt = QueryForSQLServer.GetProductFromDBTwo();
            DataGridProduct.ItemsSource = productdt;

            //PieChartView.Series = new SeriesCollection();

            foreach (var item in dataSeller)
            {
                var tmp = new ColumnSeries() { Values = new ChartValues<decimal>() { item.Item2 }, Title = item.Item1.ToString() };
                ColumChartView.Series.Add(
                    new ColumnSeries()
                    {
                        Values = new ChartValues<decimal> { item.Item2 },
                        Title = item.Item1.ToString(),
                        DataLabels = true,

                    }
                );
            }
            ColumChartView.LegendLocation = LegendLocation.Bottom;
            ColumChartView.DataContext = this;

            var linedata = new List<decimal>(); 
            foreach(var item in data)
            {
                linedata.Add(item.Item2);
            }    
            LineChartView.Series.Add(
                new LineSeries()

                {
                    Values = new ChartValues<decimal> { linedata[0] },
                    Title = "cc",
                    LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                    DataLabels = true,
                        

                }
            );
            for (int i = 1; i < linedata.Count(); i++)
            {
                LineChartView.Series[0].Values.Add(linedata[i]);
            }
            LineChartView.LegendLocation = LegendLocation.Bottom;
            LineChartView.AxisX.Add(
                new Axis
                {
                    MinValue = 0,
                    Labels = Labels
                }
                );
            LineChartView.AxisY.Add(
                new Axis
                {
                    MinValue = 0,
                }
                );
            LineChartView.DataContext = this;

        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
            PieChartScrollView.Visibility = Visibility.Collapsed;

            DataGridProduct.Visibility = Visibility.Visible;
        }

        private void PieChartButton_Click(object sender, RoutedEventArgs e)
        {
            PieChartScrollView.Visibility = Visibility.Visible;
            DataGridProduct.Visibility = Visibility.Collapsed;
        }

        private void ColumChartButton_CLick(object sender, RoutedEventArgs e)
        {
            ColumChartScrollView.Visibility = Visibility.Visible;
            DataGridProduct.Visibility = Visibility.Collapsed;
        }
        void FillByDate(DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            if(data != null)
            {
                data = QueryForSQLServer.GetTotalByMonth(2020);
                var list = data.ToList();
                if (fromDate != null && toDate != null)
                {
                    data = new List<Tuple<int, decimal>>();
                    for (int i = 0; i < list.Count(); i++)
                    {
                        var Created_At = list[i].Item1.ToString();
                        var dateMonth = int.Parse(Created_At);
                        var fromDateMonth = int.Parse(fromDate?.ToString("MM"));
                        var toDateMonth = int.Parse(toDate?.ToString("MM"));
                        if (fromDateMonth <= dateMonth && dateMonth <= toDateMonth )
                        {
                            data.Add(list[i]);
                        }
                    }
                    PieChartView.Series = new SeriesCollection();

                    foreach (var item in data)
                    {
                        var tmp = new PieSeries() { Values = new ChartValues<decimal>() { item.Item2 }, Title = item.Item1.ToString() };
                        PieChartView.Series.Add(
                            new PieSeries()
                            {
                                Values = new ChartValues<decimal> { item.Item2 },
                                Title = item.Item1.ToString(),
                                DataLabels = true,

                            }
                        );
                    }
                    PieChartView.LegendLocation = LegendLocation.Bottom;
                    PieChartView.DataContext = this;

                    LineChartView.Series = new SeriesCollection();
                    var linedata = new List<decimal>();
                    string[] Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    var fromDateMonth2 = int.Parse(fromDate?.ToString("MM"));
                    switch (fromDateMonth2)
                    {
                        case 1:
                            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 2:
                            Labels = new[] { "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 3:
                            Labels = new[] { "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 4:
                            Labels = new[] { "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 5:
                            Labels = new[] { "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 6:
                            
                            Labels = new[] { "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;                   
                        case 7:
                            Labels = new[] { "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 8:
                            Labels = new[] { "Aug", "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 9:
                            Labels = new[] { "Sep", "Oct", "Nov", "Dec" };
                            break;
                        case 10:
                            Labels = new[] { "Oct", "Nov", "Dec" };
                            break;
                        case 11:
                            Labels = new[] { "Nov", "Dec" };
                            break;
                        case 12:
                            Labels = new[] { "Dec" };
                            break;
                        default:
                            break;
                    }
                    foreach (var item in data)
                    {
                        linedata.Add(item.Item2);
                    }
                    LineChartView.Series.Add(
                        new LineSeries()

                        {
                            Values = new ChartValues<decimal> { linedata[0] },
                            Title = "cc",
                            LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                            DataLabels = true,


                        }
                    );
                    for (int i = 1; i < linedata.Count(); i++)
                    {
                        LineChartView.Series[0].Values.Add(linedata[i]);
                    }
                    LineChartView.LegendLocation = LegendLocation.Bottom;
                    LineChartView.AxisX.Clear();
                    LineChartView.AxisY.Clear();
                    LineChartView.AxisX.Add(
                        new Axis
                        {
                            MinValue = 0,
                            Labels = Labels,
                        }
                        );
                    LineChartView.AxisY.Add(
                        new Axis
                        {
                            MinValue = 0,
                        }
                        );
                    LineChartView.DataContext = this;
                }
            }    
            
        }
        private void fromDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var fromDate = fromDatePicker.Date;
            var toDate = toDatePicker.Date;
            
            FillByDate(fromDate, toDate);
        }

        private void toDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

        }


        //private void UserControl_Initialized_2(object sender, EventArgs e)
        //{
        //    this.DataContext = this;
        //}

        //private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        //{
        //    //var chart = chartPoint.ChartView as PieChart;
        //    //foreach (PieSeries pie in chart.Series)
        //    //{
        //    //    pie.PushOut = 0;
        //    //}

        //    //var neo = chartPoint.SeriesView as PieSeries;
        //    //neo.PushOut = 30;
        //}



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

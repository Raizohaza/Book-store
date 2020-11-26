using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DoAn1
{
    public class RelativeToAsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var relative = value.ToString();
            if (relative.Contains(":\\"))
                return relative;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var absolute = $"{folder}image\\{relative}";
            return absolute;



        //C: \Users\Admin\source\repos\LTUDQL1\milestone01\DoAn1\DoAn1\bin\x64\Debug\AppX
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
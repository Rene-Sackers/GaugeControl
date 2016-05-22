using System;
using Windows.UI.Xaml.Data;

namespace GaugeControl.UWP.Converters
{
    public class NegateNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double) value * -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

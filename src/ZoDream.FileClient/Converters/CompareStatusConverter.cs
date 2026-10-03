using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;
using ZoDream.FileClient.ViewModels;

namespace ZoDream.FileClient.Converters
{
    public class CompareStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (EntryCompareStatus)value;
            Color color;
            if (status == EntryCompareStatus.None)
            {
                color = Colors.Transparent;
            } else if (status == EntryCompareStatus.Compared)
            {
                color = Colors.LightCyan;
            }
            else
            {
                color = Colors.LightPink;
            }
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

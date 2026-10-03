using Microsoft.UI.Xaml.Data;
using System;

namespace ZoDream.FileClient.Converters
{
    public class RefreshIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "\uE711" : "\uE72C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

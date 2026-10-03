using Microsoft.UI.Xaml.Data;
using System;

namespace ZoDream.FileClient.Converters
{
    public class SwitchLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "下载" : "上传";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

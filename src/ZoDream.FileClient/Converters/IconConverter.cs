using Microsoft.UI.Xaml.Data;
using System;
using System.IO;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool b)
            {
                return b ? "\uE8B7" : "\uE8A5";
            }
            if (value is ISourceEntry e)
            {
                if (e.IsDirectory)
                {
                    return "\uE8B7";
                }
                return Path.GetExtension(e.Name).ToLower() switch
                {
                    ".png" or ".bmp" or ".jpeg" or ".jpg" or ".gif" or ".gif" => "\uE91B",
                    ".mp3" or ".flac" => "\uE8D6",
                    ".mp4" or ".avi" or ".mov" or ".wmv" or ".mpg" or ".flv" => "\uE8B2",
                    ".zip" or ".rar" or ".7z" or ".gzip" or ".gz" or ".zipx" => "\uF012",
                    ".exe" or ".dll" or ".apk" => "\uE7B8",
                    _ => "\uE8A5",
                };
            }
            return "\uE8A5";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

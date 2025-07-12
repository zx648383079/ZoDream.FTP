using Microsoft.UI.Xaml;
using System;
using ZoDream.FileClient.ViewModels;

namespace ZoDream.FileClient.Converters
{
    public static class ConverterHelper
    {
        public static string Format(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return "-";
            }
            return date.ToString("yyyy-MM-dd HH:mm");
        }


        public static string FormatSize(long size)
        {
            return SizeConverter.FormatSize(size);
        }


        public static string FormatHour(int value)
        {
            if (value <= 0)
            {
                return "00:00";
            }
            var m = value / 60;
            if (m >= 60)
            {
                return (m / 60).ToString("00") + ":"
                    + (m % 60).ToString("00") + ":" + (value % 60).ToString("00");
            }
            return m.ToString("00") + ":" + (value % 60).ToString("00");
        }

        public static Visibility VisibleIf(bool val)
        {
            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public static Visibility VisibleIf(string val)
        {
            return VisibleIf(!string.IsNullOrWhiteSpace(val));
        }

        public static Visibility VisibleIfAccount(int val)
        {
            return VisibleIf(val == 1);
        }

        public static Visibility CollapsedIf(bool val)
        {
            return VisibleIf(!val);
        }

        public static Visibility VisibleIfWorking(TransferStatus status)
        {
            return VisibleIf(status is TransferStatus.Sending or TransferStatus.Receiving or TransferStatus.Working);
        }
        public static string FormatIcon(TransferType val)
        {
            return val switch
            {
                TransferType.LocalToRemote => "\uE72A",
                _ => "\uE72B",
            };
        }
        public static string FormatIcon(TransferStatus status)
        {
            return status switch
            {
                TransferStatus.Waiting => "\uE712",
                TransferStatus.Sending => "\uE898",
                TransferStatus.Receiving => "\uE896",
                TransferStatus.Working => "\uE895",
                TransferStatus.Completed => "\uE930",
                TransferStatus.Paused => "\uE769",
                TransferStatus.Cancelled => "\uEA39",
                TransferStatus.Failed => "\uEA6A",
                _ => "\uE8FF",
            };
        }

        public static string Format(TransferStatus status)
        {
            return status switch
            {
                TransferStatus.Waiting => "Waiting",
                TransferStatus.Sending => "Sending",
                TransferStatus.Receiving => "Receiving",
                TransferStatus.Working => "Working",
                TransferStatus.Completed => "Completed",
                TransferStatus.Paused => "Paused",
                TransferStatus.Cancelled => "Cancelled",
                TransferStatus.Failed => "Occurred",
                _ => string.Empty,
            };
        }

        public static string FormatSpeed(TransferItemViewModel model)
        {
            if (model.Status == TransferStatus.Paused)
            {
                return $"{SizeConverter.FormatSize(model.Value)}/{SizeConverter.FormatSize(model.Length)}, {Format(model.Status)}";
            }
            return $"{SizeConverter.FormatSize(model.Speed)}/s - {SizeConverter.FormatSize(model.Value)}/{SizeConverter.FormatSize(model.Length)}, 剩余时间 {TimeConverter.FormatHour(model.ElapsedTime)}";
        }
    }
}

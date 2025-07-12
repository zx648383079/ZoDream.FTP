using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using System;
using ZoDream.FileClient.ViewModels;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.Controls
{
    public class ConsolePanel(RichTextBlock control)
    {
        private readonly AppViewModel _app = App.ViewModel;

        private readonly Brush Default = control.Foreground;
        private readonly Brush Danger = new SolidColorBrush(Colors.Red);
        private readonly Brush Success = new SolidColorBrush(Colors.Green);

        public void Clear()
        {
            control.Blocks.Clear();
        }

        public void WriteLine(string text)
        {
            WriteLine(text, LogLevel.Info);
        }

        public void WriteLine(string text, LogLevel level)
        {
            var color = level switch
            {
                LogLevel.Error or LogLevel.Fatal or LogLevel.Audit => Danger,
                _ => Default,
            };
            _app.DispatcherQueue.TryEnqueue(() => {
                var i = 0;
                foreach (var item in text.Split(['\n', '\r']))
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    var block = new Paragraph();
                    if (i == 0)
                    {
                        block.Inlines.Add(new Run()
                        {
                            Text = $"[{DateTime.Now:HH:mm:ss}]",
                        });
                    }
                    else
                    {
                        block.Inlines.Add(new Run()
                        {
                            Text = new string(' ', 10),
                        });
                    }
                    block.Inlines.Add(new Run()
                    {
                        Text = item.Trim(),
                        Foreground = color
                    });
                    i++;
                    control.Blocks.Add(block);
                }
            });
        }
    }
}

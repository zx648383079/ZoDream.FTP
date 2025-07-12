using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Windows.Input;
using Windows.System;

namespace ZoDream.FileClient.Controls
{
    internal static class UICommand
    {
        public static ICommand Open(Action execute) => Open(new RelayCommand(execute));
        public static ICommand Open(ICommand command) => new StandardUICommand(StandardUICommandKind.Open)
        {
            Command = command
        };

        public static ICommand Create(Action execute) => Create(new RelayCommand(execute));
        public static ICommand Create(ICommand command) => new XamlUICommand()
        {
            Label = "New",
            Description = "Create New Connect",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.Add
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator()
                {
                    Key = VirtualKey.N,
                    Modifiers = VirtualKeyModifiers.Control
                }
            },
            Command = command
        };
        public static ICommand Delete(Action execute) => Delete(new RelayCommand(execute));
        public static ICommand Delete(ICommand command) => new StandardUICommand(StandardUICommandKind.Delete)
        {
            Command = command
        };
        public static ICommand Play(Action execute) => Play(new RelayCommand(execute));
        public static ICommand Play(ICommand command) => new StandardUICommand(StandardUICommandKind.Play)
        {
            Command = command
        };
        public static ICommand Pause(Action execute) => Pause(new RelayCommand(execute));
        public static ICommand Pause(ICommand command) => new StandardUICommand(StandardUICommandKind.Pause)
        {
            Command = command
        };
        public static ICommand Stop(Action execute) => Stop(new RelayCommand(execute));
        public static ICommand Stop(ICommand command) => new StandardUICommand(StandardUICommandKind.Stop)
        {
            Command = command
        };
        public static ICommand Resume(Action execute) => Resume(new RelayCommand(execute));
        public static ICommand Resume(ICommand command) => new StandardUICommand(StandardUICommandKind.Redo)
        {
            Command = command
        };
        public static ICommand Save(Action execute) => Save(new RelayCommand(execute));
        public static ICommand Save(ICommand command) => new StandardUICommand(StandardUICommandKind.Save)
        {
            Command = command
        };
        public static ICommand SelectAll(Action execute) => SelectAll(new RelayCommand(execute));
        public static ICommand SelectAll(ICommand command) => new StandardUICommand(StandardUICommandKind.SelectAll)
        {
            Command = command
        };
        public static ICommand Backward(Action execute) => Backward(new RelayCommand(execute));
        public static ICommand Backward(ICommand command) => new StandardUICommand(StandardUICommandKind.Backward)
        {
            Command = command
        };

        public static ICommand Refresh(Action execute) => Refresh(new RelayCommand(execute));
        public static ICommand Refresh(ICommand command) => new XamlUICommand()
        {
            Label = "Refresh",
            Description = "Refresh",
            IconSource = new FontIconSource()
            {
                Glyph = "\uE72C"
            },
            Command = command
        };

        public static ICommand Home(Action execute) => Home(new RelayCommand(execute));
        public static ICommand Home(ICommand command) => new XamlUICommand()
        {
            Label = "Home",
            Description = "Home",
            IconSource = new FontIconSource()
            {
                Glyph = "\uE80F"
            },
            Command = command
        };
        public static ICommand Enter(Action execute) => Enter(new RelayCommand(execute));
        public static ICommand Enter(ICommand command) => new XamlUICommand()
        {
            Label = "Enter",
            Description = "Enter",
            IconSource = new FontIconSource()
            {
                Glyph = "\uE72A"//"\uE751"
            },
            Command = command
        };
        public static ICommand Info(Action execute) => Info(new RelayCommand(execute));
        public static ICommand Info(ICommand command) => new XamlUICommand()
        {
            Label = "Info",
            Description = "View information",
            IconSource = new FontIconSource()
            {
                Glyph = "\uE946"
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator()
                {
                    Key = VirtualKey.I,
                    Modifiers = VirtualKeyModifiers.Control
                }
            },
            Command = command
        };

        public static ICommand View(Action execute) => View(new RelayCommand(execute));
        public static ICommand View<T>(Action<T?> execute) => View(new RelayCommand<T>(execute));
        public static ICommand View(ICommand command) => new XamlUICommand()
        {
            Label = "View",
            Description = "View",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.View
            },
            Command = command
        };
        public static ICommand Add(Action execute) => Add(new RelayCommand(execute));
        public static ICommand Add(ICommand command) => new XamlUICommand()
        {
            Label = "Add",
            Description = "Add files",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.Add
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator()
                {
                    Key = VirtualKey.A,
                    Modifiers = VirtualKeyModifiers.Control
                }
            },
            Command = command
        };
        public static ICommand AddFolder(Action execute) => AddFolder(new RelayCommand(execute));
        public static ICommand AddFolder(ICommand command) => new XamlUICommand()
        {
            Label = "Add",
            Description = "Add folder",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.OpenLocal
            },
            Command = command
        };
        public static ICommand Find(Action execute) => Find(new RelayCommand(execute));
        public static ICommand Find(ICommand command) => new XamlUICommand()
        {
            Label = "Find",
            Description = "Find",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.Find
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator()
                {
                    Key = VirtualKey.F,
                    Modifiers = VirtualKeyModifiers.Control
                }
            },
            Command = command
        };
        public static ICommand Setting(Action execute) => Setting(new RelayCommand(execute));
        public static ICommand Setting(ICommand command) => new XamlUICommand()
        {
            Label = "Setting",
            Description = "Setting",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.Setting
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator()
                {
                    Key = VirtualKey.O,
                    Modifiers = VirtualKeyModifiers.Control
                }
            },
            Command = command
        };
        
        public static ICommand Log(Action execute) => Log(new RelayCommand(execute));
        public static ICommand Log(ICommand command) => new XamlUICommand()
        {
            Label = "日志文件",
            Description = "查看日志文件",
            IconSource = new SymbolIconSource()
            {
                Symbol = Symbol.CalendarDay
            },
            Command = command
        };
    }
}

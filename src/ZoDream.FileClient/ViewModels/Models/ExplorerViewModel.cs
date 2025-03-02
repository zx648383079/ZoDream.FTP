using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.ViewModel;

namespace ZoDream.FileClient.ViewModels
{
    public class ExplorerViewModel : BindableBase
    {
        public ExplorerViewModel()
        {
            BackCommand = new StandardUICommand(StandardUICommandKind.Backward)
            {
                Command = new RelayCommand(TapBack)
            };
            HomeCommand = new RelayCommand(TapHome);
            RefreshCommand = new RelayCommand(TapRefresh);
            GoCommand = new RelayCommand(TapEnter);
        }

        public ExplorerViewModel(IEntryExplorer source)
            : this()
        {
            _source = source;
        }

        private IEntryExplorer? _source;

        public string RoutePath => string.Empty;

        public ICommand HomeCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand GoCommand { get; private set; }

        private void TapBack(object? _)
        {
        }

        private void TapHome(object? _)
        {
        }
        private void TapRefresh(object? _)
        {
        }
        private void TapEnter(object? _)
        {
        }
    }
}

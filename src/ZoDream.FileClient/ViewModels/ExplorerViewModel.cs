using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZoDream.FileClient.Controls;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class ExplorerViewModel : ObservableObject
    {
        public ExplorerViewModel()
        {
            BackCommand = UICommand.Backward(TapBack);
            HomeCommand = UICommand.Home(TapHome);
            RefreshCommand = UICommand.Refresh(TapRefresh);
            GoCommand = UICommand.Enter(TapEnter);
        }

        public ExplorerViewModel(IEntryExplorer source)
            : this()
        {
            _source = source;
        }

        private IEntryExplorer? _source;

        public string RoutePath => string.Empty;

        private AsyncObservableCollection<ISourceEntry> _items = [];

        public AsyncObservableCollection<ISourceEntry> Items {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        public ICommand HomeCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand GoCommand { get; private set; }

        private void TapBack()
        {
        }

        private void TapHome()
        {
        }
        private void TapRefresh()
        {
        }
        private void TapEnter()
        {
        }
    }
}

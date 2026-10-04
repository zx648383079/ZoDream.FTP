using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

            EditCommand = new RelayCommand<ISourceEntry>(TapEdit);
            RenameCommand = new RelayCommand<ISourceEntry>(TapRename);
            SwitchCommand = new RelayCommand<ISourceEntry>(TapSwitch);
            DeleteCommand = new RelayCommand<ISourceEntry>(TapDelete);
            ClickCommand = new RelayCommand<ISourceEntry>(TapItem);
        }

        internal ExplorerViewModel(IEntryExplorer source, bool isRemote, WorkspaceViewModel host)
            : this()
        {
            Container = source;
            _host = host;
            IsRemote = isRemote;
        }

        private CancellationTokenSource _tokenSource = new();
        private WorkspaceViewModel? _host;
        /// <summary>
        /// 跳转历史
        /// </summary>
        private readonly List<string> _routeItems = new(10);

        private string _routePath = string.Empty;

        public string RoutePath {
            get => _routePath;
            set => SetProperty(ref _routePath, value);
        }


        public IEntryExplorer Container { get; internal set; }

        public bool IsRemote { get; private set; }

        private AsyncObservableCollection<ISourceEntry> _items = [];

        public AsyncObservableCollection<ISourceEntry> Items {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        private bool _canGoBack;

        public bool CanGoBack {
            get => _canGoBack; 
            set => SetProperty(ref _canGoBack, value);
        }
        private bool _canGoHome;

        public bool CanGoHome {
            get => _canGoHome;
            set => SetProperty(ref _canGoHome, value);
        }



        public ICommand HomeCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand GoCommand { get; private set; }

        public ICommand SwitchCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand RenameCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ClickCommand { get; private set; }

        private async void TapBack()
        {
            if (!CanGoBack)
            {
                return;
            }
            if (!Container.TryGetPrevious(new DirectoryEntry(RoutePath), out var entry))
            {
                return;
            }
            if (!Items.IsPaused)
            {
                _tokenSource.Cancel();
            }
            await LoadAsync(entry.FullPath);
        }

        private async void TapHome()
        {
            if (!Items.IsPaused)
            {
                _tokenSource.Cancel();
            }
            await LoadAsync(Container.HomeEntry.FullPath);
        }
        private async void TapRefresh()
        {
            if (!Items.IsPaused)
            {
                _tokenSource.Cancel();
                return;
            }
            await LoadAsync(RoutePath);
        }
        private async void TapEnter()
        {
            if (!Items.IsPaused)
            {
                _tokenSource.Cancel();
            }
            await LoadAsync(RoutePath);
        }
        #region 操作具体的
        private void TapEdit(ISourceEntry? entry)
        {
            if (entry is null)
            {
                return;
            }
        }
        private void TapRename(ISourceEntry? entry)
        {
            if (entry is null)
            {
                return;
            }
        }

        private void TapDelete(ISourceEntry? entry)
        {
            if (entry is null)
            {
                return;
            }

        }

        private void TapSwitch(ISourceEntry? entry)
        {
            if (entry is null)
            {
                return;
            }
            _host?.SwitchEntry(entry, IsRemote);
        }

        private async void TapItem(ISourceEntry? entry)
        {
            if (entry is null)
            {
                return;
            }
            if (!entry.IsDirectory)
            {
                return;
            }
            await LoadAsync(entry.FullPath);
        }
        #endregion


        public void Reset()
        {
            foreach (var item in Items)
            {
                if (item is EntryViewModel o)
                {
                    o.CompareStatus = EntryCompareStatus.None;
                }
            }
        }

        public async Task LoadAsync(string path)
        {
            if (Container is null)
            {
                return;
            }
            RoutePath = path;
            _tokenSource.Cancel();
            Items.Clear();
            Items.Start();
            _tokenSource = new();
            var res = await Container.OpenAsync(new DirectoryEntry(path), _tokenSource.Token);
            if (res is DirectoryEntryStream fs)
            {
                AddEntry(fs.Items);
                CanGoBack = fs.CanGoBack;
                CanGoHome = false;
            }
            Items.Stop();
        }

        private void AddEntry(IEnumerable<ISourceEntry> items)
        {
            foreach (var item in items)
            {
                if (item is TopDirectoryEntry)
                {
                    continue;
                }
                Items.Add(new EntryViewModel(item)
                {
                    Host = this
                });
            }
        }

        public bool ContainsName(string name)
        {
            foreach (var item in Items)
            {
                if (item.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public ISourceEntry CreateEntry(string name, bool isDirectory)
        {
            if (isDirectory)
            {
                return new DirectoryEntry(name);
            }
            return new FileEntry(name);
        }

    }
}

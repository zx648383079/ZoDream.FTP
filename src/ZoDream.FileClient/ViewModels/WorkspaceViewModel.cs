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
    internal class WorkspaceViewModel : BindableBase
    {
        public WorkspaceViewModel()
        {
            OpenCommand = new StandardUICommand(StandardUICommandKind.Open)
            {
                Command = new RelayCommand(TapOpen)
            };
            StopCommand = new StandardUICommand(StandardUICommandKind.Stop)
            {
                Command = new RelayCommand(TapStop)
            };
            NewCommand = new RelayCommand(TapNew);
            _service = new EntryService(App.ViewModel.Logger);
            LocalStorage = new(new StorageExplorer(_service));
            RemoteStorage = new(new FtpExplorer(_service));
        }

        private readonly IEntryService _service;
        public ExplorerViewModel LocalStorage { get; private set; }
        public ExplorerViewModel RemoteStorage { get; private set; }

        private bool _isLocked;

        public bool IsLocked {
            get => _isLocked;
            set => Set(ref _isLocked, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TapOpen(object? _)
        {
        }

        private void TapNew(object? _)
        {
        }

        private void TapStop(object? _)
        {
        }
    }
}

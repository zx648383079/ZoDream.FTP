using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;
using ZoDream.FileClient.Controls;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Logging;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    internal class WorkspaceViewModel : ObservableObject, IDisposable
    {
        public WorkspaceViewModel()
        {
            OpenCommand = UICommand.Open(TapOpen);
            StopCommand = UICommand.Stop(TapStop);
            NewCommand = UICommand.Add(TapNew);
            _logger = App.ViewModel.Logger;
            _service = new EntryService(_logger);
            LocalStorage = new(new StorageExplorer(_service));
            RemoteStorage = new(new FtpExplorer(_service));
        }

        private readonly ILogger _logger;
        private readonly IEntryService _service;

        public ConsolePanel? Console { get; set; }

        public ExplorerViewModel LocalStorage { get; private set; }
        public ExplorerViewModel RemoteStorage { get; private set; }

        public TransferViewModel Transfer { get; private set; } = new();

        private bool _isLocked;

        public bool IsLocked {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TapOpen()
        {
        }

        private void TapNew()
        {
        }

        private void TapStop()
        {
        }

        public void LoadAsync(ConnectOptions o)
        {
            if (_logger is EventLogger e)
            {
                e.OnLog += Logger_OnLog;
            }
        }

        public void Dispose()
        {
            if (_logger is EventLogger e)
            {
                e.OnLog -= Logger_OnLog;
            }
            Console = null;
        }

        private void Logger_OnLog(string message, LogLevel level)
        {
            if (Console is null)
            {
                return;
            }
            Console.WriteLine(message, level);
        }
    }
}

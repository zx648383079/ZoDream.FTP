using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ZoDream.FileClient.Controls;
using ZoDream.FileClient.Dialogs;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Logging;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    internal class WorkspaceViewModel : ObservableObject, IDisposable
    {
        public WorkspaceViewModel()
        {
            OpenCommand = UICommand.Open(new RelayCommand<string>(TapOpen));
            StopCommand = UICommand.Stop(TapStop);
            NewCommand = UICommand.Add(TapNew);
            _logger = App.ViewModel.Logger;
            _service = new EntryService(_logger);
            LocalStorage = new(new StorageExplorer(_service), false, this);
            RemoteStorage = new(new FtpExplorer(_service), true, this);
        }
        private readonly AppViewModel _app = App.ViewModel;
        private readonly ILogger _logger;
        private readonly IEntryService _service;
        private ConnectOptions _options = new();

        public ConsolePanel? Console { get; set; }

        public ExplorerViewModel LocalStorage { get; private set; }
        public ExplorerViewModel RemoteStorage { get; private set; }

        public TransferViewModel Transfer { get; private set; } = new();

        private bool _isLocked;

        public bool IsLocked {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private bool _remoteIsEmpty = true;

        public bool RemoteIsEmpty {
            get => _remoteIsEmpty;
            set => SetProperty(ref _remoteIsEmpty, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TapOpen(string? arg)
        {
            if (!Enum.TryParse<ProtocolType>(arg, out var protocol))
            {
                protocol = ProtocolType.FTP;
            }
            switch (protocol)
            {
                case ProtocolType.Local:
                    OpenLocal();
                    break;
                default:
                    OpenFtp();
                    break;
            }
        }

        private async void OpenFtp()
        {
            var picker = new ConnectDialog();
            picker.ViewModel.Load(_options);
            if (!await _app.OpenFormAsync(picker))
            {
                return;
            }
            picker.ViewModel.Unload(_options);
            await LoadRemoteAsync(_options);
        }

        private async void OpenLocal()
        {
            var picker = _app.PickFolder();
            var res = await picker.PickSingleFolderAsync();
            if (res is null || res.Path == _options.LocalPath)
            {
                return;
            }
            _options.RemotePath = res.Path;
            _options.RemoteProtocol = ProtocolType.Local;
            await LoadRemoteAsync(_options);
        }

        private void TapNew()
        {
        }

        private void TapStop()
        {
        }

        public async Task LoadAsync(ConnectOptions options)
        {
            _options = options;
            await LocalStorage.LoadAsync(options.LocalPath);
            await LoadRemoteAsync(options);
            if (_logger is EventLogger e)
            {
                e.OnLog += Logger_OnLog;
            }
        }

        private async Task LoadRemoteAsync(ConnectOptions options)
        {
            if (options.RemoteProtocol == ProtocolType.FTP && string.IsNullOrEmpty(options.RemoteHost))
            {
                RemoteIsEmpty = true;
            }
            else
            {
                RemoteIsEmpty = false;
                var container = Create(options);
                await container.ConnectAsync(options);
                RemoteStorage.Container = container;
                await RemoteStorage.LoadAsync(string.IsNullOrEmpty(options.RemotePath) ? "/" : options.RemotePath);
            }
        }

        public void Dispose()
        {
            if (_logger is EventLogger e)
            {
                e.OnLog -= Logger_OnLog;
            }
            LocalStorage.Container.Dispose();
            RemoteStorage.Container.Dispose();
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

        public void SwitchEntry(ISourceEntry entry, bool isRemote)
        {
            var item = new TransferItemViewModel(Transfer)
            {
                Type = isRemote ? TransferType.RemoteToLocal : TransferType.LocalToRemote
            };
            if (isRemote)
            {
                if (LocalStorage.ContainsName(entry.Name))
                {
                    return;
                }
                item.LocalEntry = LocalStorage.CreateEntry(entry.Name, entry.IsDirectory);
                item.RemoteEntry = entry;
            } else
            {
                if (RemoteStorage.ContainsName(entry.Name))
                {
                    return;
                }
                item.RemoteEntry = LocalStorage.CreateEntry(entry.Name, entry.IsDirectory);
                item.LocalEntry = entry;
            }
            Transfer.Add(item);
        }

        public void Compare(EntryCompareFlags flag)
        {
            if (flag == EntryCompareFlags.None)
            {
                return;
            }
            var comparer = new EntryEqualityComparer(flag);
            var localItems = new HashSet<ISourceEntry>(LocalStorage.Items, comparer);
            var remoteItems = new HashSet<ISourceEntry>(RemoteStorage.Items, comparer);
            Compare(LocalStorage.Items, remoteItems);
            Compare(RemoteStorage.Items, localItems);
        }

        private static void Compare(IEnumerable<ISourceEntry> items, HashSet<ISourceEntry> entries)
        {
            foreach (var item in items)
            {
                if (item is not EntryViewModel o)
                {
                    continue;
                }
                o.CompareStatus = EntryCompareStatus.Compared;
                if (!entries.Contains(item))
                {
                    o.CompareStatus |= EntryCompareStatus.DiffContent;
                }
            }
        }

        public IEntryExplorer Create(IConnectOptions options)
        {
            return options.RemoteProtocol switch
            {
                ProtocolType.FTP or ProtocolType.SFTP => new FtpExplorer(_service),
                ProtocolType.STORJ => throw new NotImplementedException(),
                ProtocolType.SMBv1 or ProtocolType.SMBv2 or ProtocolType.SMBv3 => new SMBExplorer(_service),
                ProtocolType.WebDAV => new WebDAVExplorer(_service),
                ProtocolType.AFP => throw new NotImplementedException(),
                ProtocolType.NFS => throw new NotImplementedException(),
                ProtocolType.Socket => new SocketExplorer(_service),
                ProtocolType.Local => new StorageExplorer(_service, true),
                _ => throw new NotImplementedException()
            };
        }
    }
}

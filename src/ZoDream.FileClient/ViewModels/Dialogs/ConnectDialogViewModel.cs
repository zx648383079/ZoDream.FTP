using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    public class ConnectDialogViewModel : ObservableObject, IFormValidator
    {
        public ConnectDialogViewModel()
        {
            LocalCommand = new RelayCommand(TapLocal);
        }

        public string[] ProtocolItems { get; private set; } = ["FTP", "SFTP"];
        public string[] MethodItems { get; private set; } = ["匿名", "账号", "询问"];

        private int _protocolIndex;

        public int ProtocolIndex {
            get => _protocolIndex;
            set => SetProperty(ref _protocolIndex, value);
        }

        private int _methodIndex = 1;

        public int MethodIndex {
            get => _methodIndex;
            set => SetProperty(ref _methodIndex, value);
        }


        private string _host = string.Empty;

        public string Host {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        private int _port = 21;

        public int Port {
            get => _port;
            set {
                if (value is 21 or 22)
                {
                    ProtocolIndex = value == 22 ? 1 : 0;
                }
                SetProperty(ref _port, value);
            }
        }

        private string _account = "root";

        public string Account {
            get => _account;
            set => SetProperty(ref _account, value);
        }


        private string _password = string.Empty;

        public string Password {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _localEntrance = string.Empty;

        public string LocalEntrance {
            get => _localEntrance;
            set => SetProperty(ref _localEntrance, value);
        }

        private string _remoteEntrance = string.Empty;

        public string RemoteEntrance {
            get => _remoteEntrance;
            set => SetProperty(ref _remoteEntrance, value);
        }

        private bool _openSync;

        public bool OpenSync {
            get => _openSync;
            set => SetProperty(ref _openSync, value);
        }

        private bool _openDiff;

        public bool OpenDiff {
            get => _openDiff;
            set => SetProperty(ref _openDiff, value);
        }


        public bool IsValid => string.IsNullOrWhiteSpace(Host);

        public ICommand LocalCommand { get; private set; }

        private async void TapLocal()
        {
            var picker = App.ViewModel.PickFolder();
            var items = await picker.PickSingleFolderAsync();
            if (items is null)
            {
                return;
            }
            LocalEntrance = items.Path;
        }

        public void Load(ConnectOptions options)
        {
            Host = options.RemoteHost;
            Port = options.RemotePort;

            MethodIndex = (int)options.RemoteAccess;
            ProtocolIndex = (int)options.RemoteProtocol;

            Account = options.RemoteUser;
            Password = options.RemotePassword;

            LocalEntrance = options.LocalPath;
            RemoteEntrance = options.RemotePath;

            OpenDiff = options.EnabledDiff;
            OpenSync = options.EnabledSync;
        }

        public void Unload(ConnectOptions options) 
        {
            options.RemoteHost = Host;
            options.RemotePort = Port;

            options.RemoteAccess = (AccessType)MethodIndex;
            options.RemoteProtocol = (ProtocolType)ProtocolIndex;

            options.RemoteUser = Account;
            options.RemotePassword = Password;

            options.LocalPath = LocalEntrance;
            options.RemotePath = RemoteEntrance;

            options.EnabledDiff = OpenDiff;
            options.EnabledSync = OpenSync;
        }
    }
}

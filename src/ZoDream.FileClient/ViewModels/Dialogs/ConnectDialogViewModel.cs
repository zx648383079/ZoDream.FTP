using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using ZoDream.Shared.ViewModel;

namespace ZoDream.FileClient.ViewModels
{
    public class ConnectDialogViewModel : BindableBase, IFormValidator
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
            set => Set(ref _protocolIndex, value);
        }

        private int _methodIndex = 1;

        public int MethodIndex {
            get => _methodIndex;
            set => Set(ref _methodIndex, value);
        }


        private string _host = string.Empty;

        public string Host {
            get => _host;
            set => Set(ref _host, value);
        }

        private int _port = 21;

        public int Port {
            get => _port;
            set {
                if (value is 21 or 22)
                {
                    ProtocolIndex = value == 22 ? 1 : 0;
                }
                Set(ref _port, value);
            }
        }

        private string _account = "root";

        public string Account {
            get => _account;
            set => Set(ref _account, value);
        }


        private string _password = string.Empty;

        public string Password {
            get => _password;
            set => Set(ref _password, value);
        }

        private string _localEntrance = string.Empty;

        public string LocalEntrance {
            get => _localEntrance;
            set => Set(ref _localEntrance, value);
        }

        private string _remoteEntrance = string.Empty;

        public string RemoteEntrance {
            get => _remoteEntrance;
            set => Set(ref _remoteEntrance, value);
        }

        private bool _openSync;

        public bool OpenSync {
            get => _openSync;
            set => Set(ref _openSync, value);
        }

        private bool _openDiff;

        public bool OpenDiff {
            get => _openDiff;
            set => Set(ref _openDiff, value);
        }


        public bool IsValid => string.IsNullOrWhiteSpace(Host);

        public ICommand LocalCommand { get; private set; }

        private async void TapLocal(object? _)
        {
            var picker = new FolderPicker();
            picker.FileTypeFilter.Add("*");
            App.ViewModel.InitializePicker(picker);
            var items = await picker.PickSingleFolderAsync();
            if (items is null)
            {
                return;
            }
            LocalEntrance = items.Path;
        }
    }
}

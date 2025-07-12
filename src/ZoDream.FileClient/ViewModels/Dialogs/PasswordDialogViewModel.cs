using CommunityToolkit.Mvvm.ComponentModel;

namespace ZoDream.FileClient.ViewModels
{
    public class PasswordDialogViewModel : ObservableObject
    {

        private string _host = string.Empty;

        public string Host {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        private int _port;

        public int Port {
            get => _port;
            set => SetProperty(ref _port, value);
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

    }
}

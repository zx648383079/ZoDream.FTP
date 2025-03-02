using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Shared.ViewModel;

namespace ZoDream.FileClient.ViewModels
{
    public class PasswordDialogViewModel : BindableBase
    {

        private string _host = string.Empty;

        public string Host {
            get => _host;
            set => Set(ref _host, value);
        }

        private int _port;

        public int Port {
            get => _port;
            set => Set(ref _port, value);
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

    }
}

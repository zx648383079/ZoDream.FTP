using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using ZoDream.FileClient.Dialogs;
using ZoDream.FileClient.Pages;
using ZoDream.Shared.ViewModel;

namespace ZoDream.FileClient.ViewModels
{
    internal class StartupViewModel: BindableBase
    {

        public StartupViewModel()
        {
            OpenCommand = new RelayCommand(TapOpen);
            CreateCommand = new RelayCommand(TapCreate);
            version = _app.Version;
        }

        private readonly AppViewModel _app = App.ViewModel;

        private string version;

        public string Version {
            get => version;
            set => Set(ref version, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }


        private async void TapOpen(object? _)
        {
            var picker = new HistoryDialog();
            if (!await _app.OpenFormAsync(picker))
            {
                return;
            }
            _app.Navigate<WorkspacePage>();
        }
        
        private async void TapCreate(object? _)
        {
            var picker = new ConnectDialog();
            if (!await _app.OpenFormAsync(picker))
            {
                return;
            }
            _app.Navigate<WorkspacePage>();
        }
    }
}

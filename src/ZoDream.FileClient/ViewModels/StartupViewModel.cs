using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using ZoDream.FileClient.Dialogs;
using ZoDream.FileClient.Pages;

namespace ZoDream.FileClient.ViewModels
{
    internal class StartupViewModel: ObservableObject
    {

        public StartupViewModel()
        {
            OpenCommand = new RelayCommand(TapOpen);
            CreateCommand = new RelayCommand(TapCreate);
            EmptyCommand = new RelayCommand(TapEmpty);
            version = _app.Version;
        }

        private readonly AppViewModel _app = App.ViewModel;

        private string version;

        public string Version {
            get => version;
            set => SetProperty(ref version, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }

        public ICommand EmptyCommand { get; private set;  }


        private async void TapOpen()
        {
            var picker = new HistoryDialog();
            if (!await _app.OpenFormAsync(picker))
            {
                return;
            }
            _app.Navigate<WorkspacePage>();
        }
        
        private async void TapCreate()
        {
            var picker = new ConnectDialog();
            if (!await _app.OpenFormAsync(picker))
            {
                return;
            }
            var options = new ConnectOptions();
            picker.ViewModel.Unload(options);
            _app.Navigate<WorkspacePage>(options);
        }

        private async void TapEmpty()
        {
            var picker = _app.PickFolder();
            var res = await picker.PickSingleFolderAsync();
            if (res is null)
            {
                return;
            }
            _app.Navigate<WorkspacePage>(new ConnectOptions()
            {
                LocalPath = res.Path
            });
        }
    }
}

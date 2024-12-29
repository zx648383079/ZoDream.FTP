using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
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
            version = App.ViewModel.Version;
        }

        private string version;

        public string Version {
            get => version;
            set => Set(ref version, value);
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }


        private async void TapOpen(object? _)
        {
            var picker = new FileOpenPicker();
            //foreach (var ext in ReaderFactory.FileFilterItems)
            //{
            //    picker.FileTypeFilter.Add(ext);
            //}
            picker.FileTypeFilter.Add("*");
            App.ViewModel.InitializePicker(picker);
            var items = await picker.PickSingleFileAsync();
            if (items is null)
            {
                return;
            }
            App.ViewModel.Navigate<WorkspacePage>(items);
        }
        
        private void TapCreate(object? _)
        {
            App.ViewModel.Navigate<WorkspacePage>();
        }
    }
}

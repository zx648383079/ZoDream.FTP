using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZoDream.FileClient.Controls;

namespace ZoDream.FileClient.ViewModels
{
    public class TransferViewModel : ObservableObject
    {
        public TransferViewModel()
        {
            DeleteCommand = UICommand.Delete(new RelayCommand<TransferItemViewModel>(TapDelete));
            PlayCommand = UICommand.Play(new RelayCommand<TransferItemViewModel>(TapPlay));
            StopCommand = UICommand.Stop(new RelayCommand<TransferItemViewModel>(TapStop));
        }

        private ObservableCollection<TransferItemViewModel> _items = [];

        public ObservableCollection<TransferItemViewModel> Items {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private TransferItemViewModel? _selectedItem;

        public TransferItemViewModel? SelectedItem {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }


        public ICommand DeleteCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TapDelete(TransferItemViewModel? arg)
        {
            arg ??= SelectedItem;
            if (arg is null)
            {
                return;
            }
            var items = new TransferItemViewModel[] { arg };
            foreach (var item in items)
            {
                item.StopCommand.Execute(null);
                Items.Remove(item);
            }
            SelectedItem = null;
        }
        private void TapPlay(TransferItemViewModel? arg)
        {
            arg ??= SelectedItem;
            if (arg is null)
            {
                return;
            }
            var items = new TransferItemViewModel[] { arg };
            foreach (var item in items)
            {
                if (item.Status is TransferStatus.Sending or TransferStatus.Receiving)
                {
                    continue;
                }

            }
        }

        private void TapStop(TransferItemViewModel? arg)
        {
            arg ??= SelectedItem;
            if (arg is null)
            {
                return;
            }
            var items = new TransferItemViewModel[] { arg };
            foreach (var item in items)
            {
                if (item.Status >= TransferStatus.Completed)
                {
                    continue;
                }
                item.StopCommand.Execute(null);
            }
        }
    }
}

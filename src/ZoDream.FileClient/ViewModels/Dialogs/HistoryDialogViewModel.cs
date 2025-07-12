using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class HistoryDialogViewModel : ObservableObject, IFormValidator
    {

        private string _keywords = string.Empty;

        public string Keywords {
            get => _keywords;
            set => SetProperty(ref _keywords, value);
        }

        private ObservableCollection<IConnectRecord> _items = [];

        public ObservableCollection<IConnectRecord> Items {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private int _selectedIndex;

        public int SelectedIndex {
            get => _selectedIndex;
            set {
                SetProperty(ref _selectedIndex, value);
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public bool IsValid => SelectedIndex >= 0 && SelectedIndex < Items.Count;
    }
}

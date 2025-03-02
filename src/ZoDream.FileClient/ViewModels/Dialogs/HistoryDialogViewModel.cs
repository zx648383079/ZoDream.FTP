using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.ViewModel;

namespace ZoDream.FileClient.ViewModels
{
    public class HistoryDialogViewModel : BindableBase, IFormValidator
    {

        private string _keywords = string.Empty;

        public string Keywords {
            get => _keywords;
            set => Set(ref _keywords, value);
        }

        private ObservableCollection<IConnectRecord> _items = [];

        public ObservableCollection<IConnectRecord> Items {
            get => _items;
            set => Set(ref _items, value);
        }

        private int _selectedIndex;

        public int SelectedIndex {
            get => _selectedIndex;
            set {
                Set(ref _selectedIndex, value);
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public bool IsValid => SelectedIndex >= 0 && SelectedIndex < Items.Count;
    }
}

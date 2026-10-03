using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class AsyncObservableCollection<T> : ObservableCollection<T>, IAsyncObservableCollection<T>
    {
        private bool _isPaused  = true;

        public bool IsPaused { 
            get => _isPaused; 
            private set {
                _isPaused = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsPaused)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(InProgress)));
            } 
        }

        public bool InProgress => !IsPaused;

        public void Start()
        {
            IsPaused = false;
        }

        public void Stop()
        {
            IsPaused = true;
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }
    }
}

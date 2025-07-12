using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading;
using System.Windows.Input;
using ZoDream.FileClient.Controls;
using ZoDream.FileClient.Converters;

namespace ZoDream.FileClient.ViewModels
{
    public class TransferItemViewModel : ObservableObject
    {
        public TransferItemViewModel(TransferViewModel host)
        {
            _host = host;
            PlayCommand = UICommand.Play(TapPlay);
            StopCommand = UICommand.Stop(TapStop);
            DeleteCommand = UICommand.Delete(TapDelete);
        }

        private readonly TransferViewModel _host;
        private CancellationTokenSource _tokenSource = new();
        private readonly Bandwidth _bandwidth = new();

        public CancellationToken Token => _tokenSource.Token;

        private string _localName = string.Empty;

        public string LocalName {
            get => _localName;
            set => SetProperty(ref _localName, value);
        }

        private string _localPath = string.Empty;

        public string LocalPath {
            get => _localPath;
            set => SetProperty(ref _localPath, value);
        }

        private string _remoteName = string.Empty;

        public string RemoteName {
            get => _remoteName;
            set => SetProperty(ref _remoteName, value);
        }

        private string _remotePath = string.Empty;

        public string RemotePath {
            get => _remotePath;
            set => SetProperty(ref _remotePath, value);
        }

        private TransferType _type = TransferType.LocalToRemote;

        public TransferType Type {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private TransferStatus _status = TransferStatus.None;

        public TransferStatus Status {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private long _length;

        public long Length {
            get => _length;
            set => SetProperty(ref _length, value);
        }

        private long _value;

        public long Value {
            get => _value;
            set => SetProperty(ref _value, value);
        }


        public long Speed => (long)_bandwidth.Speed;

        public int ElapsedTime => Speed > 0 ? (int)((Length - Value) / Speed) : 0;

        private DateTime _lastModified = DateTime.MinValue;

        public DateTime LastModified {
            get => _lastModified;
            set => SetProperty(ref _lastModified, value);
        }

        private double _progress;

        public double Progress {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }


        private string _message = string.Empty;

        public string Message {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand PlayCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        private void TapPlay()
        {
            _tokenSource = new();
            _host.PlayCommand.Execute(this);
        }
        private void TapStop()
        {
            _tokenSource.Cancel();
            Status = TransferStatus.Cancelled;
        }
        private void TapDelete()
        {
            _host.DeleteCommand.Execute(this);
        }


        public void RequestChanged()
        {
            LastModified = DateTime.Now;
            Message = ConverterHelper.FormatSpeed(this);
        }

        public void ProgressChanged(long received)
        {
            Value = received;
            _bandwidth.CalculateSpeed(Value);
            Progress = Length > 0 ? (double)Value * 100 / Length : 0;
            Message = ConverterHelper.FormatSpeed(this);
        }
    }

    public enum TransferType : byte
    {
        LocalToRemote,
        RemoteToLocal,
    }

    public enum TransferStatus : byte
    {
        None,
        Waiting,
        Sending,
        Receiving,
        Working,
        Paused,
        Completed,      // 已完成
        Failed,         // 失败
        Cancelled,       // 已取消
    }
}

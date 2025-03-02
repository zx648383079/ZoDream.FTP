using FluentFTP;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    internal class FtpLogger : IFtpLogger
    {
        public void Log(FtpLogEntry entry)
        {
            App.ViewModel.Logger.Log(entry.Severity switch
            {
                FtpTraceLevel.Verbose => LogLevel.NotSet,
                FtpTraceLevel.Info => LogLevel.Info,
                FtpTraceLevel.Warn => LogLevel.Warn,
                FtpTraceLevel.Error => LogLevel.Error,
                _ => LogLevel.Info
            }, entry.Message);
        }
    }
}

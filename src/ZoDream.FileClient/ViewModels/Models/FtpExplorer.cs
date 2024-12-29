using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels.Models
{
    public class FtpExplorer(IEntryService service) : IEntryExplorer
    {

        public bool CanGoBack { get; }

  

        public Task<IEntryStream> OpenAsync(ISourceEntry entry)
        {
            return Task.FromResult<IEntryStream>(UnknownEntryStream.Instance);
        }

        public void Dispose()
        {
        }
    }
}

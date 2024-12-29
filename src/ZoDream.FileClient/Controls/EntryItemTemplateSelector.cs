using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using ZoDream.FileClient.ViewModels;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.Controls
{
    public class EntryItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? DefaultTemplate { get; set; }
        public DataTemplate? DirectoryTemplate { get; set; }
        public DataTemplate? BackTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is TopDirectoryEntry)
            {
                return BackTemplate;
            }
            if (item is ISourceEntry e)
            {
                return e.IsDirectory ? DirectoryTemplate : DefaultTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}

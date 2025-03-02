using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using ZoDream.FileClient.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoDream.FileClient.Controls
{
    public sealed class ExplorerView : Control
    {
        public ExplorerView()
        {
            this.DefaultStyleKey = typeof(ExplorerView);
        }

        #region 属性


        public string RoutePath {
            get { return (string)GetValue(RoutePathProperty); }
            set { SetValue(RoutePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoutePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoutePathProperty =
            DependencyProperty.Register("RoutePath", typeof(string), typeof(ExplorerView), new PropertyMetadata(string.Empty));

        public IEnumerable<EntryViewModel> ItemsSource {
            get { return (IEnumerable<EntryViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<EntryViewModel>), typeof(ExplorerView), new PropertyMetadata(null));



        public bool HomeEnabled {
            get { return (bool)GetValue(HomeEnabledProperty); }
            set { SetValue(HomeEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HomeEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeEnabledProperty =
            DependencyProperty.Register("HomeEnabled", typeof(bool), typeof(ExplorerView), new PropertyMetadata(false));



        public Visibility HomeVisible {
            get { return (Visibility)GetValue(HomeVisibleProperty); }
            set { SetValue(HomeVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HomeVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeVisibleProperty =
            DependencyProperty.Register("HomeVisible", typeof(Visibility), typeof(ExplorerView), new PropertyMetadata(Visibility.Collapsed));



        public bool BackEnabled {
            get { return (bool)GetValue(BackEnabledProperty); }
            set { SetValue(BackEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackEnabledProperty =
            DependencyProperty.Register("BackEnabled", typeof(bool), typeof(ExplorerView), new PropertyMetadata(false));



        public Visibility BackVisible {
            get { return (Visibility)GetValue(BackVisibleProperty); }
            set { SetValue(BackVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackVisibleProperty =
            DependencyProperty.Register("BackVisible", typeof(Visibility), typeof(ExplorerView), new PropertyMetadata(Visibility.Collapsed));


        


        public ICommand HomeCommand {
            get { return (ICommand)GetValue(HomeCommandProperty); }
            set { SetValue(HomeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HomeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeCommandProperty =
            DependencyProperty.Register("HomeCommand", typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));



        public ICommand BackCommand {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));




        public ICommand RefreshCommand {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefreshCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));




        public ICommand GoCommand {
            get { return (ICommand)GetValue(GoCommandProperty); }
            set { SetValue(GoCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoCommandProperty =
            DependencyProperty.Register("GoCommand", typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));

        #endregion
    }
}

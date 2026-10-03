using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using ZoDream.FileClient.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoDream.FileClient.Controls
{
    [TemplatePart(Name = RouteElementName, Type = typeof(TextBox))]
    [TemplatePart(Name = ListElementName, Type = typeof(ListView))]
    public sealed class ExplorerView : Control
    {
        const string RouteElementName = "PART_RouteTb";
        const string ListElementName = "PART_ListBox";
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



        public string RefreshIcon {
            get { return (string)GetValue(RefreshIconProperty); }
            set { SetValue(RefreshIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefreshIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshIconProperty =
            DependencyProperty.Register(nameof(RefreshIcon), typeof(string), typeof(ExplorerView), new PropertyMetadata(string.Empty));




        public ICommand GoCommand {
            get { return (ICommand)GetValue(GoCommandProperty); }
            set { SetValue(GoCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoCommandProperty =
            DependencyProperty.Register("GoCommand", typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));




        public ICommand ItemClickCommand {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register(nameof(ItemClickCommand), typeof(ICommand), typeof(ExplorerView), new PropertyMetadata(null));



        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild(RouteElementName) is TextBox input)
            {
                input.KeyDown += Input_KeyDown;
            }
            if (GetTemplateChild(ListElementName) is ListView control)
            {
                control.DoubleTapped += Control_DoubleTapped;
            }
        }

        private void Control_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (sender is not Selector s)
            {
                return;
            }
            if (s.SelectedItem is null)
            {
                return;
            }
            ItemClickCommand?.Execute(s.SelectedItem);
        }

        private void Input_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                GoCommand?.Execute(null);
            }
        }
    }
}

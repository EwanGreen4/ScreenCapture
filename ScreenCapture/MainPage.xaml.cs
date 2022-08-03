using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.AccessCache;
using Windows.Graphics.Display;
using Windows.Graphics.Display.Core;
using Windows.UI;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using System.Collections.ObjectModel;

using System.Text.Json;
using System.Diagnostics;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


namespace ScreenCapture {

    public sealed partial class MainPage : Page {

        public SettingsPage PageSettings = new SettingsPage();
        public GalleryPage PageGallery = new GalleryPage();
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args) {
            // Get the size of the caption controls and set padding.
            //  LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
            // RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args) {
            if(sender.IsVisible) {
                AppTitleBar.Visibility = Visibility.Visible;
            } else {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args) {
            //UISettings settings = new UISettings();
            //if(args.WindowActivationState == CoreWindowActivationState.Deactivated) {
            //    AppTitleTextBlock.Foreground =
            //       new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
            //}
            //else {
            //    AppTitleTextBlock.Foreground =
            //       new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
            //}
        }

        public MainPage() {
            this.InitializeComponent();

            CoreApplicationViewTitleBar coreTitleBar =
            CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar =
            ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            Window.Current.SetTitleBar(AppTitleBar);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;

            MainNavView.SelectedItem = CapturePage;
        }

        private Stack<Microsoft.UI.Xaml.Controls.NavigationViewItemBase> NavigationHistory = new Stack<Microsoft.UI.Xaml.Controls.NavigationViewItemBase>();

        private void pollNavigation(String str) {
            switch(str) {
                case "Settings":
                    CurrentPageControl.Content = PageSettings;
                    break;
                case "Capture Gallery":
                    CurrentPageControl.Content = PageGallery;
                    break;
                default:
                    CurrentPageControl.Content = null;
                    break;

            }
            MainNavView.Header = str;
        }
        private void MainNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args) {
            var item = args.SelectedItemContainer;
            NavigationHistory.Push(item);
            pollNavigation(item.Content.ToString());

            MainNavView.IsBackEnabled = NavigationHistory.Count > 1;
            if(MainNavView.IsBackEnabled) EnterStoryboard.Begin();
        }

        private void MainNavView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args) {
            NavigationHistory.Pop();
            var item = NavigationHistory.Pop();
            MainNavView.SelectedItem = item;
            ExitStoryboard.Begin();
        }
    }
}

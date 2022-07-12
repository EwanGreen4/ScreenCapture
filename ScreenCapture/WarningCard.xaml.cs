using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml.Media.Animation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class WarningCard : UserControl {
        public ObservableCollection<string> Strings { get; set; } = new ObservableCollection<string>();

        private double OldHeight { get; set; }
        private double NewHeight { get; set; }

        public void UpdateText(object sender, NotifyCollectionChangedEventArgs e) {
            if(Strings.Count == 0) {
                if(Visibility != Visibility.Collapsed) {
                    ExitStoryboard.Begin();
                }
                return;
            }
            else {
                if(Visibility != Visibility.Visible) {
                    Visibility = Visibility.Visible;
                    EnterStoryboard.Begin();
                }
                
                ContentPanel.Children.Clear();
                foreach(string i in Strings) {
                    TextBlock textBlock = new TextBlock {
                        Text = i
                    };
                    ContentPanel.Children.Add(textBlock);
                }
            }
        }

        public WarningCard() {
            InitializeComponent();
        }

        private void Card_Loaded(object sender, RoutedEventArgs e) {
            Strings.CollectionChanged += UpdateText;
            ExitStoryboard.Completed += ExitStoryboard_Completed;
        }

        private void ExitStoryboard_Completed(object sender, object e) {
            Visibility = Visibility.Collapsed;
        }

        private void Card_SizeChanged(object sender, SizeChangedEventArgs e) {
            if(e.PreviousSize.Height != e.NewSize.Height) {
                OldHeight = e.PreviousSize.Height;
                NewHeight = e.NewSize.Height;
                ResizeStoryboard.Begin();
            }
        }
    }
}

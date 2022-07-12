using Microsoft.UI.Xaml.Controls;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class ExpanderSettingsCard : UserControl {

        public object HeaderInnards {
            get => (object)GetValue(HeaderInnardsDependency);
            set => SetValue(HeaderInnardsDependency, value);
        }
        public static readonly DependencyProperty HeaderInnardsDependency =
            DependencyProperty.Register(nameof(HeaderInnards), typeof(object), typeof(ExpanderSettingsCard), null);

        public DataTemplate HeaderInnardsTemplate { get; set; }
        public DataTemplateSelector HeaderInnardsTemplateSelector { get; set; }

        public object ExpanderInnards {
            get => (object)GetValue(ExpanderInnardsDependency);
            set => SetValue(ExpanderInnardsDependency, value);
        }
        public static readonly DependencyProperty ExpanderInnardsDependency =
            DependencyProperty.Register(nameof(ExpanderInnards), typeof(object), typeof(ExpanderSettingsCard), null);

        public DataTemplate ExpanderInnardsTemplate { get; set; }
        public DataTemplateSelector ExpanderInnardsTemplateSelector { get; set; }

        private bool Expanded = false;

        public ExpanderSettingsCard() {
            this.InitializeComponent();
            ExitStoryboard.Completed += ExitStoryboard_Completed;


        }

        private void ExitStoryboard_Completed(object sender, object e) {
            ExpanderInnardsContainer.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if(!Expanded) {
                ExpanderInnardsContainer.Visibility = Visibility.Visible;
                ChevronIcon.Glyph = "\ue70e";
                Button.CornerRadius = Button.CornerRadius with { BottomLeft = 0, BottomRight = 0 };
                EnterStoryboard.Begin();
            }
            else {
                ChevronIcon.Glyph = "\ue70d";
                Button.CornerRadius = Button.CornerRadius with { BottomLeft = 4, BottomRight = 4 };
                ExitStoryboard.Begin();
            }

            Expanded = !Expanded;
        }

    }
}

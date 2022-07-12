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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class SettingsCardInnards : UserControl {

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string IconGlyph { get; set; }

        public object RightControl {
            get => (object)GetValue(RightControlDependency);
            set => SetValue(RightControlDependency, value);
        }

        public static readonly DependencyProperty RightControlDependency =
            DependencyProperty.Register(nameof(RightControl), typeof(object), typeof(SettingsCardInnards), null);

        public DataTemplate RightControlTemplate { get; set; }
        public DataTemplateSelector RightControlTemplateSelector { get; set; }


        public SettingsCardInnards() {
            this.InitializeComponent();
        }
    }
}

using System.Drawing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class StaticSettingsCard : UserControl {


        public object Innards {
            get => (object)GetValue(InnardsDependency);
            set => SetValue(InnardsDependency, value);
        }
        public static readonly DependencyProperty InnardsDependency =
            DependencyProperty.Register(nameof(Innards), typeof(object), typeof(StaticSettingsCard), null);

        public DataTemplate InnardsTemplate { get; set; }
        public DataTemplateSelector InnardsTemplateSelector { get; set; }

        public StaticSettingsCard() {
            this.InitializeComponent();
        }
    }
}

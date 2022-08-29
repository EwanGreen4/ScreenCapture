using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace ScreenCapture {
    public sealed class UnitTextBox : TextBox {
        public enum Affix {
            Prefix, Suffix
        }

        // prefix or suffix text
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register(nameof(Unit), typeof(string), typeof(UnitTextBox), new PropertyMetadata(0));
        // prefix or suffix type
        public static readonly DependencyProperty AffixTypeProperty =
            DependencyProperty.Register(nameof(AffixType), typeof(Affix), typeof(UnitTextBox), new PropertyMetadata(0));

        public string Unit {
            get => (string)this.GetValue(UnitProperty);
            set => this.SetValue(UnitProperty, value);
        }
        public Affix AffixType {
            get => (Affix)this.GetValue(AffixTypeProperty);
            set => this.SetValue(AffixTypeProperty, value);
        }
        

        public UnitTextBox() {
            this.DefaultStyleKey = typeof(UnitTextBox);
            this.TextChanged += UnitTextBox_TextChanged;


        }

        private void UnitTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            string a = AffixType == Affix.Prefix ? Text : Unit;
            string b = AffixType == Affix.Prefix ? Unit : Text;
            Text = a + b;
        }
    }
}

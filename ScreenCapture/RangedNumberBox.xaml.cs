using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class RangedNumberBox : UserControl {

        public double Minimum { get; set; } = 0; // these are inclusive
        public double Maximum { get; set; } = 100;
        public string Warning { get; set; } = string.Empty;

        public delegate void NumberValidityEventHandler(double e);
        public event NumberValidityEventHandler ValidNumberChosen;
        public event NumberValidityEventHandler InvalidNumberChosen;

        public WarningCard WarningCard { get; set; }

        public RangedNumberBox() {
            this.InitializeComponent();
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e) {
            TextBox.PlaceholderText = $"{Minimum} - {Maximum}";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            bool valid;
            double Value = double.NaN;
            if(!string.IsNullOrEmpty(TextBox.Text)) {
                 double.TryParse(TextBox.Text, out Value);
                valid = Value != double.NaN && Value >= Minimum && Value <= Maximum;
            }
            else
                valid = false;

            if(valid) {
                Status.Glyph = "\ue73e";
                if(WarningCard.Strings.Contains(Warning))
                    WarningCard.Strings.Remove(Warning);
                ValidNumberChosen?.Invoke(Value);
            }
            else {
                Status.Glyph = "\ue711";
                if(!WarningCard.Strings.Contains(Warning))
                    WarningCard.Strings.Add(Warning);
                InvalidNumberChosen?.Invoke(Value);
            }
        }
    }
}

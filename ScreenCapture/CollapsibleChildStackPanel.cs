// This stack panel implementation has been taken from this gist (https://gist.github.com/tomtom-m/4df733e1fdb68291ed4a5100629c11c4), with minor changes
// Thank you tomtom-m!

// Override of StackPanel which fixes a spacing problem.
// https://github.com/microsoft/microsoft-ui-xaml/issues/916
// The original StackPanel applies spacing to collapsed items.
// This implementation doesn't.

using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ScreenCapture {
    public class CollapsibleChildStackPanel : StackPanel {

        public static readonly DependencyProperty SpaceProperty =
            DependencyProperty.Register(nameof(Space), typeof(int), typeof(CollapsibleChildStackPanel), new PropertyMetadata(0));

        public CollapsibleChildStackPanel() {
            this.Loaded += this.CollapsibleChildStackPanel_Loaded;
        }

        public int Space {
            get => (int)this.GetValue(SpaceProperty);
            set => this.SetValue(SpaceProperty, value);
        }

        private void CollapsibleChildStackPanel_Loaded(object sender, object e)
            => this.SetSpacingForChildren(this.Space);

        private void SetSpacingForChildren(int spacing) {
            FrameworkElement last = null;
            foreach(var it in this.Children) {
                if(it is FrameworkElement element) {

                    if(it.ActualSize.X > 0 && it.Visibility == Visibility.Visible) {
                        
                    } else {

                    }

                    last = element;
                }
            }
        }
    }
}
// This stack panel implementation has been taken from this gist (https://gist.github.com/tomtom-m/4df733e1fdb68291ed4a5100629c11c4), with minor changes
// Thank you tomtom-m!

// Override of StackPanel which fixes a spacing problem.
// https://github.com/microsoft/microsoft-ui-xaml/issues/916
// The original StackPanel applies spacing to collapsed items.
// This implementation doesn't.
 
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            for(int i = 0; i < this.Children.Count; i++) {
                if(this.Children[i] is FrameworkElement element
                    && element.Visibility == Visibility.Visible) {
                    var halfSpacing = spacing / 2;
                    var topSpacing = i == 0 ? 0 : halfSpacing;

                    element.Margin = new Thickness(element.Margin.Left, element.Margin.Top + topSpacing, element.Margin.Right, element.Margin.Bottom + halfSpacing);
                }
            }
        }
    }
}
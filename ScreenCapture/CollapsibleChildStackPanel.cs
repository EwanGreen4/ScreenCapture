// This stack panel implementation has been taken from this gist (https://gist.github.com/tomtom-m/4df733e1fdb68291ed4a5100629c11c4), with minor changes
// Thank you tomtom-m!

// Override of StackPanel which fixes a spacing problem.
// https://github.com/microsoft/microsoft-ui-xaml/issues/916
// The original StackPanel applies spacing to collapsed items.
// This implementation doesn't.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ScreenCapture {

    public class CollapsibleChildStackPanel : StackPanel {

        public static readonly DependencyProperty SpaceProperty =
            DependencyProperty.Register(nameof(Space), typeof(int), typeof(CollapsibleChildStackPanel), new PropertyMetadata(0));
        public int Space {
            get => (int)this.GetValue(SpaceProperty);
            set => this.SetValue(SpaceProperty, value);
        }

        public CollapsibleChildStackPanel() {
            this.Loaded += this.CollapsibleChildStackPanel_Loaded;
        }

        private void CollapsibleChildStackPanel_Loaded(object sender, object e)
            => this.SetSpacingForChildren(this.Space);

        private List<Thickness> originalMargins = new();

        //void UpdateMargin(DependencyObject d , DependencyPropertyChangedEventArgs a) {
        //    FrameworkElement element = d as FrameworkElement;
        //    var old = (Predicate<Thickness>)a.OldValue;

        //    if(old != null) {
        //        int index = originalMargins.FindIndex(old);
        //        originalMargins.RemoveAt(index);
        //        originalMargins.Insert(index, element.Margin);
        //    } //else
        //     //   originalMargins.Add(element.Margin);


        //    SetSpacingForChildren(Space);
        //}

        //private void TrackChildMargins(FrameworkElement element) {
        //    PropertyChangedCallback callback = UpdateMargin;
        //    Binding bind = new Binding() { Path = new PropertyPath("Margin"), Source = element };
        //    var property = DependencyProperty.RegisterAttached("Margin", typeof(object), typeof(CollapsibleChildStackPanel), new PropertyMetadata(null, callback));
        //    element.SetBinding(property, bind);

        //}
        private void SetSpacingForChildren(int spacing) {
            foreach(var it in this.Children) {
                if(it is FrameworkElement element) {
                    if(!originalMargins.Contains(element.Margin)) originalMargins.Add(element.Margin);
                  //  TrackChildMargins(element);

                    var endSpacing = Space;
                    var mainSpacing = Space / 2;

                    double l = element.Margin.Left, t = element.Margin.Top, r = element.Margin.Right, b = element.Margin.Bottom;

                    bool vertical = this.Orientation != Orientation.Vertical;
                    if(it.ActualSize.X > 0 && it.ActualSize.Y > 0 && it.Visibility == Visibility.Visible) {
                        var isActuallyVisible = (UIElement e) => {
                            return e.Visibility == Visibility.Visible && e.ActualSize.X > 0 && e.ActualSize.Y > 0;
                        };

                        int index = this.Children.IndexOf(it);

                        var baseSpacing = index > 0 && isActuallyVisible(this.Children[index - 1]) ? mainSpacing : 0;
                        var footSpacing = index < this.Children.Count - 1 && isActuallyVisible(this.Children[index + 1]) ? mainSpacing : 0;

                        l += vertical ? baseSpacing : 0;
                        t += vertical ? 0 : baseSpacing;
                        r += vertical ? footSpacing : 0;
                        b += vertical ? 0 : footSpacing;
                    } else {
                        l = 0;
                        t = 0;
                        r = 0;
                        b = 0;
                    }
                    Trace.WriteLine($"{l} {t} {r} {b}, {element.GetType()}, {element.Name}");
                    element.Margin = new Thickness(l, t, r, b);
                }
            }
        }
    }
}
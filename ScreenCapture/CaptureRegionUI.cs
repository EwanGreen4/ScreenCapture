using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace ScreenCapture {
    internal class CaptureRegionUI {
        private CanvasControl canvas = new CanvasControl();
        public void promptForSelection() {
            var thread = new Thread(() => {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                Window.Current.SetTitleBar(null);
                canvas.Background = new SolidColorBrush(Colors.Transparent);
                Window.Current.Content = canvas;
                canvas.Draw += Canvas_Draw;
            });
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args) {
            var ds = args.DrawingSession;
       //     SolidColorBrush c = new SolidColorBrush(Colors.Red);
            ds.DrawRectangle(new Rect(canvas.ActualWidth / 3, canvas.ActualHeight / 3, canvas.ActualWidth / 3 * 2, canvas.ActualHeight / 3 * 2), Colors.Red);
        }
    }
}

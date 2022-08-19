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
using Microsoft.Graphics.Canvas.Brushes;

namespace ScreenCapture {
    internal class CaptureRegionUI {
        private CanvasControl canvas = new CanvasControl();
        public void promptForSelection() {
           // var thread = new Thread(() => {
          //      var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                //Window.Current.SetTitleBar(null);
                canvas.Background = new SolidColorBrush(Colors.Transparent);
            var root = canvas.XamlRoot;
            
            Window.Current.Content = canvas;
                canvas.Draw += Canvas_Draw;
         //   });
           // thread.Start();
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args) {
            var ds = args.DrawingSession;
            CanvasSolidColorBrush c = new(args.DrawingSession.Device, Colors.Red);
            c.Opacity = 0.5f;

            ds.DrawRectangle(new Rect(canvas.ActualWidth / 3, canvas.ActualHeight / 3, canvas.ActualWidth / 3 * 2, canvas.ActualHeight / 3 * 2), c);
        }
    }
}

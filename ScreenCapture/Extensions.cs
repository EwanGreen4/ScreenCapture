using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ScreenCapture {
    public static class Extensions {
        public static IAsyncOperation<ContentDialogResult> ShowWithAnimationAsync(this object contentDialog) { // thanks kimbra
            var dialog = contentDialog as ContentDialog;
            if(dialog.Style == null) {
                dialog.Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"];
            }
            return dialog.ShowAsync();
        }
    }
}

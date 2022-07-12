using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Storage.AccessCache;
using Windows.Graphics.Display;
using Windows.Graphics.Display.Core;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel;
using System.Diagnostics;
using Windows.UI.ViewManagement;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


namespace ScreenCapture {

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page {
        private StorageFolder VideosFolder;

        private ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
        private StorageItemAccessList AccessList =
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;

        public double ResolutionScale { get; set; }
        public String NamingScheme { get; set; }
        public String AudioCodecName { get; set; }
        public String VideoCodecName { get; set; }
        public String VideoFormatName { get; set; }

        const string videosKey = "VideoOutputDirectory";

        public Tuple<int, int> RecordingDimensions { get; set; }
        public readonly Tuple<int, int> ScreenDimensions;

        public double FPS { get; set; }

        public readonly double ScreenRefreshRate;

        private async Task GetVideosFolder() {
            StorageFolder folder = null;
            var settingsEntry = LocalSettings.Values[videosKey]?.ToString();
            if(!string.IsNullOrEmpty(settingsEntry)) {
                if(AccessList.ContainsItem(settingsEntry)) // not working?
                    folder = await AccessList.GetFolderAsync(settingsEntry);
            }
            if(folder == null) {
                StorageLibrary library = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Videos);
                folder = library.SaveFolder;
            }
            SetVideosFolder(folder);
        }

        private void SetVideosFolder(StorageFolder s) {
            AccessList.Clear();
            LocalSettings.Values.Remove(videosKey);
            LocalSettings.Values[videosKey] = AccessList.Add(s);
            VideosFolder = s;
            DirectorySubtitle.Text = VideosFolder.Path;
        }

        private async Task<StorageFolder> ChooseVideoPath() {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;

            return await picker.PickSingleFolderAsync();
        }

        private async void OutputPathBrowse_Click(object sender, RoutedEventArgs e) {
            var x = await ChooseVideoPath();
            if(x != null) {
                SetVideosFolder(x);
                SetInfoBlock();
            }
        }

        private static readonly List<string> VideoFormats = new List<string>{
          "MP4",
       //   "MKV",
          "WebM",
          "GIF"
        };
        private static readonly Dictionary<string, List<string>> VideoCodecs = new Dictionary<string, List<string>>{
            {"MP4", new List<string>{ "x264", "h264", "x265", "h265", "AV1" }},
            {"WebM", new List<string>{ "VP8", "VP9", "AV1" }},
     //       {"MKV", new List<string>{ "VP8", "VP9" }},

         };
        private static readonly Dictionary<string, List<string>> AudioCodecs = new Dictionary<string, List<string>>{
            {"MP4", new List<string>{ "AAC", "AC-3", "MP3", "ALAC" }},
            {"WebM", new List<string>{ "Vorbis", "Opus" }},
     //       {"MKV", new List<string>{ "VP8", "VP9" }},
         };

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args) {
            // Get the size of the caption controls and set padding.
            //  LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
            // RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args) {
            if(sender.IsVisible) {
                AppTitleBar.Visibility = Visibility.Visible;
            } else {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args) {
            //UISettings settings = new UISettings();
            //if(args.WindowActivationState == CoreWindowActivationState.Deactivated) {
            //    AppTitleTextBlock.Foreground =
            //       new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
            //}
            //else {
            //    AppTitleTextBlock.Foreground =
            //       new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
            //}
        }

        public MainPage() {
            this.InitializeComponent();

            CoreApplicationViewTitleBar coreTitleBar =
            CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar =
            ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            Window.Current.SetTitleBar(AppTitleBar);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;

            GetVideosFolder(); // will error but I do not want to await

            var displayInformation = DisplayInformation.GetForCurrentView();
            var screenSize = new Size(displayInformation.ScreenWidthInRawPixels,
                                      displayInformation.ScreenHeightInRawPixels);
            ScreenDimensions = Tuple.Create((int)screenSize.Width, (int)screenSize.Height);

            var info = HdmiDisplayInformation.GetForCurrentView();
            if(info != null) {
                var mode = info.GetCurrentDisplayMode();
                if(mode != null)
                    ScreenRefreshRate = mode.RefreshRate;
                return;
            }

            ScreenRefreshRate = 60;
            // lots of other handy stuff in info; implement colorspace stuff too


            FPSBox.Minimum = 1;
            FPSBox.Maximum = ScreenRefreshRate;
            FPSBox.WarningCard = SettingsWarningCard;
            FPSBox.Warning = $"FPS is out of range. ({FPSBox.Minimum} - {FPSBox.Maximum})";
            FPSBox.ValidNumberChosen += new RangedNumberBox.NumberValidityEventHandler(delegate (double e) {
                FPS = e;

                SetInfoBlock();
            });

            ResolutionBox.Minimum = 1;
            ResolutionBox.Maximum = 100;
            ResolutionBox.WarningCard = SettingsWarningCard;
            ResolutionBox.Warning = "Resolution is not a vaild percentage.";
            ResolutionBox.ValidNumberChosen += new RangedNumberBox.NumberValidityEventHandler(delegate (double e) {
                ResolutionScale = e;
                RecordingDimensions = Tuple.Create((int)Math.Ceiling(ScreenDimensions.Item1 * ResolutionScale / 100), (int)Math.Ceiling(ScreenDimensions.Item2 * ResolutionScale / 100));
                SetInfoBlock();
            });

            foreach(var i in VideoFormats) {
                FormatBox.Items.Add(i);
            }

            FormatBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                VideoFormatName = FormatBox.SelectedItem.ToString();

                VideoCodecBox.Items.Clear();

                if(VideoCodecs.ContainsKey(VideoFormatName)) {
                    foreach(var i in VideoCodecs[VideoFormatName]) {
                        VideoCodecBox.Items.Add(i);
                    }
                    VideoCodecBox.SelectedIndex = 0;
                    VideoCodecBox.IsEnabled = true;

                    VideoCodecSubtitle.Text = "";
                    VideoCodecSubtitle.Visibility = Visibility.Collapsed;
                } 
                if(VideoCodecBox.Items.Count == 0){
                    VideoCodecName = "";
                    VideoCodecBox.IsEnabled = false;
                    VideoCodecSubtitle.Visibility = Visibility.Visible;
                    VideoCodecSubtitle.Text = $"Video codecs are not applicable to {VideoFormatName}.";
                }

                AudioCodecBox.Items.Clear();
                if(AudioCodecs.ContainsKey(VideoFormatName)) {
                    foreach(var i in AudioCodecs[VideoFormatName]) {
                        AudioCodecBox.Items.Add(i);
                    }
                    AudioCodecBox.SelectedIndex = 0;
                    AudioCodecBox.IsEnabled = true;

                    AudioCodecSubtitle.Text = "";
                    AudioCodecSubtitle.Visibility = Visibility.Collapsed;
                } 
                if(AudioCodecBox.Items.Count == 0) {
                    AudioCodecName = "";
                    AudioCodecBox.IsEnabled = false;
                    AudioCodecSubtitle.Visibility = Visibility.Visible;
                    AudioCodecSubtitle.Text = $"Audio is not supported by {VideoFormatName}.";
                }
                SetInfoBlock();
            });

            VideoCodecBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                VideoCodecName = VideoCodecBox.SelectedItem?.ToString();
                SetInfoBlock();
            });
            AudioCodecBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                AudioCodecName = AudioCodecBox.SelectedItem?.ToString();
                SetInfoBlock();
            });
        }

        private void MainNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args) {
            sender.Header = args.SelectedItemContainer.Content.ToString();
        }

        private void SetInfoBlock() {
            if(FPS == double.NaN || ResolutionScale == double.NaN || RecordingDimensions == null || string.IsNullOrEmpty(VideoFormatName) || string.IsNullOrEmpty(VideosFolder.Path))
                return;

            ResolutionBlock.Text = $"{RecordingDimensions.Item1}x{RecordingDimensions.Item2} at {FPS} FPS (At fullscreen with {ResolutionScale}% scaling)";
            string EncodingString = VideoFormatName;
            if(!String.IsNullOrEmpty(VideoCodecName))
                EncodingString += " with " + VideoCodecName;
            if(!String.IsNullOrEmpty(AudioCodecName))
                EncodingString += " and " + AudioCodecName;
            EncodingBlock.Text = EncodingString;
            OutputFolderBlock.Text = VideosFolder.Path;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Display.Core;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ScreenCapture {
    public sealed partial class SettingsPage : UserControl {
        private ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
        private StorageItemAccessList AccessList =
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
        const string videosKey = "VideoOutputDirectory";

        [Serializable]
        public struct VideoConfig {
            public StorageFolder VideosFolder;
            public byte ResolutionScale { get; set; }
            public String NamingScheme { get; set; }
            public String AudioCodecName { get; set; }
            public String VideoCodecName { get; set; }
            public String VideoFormatName { get; set; }
            public String VideoEncoderAgentName { get; set; }
            public ushort FPS { get; set; } //cannot use byte because some screens can exceed 255 fps
        };
        public VideoConfig Config;

        public Tuple<ushort, ushort> RecordingDimensions { get; set; }
        public readonly Tuple<ushort, ushort> ScreenDimensions;

        public readonly int PracticalScreenRefreshRate;

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
            Config.VideosFolder = s;
            DirectorySubtitle.Text = Config.VideosFolder.Path;
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
          "GIF", "APNG"
        };
        private static readonly Dictionary<string, ObservableCollection<string>> VideoCodecs = new Dictionary<string, ObservableCollection<string>>{
            {"MP4", new ObservableCollection<string>{ "x264", "H.264", "x265", "HEVC", "AV1" }},
            {"WebM", new ObservableCollection<string>{ "VP8", "VP9", "AV1" }},
     //       {"MKV", new ObservableCollection<string>{  }},

         };
        private static readonly Dictionary<string, ObservableCollection<string>> AudioCodecs = new Dictionary<string, ObservableCollection<string>>{
            {"MP4", new ObservableCollection<string>{ "AAC", "AC-3", "MP3", "ALAC" }},
            {"WebM", new ObservableCollection<string>{ "Vorbis", "Opus" }},
     //       {"MKV", new ObservableCollection<string>{  }},
         };
        private static readonly Dictionary<string, ObservableCollection<string>> VideoEncoderAgents = new Dictionary<string, ObservableCollection<string>>{
            {"H.264", new ObservableCollection<string>{ "x264", "NVENC", "AMF", "Quick Sync"}},
            {"HEVC", new ObservableCollection<string>{ "x265", "NVENC", "AMF", "Quick Sync" }},
            //{"WebM", new ObservableCollection<string>{ "VP8", "VP9", "AV1" }},
     //       {"MKV", new ObservableCollection<string>{  }},

         };


        public SettingsPage() {
            this.InitializeComponent();

            GetVideosFolder(); // will error but I do not want to await

            var displayInformation = DisplayInformation.GetForCurrentView();
            var screenSize = new Size(displayInformation.ScreenWidthInRawPixels,
                                      displayInformation.ScreenHeightInRawPixels);
            ScreenDimensions = Tuple.Create((ushort)screenSize.Width, (ushort)screenSize.Height);

            var info = HdmiDisplayInformation.GetForCurrentView();
            if(info != null) {
                var mode = info.GetCurrentDisplayMode();
                if(mode != null)
                    PracticalScreenRefreshRate = (int)Math.Ceiling(mode.RefreshRate);
                return;
            }

            PracticalScreenRefreshRate = 60;
            // lots of other handy stuff in info; implement colorspace stuff too


            FPSBox.Minimum = 1;
            FPSBox.Maximum = PracticalScreenRefreshRate;
            FPSBox.WarningCard = SettingsWarningCard;
            FPSBox.Warning = $"FPS is out of range. ({FPSBox.Minimum} - {FPSBox.Maximum})";
            FPSBox.ValidNumberChosen += new RangedNumberBox.NumberValidityEventHandler(delegate (double e) {
                Config.FPS = (ushort)Math.Ceiling(e);

                SetInfoBlock();
            });

            ResolutionBox.Minimum = 1;
            ResolutionBox.Maximum = 100;
            ResolutionBox.WarningCard = SettingsWarningCard;
            ResolutionBox.Warning = "Resolution is not a vaild percentage.";
            ResolutionBox.ValidNumberChosen += new RangedNumberBox.NumberValidityEventHandler(delegate (double e) {
                Config.ResolutionScale = (byte)Math.Ceiling(e);
                RecordingDimensions = Tuple.Create((ushort)Math.Ceiling((double)ScreenDimensions.Item1 * Config.ResolutionScale / 100), (ushort)Math.Ceiling((double)ScreenDimensions.Item2 * Config.ResolutionScale / 100));
                SetInfoBlock();
            });

            foreach(var i in VideoFormats) {
                FormatBox.Items.Add(i);
            }

            FormatBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                Config.VideoFormatName = FormatBox.SelectedItem.ToString();

                if(VideoCodecs.ContainsKey(Config.VideoFormatName)) {
                    VideoCodecBox.ItemsSource = VideoCodecs[Config.VideoFormatName];
                    VideoCodecBox.SelectedIndex = 0;
                    VideoCodecBox.IsEnabled = true;

                    VideoCodecSubtitle.Text = "";
                    VideoCodecSubtitle.Visibility = Visibility.Collapsed;
                } else {
                    VideoCodecBox.ItemsSource = null;
                    Config.VideoCodecName = "";
                    VideoCodecBox.IsEnabled = false;
                    VideoCodecSubtitle.Visibility = Visibility.Visible;
                    VideoCodecSubtitle.Text = $"Video codecs are not applicable to {Config.VideoFormatName}.";
                }

                if(AudioCodecs.ContainsKey(Config.VideoFormatName)) {
                    AudioCodecBox.ItemsSource = AudioCodecs[Config.VideoFormatName];
                    AudioCodecBox.SelectedIndex = 0;
                    AudioCodecBox.IsEnabled = true;

                    AudioCodecSubtitle.Text = "";
                    AudioCodecSubtitle.Visibility = Visibility.Collapsed;
                } else {
                    AudioCodecBox.ItemsSource = null;
                    Config.AudioCodecName = "";
                    AudioCodecBox.IsEnabled = false;
                    AudioCodecSubtitle.Visibility = Visibility.Visible;
                    AudioCodecSubtitle.Text = $"Audio is not supported by {Config.VideoFormatName}.";
                }
                SetInfoBlock();
            });

            VideoCodecBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                Config.VideoCodecName = VideoCodecBox.SelectedItem?.ToString();
                if(!String.IsNullOrEmpty(Config.VideoCodecName) && VideoEncoderAgents.ContainsKey(Config.VideoCodecName)) {
                    VideoEncoderAgentBox.ItemsSource = VideoEncoderAgents[Config.VideoCodecName];
                    VideoEncoderAgentBox.SelectedIndex = 0;
                    VideoEncoderAgentBox.IsEnabled = true;
                    VideoEncoderAgentBox.Visibility = Visibility.Visible;
                    VideoEncoderAgentLabel.Visibility = Visibility.Visible;
                } else {
                    VideoEncoderAgentBox.ItemsSource = null;
                    Config.VideoEncoderAgentName = "";
                    VideoEncoderAgentBox.IsEnabled = false;
                    VideoEncoderAgentBox.Visibility = Visibility.Collapsed;
                    VideoEncoderAgentLabel.Visibility = Visibility.Collapsed;
                }
                SetInfoBlock();
            });
            AudioCodecBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                Config.AudioCodecName = AudioCodecBox.SelectedItem?.ToString();
                SetInfoBlock();
            });
            VideoEncoderAgentBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e) {
                Config.VideoEncoderAgentName = VideoEncoderAgentBox.SelectedItem?.ToString();
                SetInfoBlock();
            });

            ReadConfig();
        }

        const String Filename = "config.json";
        private async Task ReadConfig() {
            StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(Filename);
            Config = JsonSerializer.Deserialize<VideoConfig>(JsonDocument.Parse(await FileIO.ReadTextAsync(File)));
            SetVideosFolder(Config.VideosFolder);
        }

        private async Task SaveConfig() {
            String ConfigString = JsonSerializer.Serialize(Config);

            var Folder = ApplicationData.Current.LocalFolder;
            StorageFile File;
            try {
                File = await Folder.GetFileAsync(Filename);
            } catch {
                File = await Folder.CreateFileAsync(Filename);
            }
            await FileIO.WriteTextAsync(File, ConfigString);
        }
        private void SetInfoBlock() {
            if(Config.FPS <= 0 || Config.ResolutionScale <= 0 || RecordingDimensions == null || string.IsNullOrEmpty(Config.VideoFormatName) || Config.VideosFolder == null || string.IsNullOrEmpty(Config.VideosFolder.Path))
                return;

            ResolutionBlock.Text = $"{RecordingDimensions.Item1}x{RecordingDimensions.Item2} at {Config.FPS} FPS (At fullscreen with {Config.ResolutionScale}% scaling)";
            string EncodingString = Config.VideoFormatName;
            if(!String.IsNullOrEmpty(Config.VideoCodecName))
                EncodingString += " with " + Config.VideoCodecName;
            if(!String.IsNullOrEmpty(Config.VideoEncoderAgentName))
                EncodingString += $" ({Config.VideoEncoderAgentName})";
            if(!String.IsNullOrEmpty(Config.AudioCodecName))
                EncodingString += " and " + Config.AudioCodecName;
            EncodingBlock.Text = EncodingString;
            OutputFolderBlock.Text = Config.VideosFolder.Path;

            SaveConfig();
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e) {
        //    SetInfoBlock();
        //}
    }
}

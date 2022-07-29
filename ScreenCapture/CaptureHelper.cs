using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.Media.Core;

namespace ScreenCapture {
    internal class CaptureHelper {
        public struct Config {
            public string FormatName;
            public string VideoCodecName;
            public string AudioCodecName;

            public ushort Framerate;
            public Rect CaptureRegion;
            public Tuple<ushort, ushort> FinishedResolution;
        }
        public readonly Config config;

        private Process ffmpeg;

        public enum Error : ushort {
            Success = 0,
            UnknownError = 0b11111111,
            VideoCodecUnsupportedError = 0b00000001,
            AudioCodecUnsupportedError = 0b00000010,

        }

        CaptureHelper() {

        }

        public async Task<IReadOnlyList<CodecInfo>> CodecProbeAsync() {
            var codecQuery = new CodecQuery();
            IReadOnlyList<CodecInfo> result =
                 await codecQuery.FindAllAsync(CodecKind.Video, CodecCategory.Encoder, "");


            return result;
        }

        public async Task<Error> StartRecordingAsync(Config config) {

            ffmpeg = Process.Start("ffmpeg.exe", "");
            
           // x.OutputDataReceived += X_OutputDataReceived;
            return Error.Success & Error.UnknownError;
        }

        public void EndRecording() {
            ffmpeg.Kill();
        }
        private void X_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}

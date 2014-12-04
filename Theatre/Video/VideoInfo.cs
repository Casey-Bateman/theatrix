using System;
using System.IO;
using NAudio.Wave;
using Theatre.Conversion.Utility;

namespace Theatre.Conversion.Video
{
    public class VideoInfo
    {
        private const string MP3_FORMAT = "MPEG Audio";
        private readonly string _filename;
        private readonly IMediaInfo _mi;

        internal VideoInfo(string videoPath)
        {
            _mi = new MediaInfo();
            _filename = videoPath;
            
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(_filename)) throw new FileNotFoundException("Video file not found", _filename);

            ReadInfo(_filename);
            if (!HasVideo) throw new InvalidOperationException("File must contain video");
        }

        public virtual void ReadInfo(string path)
        {
            _mi.Option("Internet", "No");
            int opened = _mi.Open(path);
            if (opened == 0) throw new Exception("Unable to open file " + path);

            FullName = path;

            try
            {
                string format = _mi.Get(StreamKind.General, 0, "Format");

                // duration is in milliseconds
                double durms;
                if (double.TryParse(_mi.Get(StreamKind.General, 0, "Duration"), out durms)) Duration = TimeSpan.FromMilliseconds(durms);

                if (Duration == TimeSpan.Zero && format == MP3_FORMAT)
                {
                    Duration = TimeSpan.FromSeconds(CalculateMp3Duration(path));
                }

                HasAudio = _mi.Count_Get(StreamKind.Audio) > 0;
                HasVideo = _mi.Count_Get(StreamKind.Video) > 0;
                if (HasVideo)
                {
                    int w, h;
                    if (int.TryParse(_mi.Get(StreamKind.Video, 0, "Width"), out w)) Width = w;
                    if (int.TryParse(_mi.Get(StreamKind.Video, 0, "Height"), out h)) Height = h;

                    double ar;
                    if (double.TryParse(_mi.Get(StreamKind.Video, 0, "AspectRatio"), out ar)) AspectRatio = Math.Round(ar, 2);

                    double fr;
                    if (double.TryParse(_mi.Get(StreamKind.Video, 0, "FrameRate"), out fr)) FrameRate = fr;
                }

                DateTime rd;
                RecordingDate = DateTime.TryParse(_mi.Get(StreamKind.General, 0, "Recorded_Date"), out rd) ? rd : File.GetLastWriteTime(FullName);
            }
            finally
            {
                _mi.Close();
            }

            var fileInfo = new FileInfo(path);
            FileSize = fileInfo.Length;
        }

        double CalculateMp3Duration(string fileName)
        {
            var duration = 0.0;
            using (FileStream fs = File.OpenRead(fileName))
            {
                var frame = Mp3Frame.LoadFromStream(fs);
                while (frame != null)
                {
                    duration += frame.SampleCount / (double)frame.SampleRate;
                    frame = Mp3Frame.LoadFromStream(fs);
                }
            }
            return duration;
        }

        public string FullName { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public bool HasVideo { get; protected set; }
        public bool HasAudio { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public double AspectRatio { get; protected set; }
        public DateTime RecordingDate { get; protected set; }
        public double FrameRate { get; protected set; }
        public long FileSize { get; protected set; }
    }
}
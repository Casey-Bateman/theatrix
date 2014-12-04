using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Theatre.Conversion.Video
{
    public class VideoJob
    {
        public List<VideoInfo> DvdChapters { get; set; }

        public VideoJob()
        {
            DvdChapters = new List<VideoInfo>();
        }

        public static VideoJob From(string videoTsPath)
        {
            if (string.IsNullOrWhiteSpace(videoTsPath))
            {
                return null;
            }

            var vobFiles = Directory.GetFiles(videoTsPath, "*.vob", SearchOption.TopDirectoryOnly);
            if (vobFiles.Length == 0)
            {
                return null; 
            }

            var videoPresentaion = vobFiles.Where(vbf => vbf.IndexOf("VTS_01", StringComparison.InvariantCulture) != -1).ToList();
            if (videoPresentaion.Count == 0)
            {
                return null;
            }

            videoPresentaion.Sort();
            return new VideoJob()
                {
                    DvdChapters = videoPresentaion.Select(vbf => new VideoInfo(vbf)).ToList()
                };
        }
    }
}

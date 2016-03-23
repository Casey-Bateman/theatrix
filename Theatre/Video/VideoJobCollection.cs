using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Theatre.Conversion.Video
{
    public class VideoJobCollection
    {
        private List<VideoJob> Jobs { get; set; }

        public int Count { get { return Jobs.Count; } }

        public VideoJobCollection()
        {
            Jobs = new List<VideoJob>();
        }

        public void Add(VideoJob videoJob)
        {
            Jobs.Add(videoJob);    
        }

        public void ForEach(Action<VideoJob> processAction)
        {
            if (processAction == null)
            {
                throw new ArgumentNullException("processAction");
            }

            Jobs.ForEach(processAction);
        }

        public static VideoJobCollection From(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentNullException("directory");
            }

            var videoJobCollection = new VideoJobCollection(); 
            var topLevelDirectories = Directory.GetDirectories(directory, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (var topLevelDirectory in topLevelDirectories)
            {
                var videoTsDirectory = Directory.GetDirectories(topLevelDirectory, "VIDEO_TS", SearchOption.AllDirectories); 
                if (videoTsDirectory.Length == 0) { continue; }

                var videoJob = VideoJob.From(videoTsDirectory.FirstOrDefault()); 
                if (videoJob == null) { continue; }

                videoJobCollection.Add(videoJob);
            }

            return videoJobCollection;
        }
    }
}

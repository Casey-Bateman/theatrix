using Theatre.Conversion.Video;
using log4net;
using log4net.Config;

namespace Theatre.Conversion
{
    public class Program
    {
        private const string DefaultVideoLibrary = "E:\\MovieLibrary";
        private static ILog _log = LogManager.GetLogger(typeof (Program));

        private static VideoJobCollection ConversionJobs { get; set; }

        private static void Main(string[] args)
        {

        }

        private static void Start()
        {
            BasicConfigurator.Configure();

            _log.Info("*********************************************");
            _log.Info("***   Staring Theatre Stream Conversion   ***");
            _log.Info("*********************************************");

            _log.InfoFormat("Seeking for movies in: {0}", DefaultVideoLibrary);

            ConversionJobs = VideoJobCollection.From(DefaultVideoLibrary); 

            _log.InfoFormat("Found {0} movies in: {1}", ConversionJobs.Count, DefaultVideoLibrary);


        }

        private static void Stop()
        {
            _log.Info("Shutting down the conversion utility...");
        }
    }
}

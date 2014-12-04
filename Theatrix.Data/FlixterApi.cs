using System.Text;
using Theatrix.Data.BaseObjects;
using Theatrix.Data.Models;

namespace Theatrix.Data
{
    public class FlixterApi : WebRequestApi
    {
        public const int FlixterTimeoutMilliseconds = 10000;
        public const string FlixterConfigPath = @"c:\theatrix\config\flixter-api.json";

        private FlixterApi() 
            : base(FlixterConfigPath, FlixterTimeoutMilliseconds)
        {
        }

        public static FlixterApi Create()
        {
            return new FlixterApi();
        }

        private string GetMoviesApiUri()
        {
            return "http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey=" + ApiConfig.ApplicationKey;
        }

        private string GetListsApiUri()
        {
            return "http://api.rottentomatoes.com/api/public/v1.0/lists.json?apikey=" + ApiConfig.ApplicationKey;
        }

        public FlixterMovieList GetMovies(string title)
        {
            var baseUri = new StringBuilder(GetMoviesApiUri());

            baseUri.Append("&q=" + title);
            baseUri.Append("&page_limit=50");

            var searchResult = (baseUri.ToString());

            return SerializeAs<FlixterMovieList>(searchResult);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Theatrix.Data.BaseObjects;

namespace Theatrix.Data
{
    public class FlixterDao
    {
        public const int FlixterTimeoutMilliseconds = 10000;
        private const string FlixterApiKey = "ybg6m6hgxuztkusn59y638sq";

        private string GetMoviesApiUri()
        {
            return "http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey=" + FlixterApiKey;
        }
        private string GetListsApiUri()
        {
            return "http://api.rottentomatoes.com/api/public/v1.0/lists.json?apikey=" + FlixterApiKey;
        }

        public List<FlixterMovies> GetMovies(string title)
        {
            var baseUri = new StringBuilder(GetMoviesApiUri());

            baseUri.Append("&q=" + title);
            baseUri.Append("&page_limit=50");

            var searchResult = SendRequest(baseUri.ToString());

            return (from JObject rawMovie in (JArray)searchResult["movies"]
                           select new FlixterMovies
                               {
                                   Id = GetValue<string>(rawMovie, "id"),
                                   Year = GetValue<int>(rawMovie, "year"),
                                   Title = GetValue<string>(rawMovie, "title"),
                                   Runtime = GetValue<int>(rawMovie, "runtime"),
                                   Studio = GetValue<string>(rawMovie, "studio"),
                                   Synopsis = GetValue<string>(rawMovie, "synopsis"),
                                   MpaaRating = GetValue<string>(rawMovie, "mpaa_rating"),
                                   AlternateIds = GetValue<string>(rawMovie, "altenate_ids"),
                                   CriticsConsensus = GetValue<string>(rawMovie, "critics_consensus"), 
                                   AbridgedDirectors = GetValue<string>(rawMovie, "abridged_directors"), 
                                   Links = GetObject<MovieLinks>(rawMovie, "links"),
                                   Poster = GetObject<Posters>(rawMovie, "posters"),
                                   Ratings = GetObject<Ratings>(rawMovie, "ratings"), 
                                   ReleaseDate = GetObject<ReleaseDates>(rawMovie, "release_dates"), 
                                   AbridgedCast = GetObject<AbridgedCast[]>(rawMovie, "abridged_cast").ToList(),
                               }).ToList();
        }

        private T GetValue<T>(JObject source, string key)
        {
            JToken parseValue;
            var returnValue = default(T);
            try
            {
                return source.TryGetValue(key, out parseValue)
                           ? parseValue.Value<T>()
                           : returnValue;
            }
            catch (Exception e)
            {
                return returnValue;
            }
            
        }
        private T GetObject<T>(JObject source, string key)
        {
            JToken parseValue;
            var returnValue = default(T);
            try
            {
                return source.TryGetValue(key, out parseValue)
                           ? parseValue.ToObject<T>()
                           : returnValue;
            }
            catch (Exception e)
            {
                return returnValue;
            }
        }


        private JObject SendRequest(string url)
        {
            var flixterWebRequest = WebRequest.Create(url);
            flixterWebRequest.Timeout = FlixterTimeoutMilliseconds;

            var result = string.Empty; 
             
            using (var response = flixterWebRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        var stream = new StreamReader(receiveStream);
                        result = stream.ReadToEnd();
                    }
                }
            }

            return JObject.Parse(result);
        }
    }
}

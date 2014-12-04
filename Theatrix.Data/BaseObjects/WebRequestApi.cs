using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Theatrix.Data.Models.Config;

namespace Theatrix.Data.BaseObjects
{
    public abstract class WebRequestApi
    {
        protected WebRequestApi(string configPath, int webTimeout)
        {
            WebTimeout = webTimeout;
            ApiConfig = SerializeAs<ApiConfig>(File.ReadAllText(configPath));
        }

        protected ApiConfig ApiConfig { get; set; }

        protected int WebTimeout { get; set; }

        protected JObject GetResponseJObject(string url)
        {
            var result = GetResponseString(url);

            return JObject.Parse(result);
        }

        protected string GetResponseString(string url)
        {
            var flixterWebRequest = WebRequest.Create(url);
            flixterWebRequest.Timeout = WebTimeout;

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

            return result;
        }

        protected T SerializeAs<T>(string jsonString)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return (T)jsonSerializer.ReadObject(stream);
        }

        protected T GetValue<T>(JObject source, string key)
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

        protected T GetObject<T>(JObject source, string key)
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
    }
}

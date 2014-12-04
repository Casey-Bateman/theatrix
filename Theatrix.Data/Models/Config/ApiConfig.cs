using System.Runtime.Serialization;

namespace Theatrix.Data.Models.Config
{
    [DataContract]
    public class ApiConfig
    {
        [DataMember(Name = "application")]
        public string Application { get; set; }

        [DataMember(Name = "application_key")]
        public string ApplicationKey { get; set; }
    }
}

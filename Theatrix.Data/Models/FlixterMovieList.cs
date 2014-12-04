using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Theatrix.Data.Models
{
    /// <summary>
    /// This is the return movie object from the rotten tomatoes api. 
    /// </summary>
    [DataContract]
    public class FlixterMovieList
    {
        [DataMember(Name="movies")]
        public List<FlixterMovie> Movies { get; set; }
    }
}

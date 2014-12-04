using System.Collections.Generic;
using System.Runtime.Serialization;
using Theatrix.Data.BaseObjects;

namespace Theatrix.Data.Models
{
    /// <summary>
    /// This is the return movie object from the rotten tomatoes api. 
    /// </summary>
    [DataContract]
    public class FlixterMovie
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }

        [DataMember(Name = "mpaa_rating")]
        public string MpaaRating { get; set; }

        [DataMember(Name = "runtime")]
        public int Runtime { get; set; }

        [DataMember(Name = "critics_consensus")]
        public string CriticsConsensus { get; set; }

        [DataMember(Name = "release_dates")]
        public ReleaseDates ReleaseDate { get; set; }

        [DataMember(Name = "ratings")]
        public Ratings Ratings { get; set; }

        [DataMember(Name = "synopsis")]
        public string Synopsis { get; set; }

        [DataMember(Name = "posters")]
        public Posters Poster { get; set; }

        [DataMember(Name = "abridged_cast")]
        public List<AbridgedCast> AbridgedCast { get; set; }

        [DataMember(Name = "abridged_directors")]
        public string AbridgedDirectors { get; set; }

        [DataMember(Name = "studio")]
        public string Studio { get; set; }

        [DataMember(Name = "altenate_ids")]
        public string AlternateIds { get; set; }

        [DataMember(Name = "links")]
        public MovieLinks Links { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theatrix.Data.BaseObjects;

namespace Theatrix.Data.BaseObjects
{
    /// <summary>
    /// This is the return movie object from the rotten tomatoes api. 
    /// </summary>
    public class FlixterMovies
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string MpaaRating { get; set; }

        public int Runtime { get; set; }

        public string CriticsConsensus { get; set; }

        public ReleaseDates ReleaseDate { get; set; }

        public Ratings Ratings { get; set; }

        public string Synopsis { get; set; }

        public Posters Poster { get; set; }

        public List<AbridgedCast> AbridgedCast { get; set; }

        public string AbridgedDirectors { get; set; }

        public string Studio { get; set; }

        public string AlternateIds { get; set; }

        public MovieLinks Links { get; set; }
    }
}

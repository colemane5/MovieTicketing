using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class Movie
    {
        public int Id;
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }

        public Movie(int id, string title, string releaseDate, string description)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Description = description;
        }
    }
}

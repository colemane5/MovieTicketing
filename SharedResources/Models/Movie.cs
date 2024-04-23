using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharedResources.Models
{
    public struct Movie(int id, string title, DateTime releaseDate, string description)
    {
        public int Id = id;
        public string Title { get; set; } = title;
        public DateTime ReleaseDate { get; set; } = releaseDate;
        public string Description { get; set; } = description;
    }
}

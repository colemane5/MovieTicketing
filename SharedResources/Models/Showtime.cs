using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class Showtime
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }

        public Showtime(int movieId, int theaterId, DateTime showingDateTime, decimal price, int seatsAvailable)
        {
            MovieId = movieId;
            TheaterId = theaterId;
            ShowingDateTime = showingDateTime;
            Price = price;
            SeatsAvailable = seatsAvailable;
        }
    }
}

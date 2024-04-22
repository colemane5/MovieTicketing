using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public struct Showtime(int id, int movieId, int theaterId, DateTime showingDateTime, decimal price, int seatsAvailable)
    {
        public int Id { get; set; } = id;
        public int MovieId { get; set; } = movieId;
        public int TheaterId { get; set; } = theaterId;
        public DateTime ShowingDateTime { get; set; } = showingDateTime;
        public decimal Price { get; set; } = price;
        public int SeatsAvailable { get; set; } = seatsAvailable;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public struct Showtime(int id, int movieId, int theaterId, DateTime startTime, decimal price, int seatsAvailable)
    {
        public int Id { get; set; } = id;
        public int MovieId { get; set; } = movieId;
        public int TheaterId { get; set; } = theaterId;
        public DateTime StartTime { get; set; } = startTime;
        public string StartDay => StartTime.ToString("g");
        public decimal Price { get; set; } = price;
        public int SeatsAvailable { get; set; } = seatsAvailable;
    }
}

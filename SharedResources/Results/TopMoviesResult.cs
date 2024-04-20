using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public struct TopMoviesResult(string movieTitle, int ticketsSold, int totalShowings, float avgTicketsPerShowing)
    {
        public string MovieTitle { get; } = movieTitle;
        public int TicketsSold { get; } = ticketsSold;
        public int TotalShowings { get; } = totalShowings;
        public float AvgTicketsPerShowing { get; } = avgTicketsPerShowing;
    }
}

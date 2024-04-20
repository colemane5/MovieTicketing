using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public struct TopMoviesResult(string MovieTitle, int TicketsSold, int TotalShowings, float AvgTicketsPerShowing)
    {
        public string MovieTitle { get; } = MovieTitle;
        public int TicketsSold { get; } = TicketsSold;
        public int TotalShowings { get; } = TotalShowings;
        public float AvgTicketsPerShowing { get; } = AvgTicketsPerShowing;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public readonly struct HourlySalesResult(int hourOfDay, int uniqueMovies, int uniqueTheaters, int ticketSales)
    {
        public int HourOfDay { get; } = hourOfDay;
        public int MovieCount { get; } = uniqueMovies;
        public int TheaterCount { get; } = uniqueTheaters;
        public decimal TicketSales { get; } = ticketSales;
    }
}

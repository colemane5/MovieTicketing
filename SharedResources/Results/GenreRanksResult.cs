using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public readonly struct GenreRanksResult(int rank, string genreName, int ticketsSold)
    {
        public int Rank { get; } = rank;
        public string GenreName { get; } = genreName;
        public int TicketsSold { get; } = ticketsSold;
    }
}

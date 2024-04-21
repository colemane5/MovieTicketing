using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.SqlInterfaces
{
    class SqlTheaterRepository : ITheaterRepository
    {
        IReadOnlyList<DateTime> ITheaterRepository.FindShowtimes(int movieID, int theaterID)
        {
            throw new NotImplementedException();
        }

        string ITheaterRepository.GetTicket(int userID, int MovieShowtimeID, decimal salePrice)
        {
            throw new NotImplementedException();
        }

        List<Theater> ITheaterRepository.RetrieveTheaters()
        {
            throw new NotImplementedException();
        }
    }
}

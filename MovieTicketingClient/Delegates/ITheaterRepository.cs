using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.Delegates
{
    public interface ITheaterRepository
    {
        /// <summary>
        /// Returns the showtimes of movies from a given theater and movie
        /// </summary>
        /// <param name="movieID">the ID for the given movie</param>
        /// <param name="theaterID">the ID for the given theater</param>
        /// <returns>A read-only list of DateTimes of movie showings</returns>
        IReadOnlyList<DateTime> FindShowtimes(int movieID, int theaterID);

        /// <summary>
        /// Logs a ticket sale into the database
        /// </summary>
        /// <param name="userID">the ID of the purchaser</param>
        /// <param name="MovieShowtimeID">the showtime ID for the ticket sale</param>
        /// <param name="salePrice">the price of the ticket</param>
        /// <returns>returns a string stating whether the operation succeeded</returns>
        string GetTicket(int userID, int MovieShowtimeID, decimal salePrice);
    }
}

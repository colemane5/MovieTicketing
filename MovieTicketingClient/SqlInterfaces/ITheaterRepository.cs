using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.SqlInterfaces
{
    public interface ITheaterRepository
    {
        /// <summary>
        /// Returns the showtimes of movies from a given theater and movie
        /// </summary>
        /// <param name="movieID">the ID for the given movie</param>
        /// <param name="theaterID">the ID for the given theater</param>
        /// <returns>A read-only list of Showtimes</returns>
        IReadOnlyList<Showtime> FindShowtimes(int movieID, int theaterID);

        /// <summary>
        /// Logs a ticket sale into the database
        /// </summary>
        /// <param name="userID">the ID of the purchaser</param>
        /// <param name="MovieShowtimeID">the showtime ID for the ticket sale</param>
        /// <param name="salePrice">the price of the ticket</param>
        /// <returns>returns a string stating whether the operation succeeded</returns>
        string GetTicket(int userID, int MovieShowtimeID, decimal salePrice, int seatsLeft);

        /// <summary>
        /// A function to return all theaters on the db to use in the filter function
        /// </summary>
        /// <returns>A list of theaters in the db</returns>
        IReadOnlyList<Theater> RetrieveTheaters(int movieID);
    }
}

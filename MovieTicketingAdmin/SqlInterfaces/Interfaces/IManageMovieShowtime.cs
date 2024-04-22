using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace MovieTicketingAdmin.SqlInterfaces.Interfaces
{
    public interface IManageMovieShowtime
    {
        /// <summary>
        /// A function that allows the admin to Add, Edit, or Remove Movie Showtimes in the db
        /// </summary>
        /// <param name="task">a string that contains the intended action
        /// VALUES: "ADD", "UPDATE", OR "REMOVE"</param>
        /// <param name="movieId">the Movie Id for the desired showing to make changes to or add</param>
        /// <param name="theaterId">the Theater Id for the desired showing to make changes to or add</param>
        /// <param name="startOn">the starting time for the desired showing to make changes to or add</param>
        /// <param name="newStartOn">the new desired starting time when updating a showtime</param>
        /// <returns>an int indicating if an update or removal was successful or not</returns>
        public int ManageMovieShowtime(string task, int movieId, int theaterId,
            DateTimeOffset startOn, DateTimeOffset newStartOn);

        /// <summary>
        /// Returns the showtimes of movies from a given theater and movie
        /// </summary>
        /// <param name="movieID">the ID for the given movie</param>
        /// <param name="theaterID">the ID for the given theater</param>
        /// <returns>A read-only list of Showtimes</returns>
        public IReadOnlyList<Showtime> FindShowtimes(int movieId, int theaterId);
    }
}

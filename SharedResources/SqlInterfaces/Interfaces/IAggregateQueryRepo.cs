using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Results;

namespace SharedResources.SqlInterfaces.Interfaces
{
    public interface IAggregateQueryRepo
    {
        /// <summary>
        /// A function that calls an aggregate query that returns a list of genre ranks based on
        /// ticket sales within a specified time frame
        /// </summary>
        /// <returns>A list of structs containing the resulting ranks for each genre
        /// in the given time frame</returns>
        List<GenreRanksResult> GetGenreRanks();

        /// <summary>
        /// A function that calls an aggregate query that returns a list of rankings for all hours
        /// of showtimes within a specified time frame
        /// </summary>
        /// <param name="StartTime">the beginning date for the rank assesment</param>
        /// <param name="EndTime">the end date for the rank assesment</param>
        /// <returns>A list of structs containing the resulting ranks for each hour that
        /// showtimes were shown in the given time frame</returns>
        List<HourlySalesResult> GetSalesPerHourOfTheDay(DateTimeOffset startTime,
            DateTimeOffset endTime);

        /// <summary>
        /// A function that calls an aggregate query that returns a list of rankings for top theaters
        /// in terms of sales for each month, year to date
        /// </summary>
        /// <returns>A list of structs containing the resulting monthly ranks for each theater and
        /// the sales attributed to each theater</returns>
        List<TopTheatersResult> GetTopTheaters();

        /// <summary>
        /// A function that calls an aggregate query that returns a list of movie ranks based on
        /// ticket sales within a specified time frame as well as Total Showings and Average Ticket
        /// Sales per Showing
        /// </summary>
        /// <returns>A list of structs containing the resulting ranks for each movie in the
        /// given time frame and the associated total showings and average ticket sales</returns>
        List<TopMoviesResult> MovieStatistics();
    }
}

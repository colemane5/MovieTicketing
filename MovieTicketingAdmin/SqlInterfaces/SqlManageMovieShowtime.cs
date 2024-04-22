using Microsoft.Data.SqlClient;
using MovieTicketingAdmin.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingAdmin.SqlInterfaces
{
    public class SqlManageMovieShowtime : IManageMovieShowtime
    {
        private readonly string connectionString;

        public SqlManageMovieShowtime(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageMovieShowtime(string task, int movieId, int theaterId, DateTimeOffset startOn, DateTimeOffset newStartOn)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Showtime> FindShowtimes(int movieId, int theaterId)
        {
            var showTimes = new List<Showtime>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("FindShowtimes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("MovieID", movieId);

                    command.Parameters.AddWithValue("TheaterID", theaterId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var startOnOrdinal = reader.GetOrdinal("StartOn");
                        var seatsAvailableOrdinal = reader.GetOrdinal("SeatsAvailable");
                        var salePriceOrdinal = reader.GetOrdinal("SalePrice");

                        while (reader.Read())
                        {
                            showTimes.Add(new Showtime(
                                   movieId,
                                   theaterId,
                                   reader.GetDateTimeOffset(startOnOrdinal).DateTime,
                                   reader.GetDecimal(salePriceOrdinal),
                                   reader.GetInt32(seatsAvailableOrdinal)));
                        }
                    }

                    connection.Close();

                    return showTimes;
                }
            }
        }        
    }
}

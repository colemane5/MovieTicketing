using Microsoft.Data.SqlClient;
using MovieTicketingAdmin.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            int result = -1;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Person.SavePersonAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("Task", task);
                        command.Parameters.AddWithValue("MovieID", movieId);
                        command.Parameters.AddWithValue("TheaterID", theaterId);
                        command.Parameters.AddWithValue("StartOn", startOn);
                        command.Parameters.AddWithValue("NewStartOn", newStartOn);
                        command.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                        connection.Open();

                        command.ExecuteNonQuery();

                        result = Convert.ToInt32(command.Parameters["Result"].Value);

                        transaction.Complete();

                        return result;
                    }
                }
            }
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
                                   0, // REPLACE WITH showtimeID
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

        public IReadOnlyList<Movie> RetrieveMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveMovies", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var movieIdOrdinal = reader.GetOrdinal("MovieID");
                        var movieTitleOrdinal = reader.GetOrdinal("MovieTitle");
                        var releaseDateOrdinal = reader.GetOrdinal("ReleaseDate");
                        var descriptionOrdinal = reader.GetOrdinal("MovieDescription");

                        while (reader.Read())
                        {
                            movies.Add(new Movie(
                               reader.GetInt32(movieIdOrdinal),
                               reader.GetString(movieTitleOrdinal),
                               reader.GetDateTime(releaseDateOrdinal).ToString(),
                               reader.GetString(descriptionOrdinal)));
                        }
                    }

                    connection.Close();

                    return movies;
                }
            }
        }

        public IReadOnlyList<Theater> RetrieveTheaters()
        {
            var theaters = new List<Theater>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveActors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var theaterIDOrdinal = reader.GetOrdinal("TheaterID");
                        var theaterNameOrdinal = reader.GetOrdinal("TheaterName");
                        var theaterAddressOrdinal = reader.GetOrdinal("TheaterAddress");

                        while (reader.Read())
                        {
                            theaters.Add(new Theater(
                                   reader.GetInt32(theaterIDOrdinal),
                                   reader.GetString(theaterNameOrdinal),
                                   reader.GetString(theaterAddressOrdinal)));
                        }
                    }

                    connection.Close();

                    return theaters;
                }
            }
        }
    }
}

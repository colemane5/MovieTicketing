﻿using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System.Data;
using System.Transactions;

namespace SharedResources.SqlInterfaces
{
    public class SqlManageMovieShowtime : IManageMovieShowtime
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageMovieShowtime(string task, int movieId, int theaterId, DateTimeOffset startOn, DateTimeOffset? newStartOn)
        {
            int result = -1;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("ManageMovieShowtimes", connection))
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

        public IReadOnlyList<Showtime> FindShowtimes(int movieID, int theaterID)
        {
            var showTimes = new List<Showtime>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("FindShowtimes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("MovieID", movieID);

                    command.Parameters.AddWithValue("TheaterID", theaterID);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var showTimeIdOrdinal = reader.GetOrdinal("MovieShowtimeID");
                        var startOnOrdinal = reader.GetOrdinal("StartOn");
                        var seatsLeftOrdinal = reader.GetOrdinal("SeatsLeft");
                        var salePriceOrdinal = reader.GetOrdinal("SalePrice");

                        while (reader.Read())
                        {
                            showTimes.Add(new Showtime(
                                   reader.GetInt32(showTimeIdOrdinal),
                                   movieID,
                                   theaterID,
                                   reader.GetDateTime(startOnOrdinal),
                                   reader.IsDBNull(salePriceOrdinal) ? 0 : reader.GetInt32(salePriceOrdinal),
                                   reader.GetInt32(seatsLeftOrdinal)));
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
                        var descriptionOrdinal = reader.GetOrdinal("Description");

                        while (reader.Read())
                        {
                            movies.Add(new Movie(
                               reader.GetInt32(movieIdOrdinal),
                               reader.GetString(movieTitleOrdinal),
                               reader.GetDateTimeOffset(releaseDateOrdinal).DateTime,
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
                using (var command = new SqlCommand("RetrieveTheaters", connection))
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

using SharedResources.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace MovieTicketingClient.SqlInterfaces
{
    public class SqlTheaterRepository : ITheaterRepository
    {
        private readonly string connectionString;

        public SqlTheaterRepository(string connectionString)
        {
            this.connectionString = connectionString;
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
                        var startOnOrdinal = reader.GetOrdinal("StartOn");
                        var seatsAvailableOrdinal = reader.GetOrdinal("SeatsAvailable");
                        var salePriceOrdinal = reader.GetOrdinal("SalePrice");

                        while (reader.Read())
                        {
                            showTimes.Add(new Showtime(
                                   0, // REPLACE WITH showtimeID
                                   movieID,
                                   theaterID,
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

        public string GetTicket(int userID, int MovieShowtimeID, decimal salePrice, int seatsLeft)
        {
            string result = string.Empty;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Person.SavePersonAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("UserID", userID);
                        command.Parameters.AddWithValue("MovieShowtimeID", MovieShowtimeID);
                        command.Parameters.AddWithValue("SalePrice", salePrice);
                        command.Parameters.AddWithValue("SeatsLeft", seatsLeft);
                        command.Parameters.Add("Result", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                        connection.Open();

                        command.ExecuteNonQuery();
                        result = command.Parameters["Result"].Value.ToString();

                        transaction.Complete();

                        return result;
                    }
                }
            }
        }

        public IReadOnlyList<Theater> RetrieveTheaters(int movieID)
        {
            var theaters = new List<Theater>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveActors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("MovieID", movieID);

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

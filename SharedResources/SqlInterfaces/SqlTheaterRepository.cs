using SharedResources.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using SharedResources.SqlInterfaces.Interfaces;

namespace SharedResources.SqlInterfaces
{
    public class SqlTheaterRepository : ITheaterRepository
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

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
                                   reader.IsDBNull(salePriceOrdinal) ? 10 : reader.GetInt32(salePriceOrdinal),
                                   reader.GetInt32(seatsLeftOrdinal)));
                        }
                    }

                    connection.Close();

                    return showTimes;
                }
            }
        }

        public bool GetTicket(int userID, int MovieShowtimeID, decimal salePrice)
        {
            bool result = false;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetTicket", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("UserID", userID);
                        command.Parameters.AddWithValue("MovieShowtimeID", MovieShowtimeID);
                        command.Parameters.AddWithValue("SalePrice", salePrice);
                        command.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                        connection.Open();

                        command.ExecuteNonQuery();
                        result = (bool)command.Parameters["Result"].Value;

                        transaction.Complete();
                    }
                }
            }

            if (result)
            {
                using (var transaction = new TransactionScope())
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        using (var command = new SqlCommand("UpdateSeatsLeft", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("MovieShowtimeID", MovieShowtimeID);

                            connection.Open();

                            command.ExecuteNonQuery();

                            transaction.Complete();
                        }
                    }
                }

                return true;
            }
            return false;
        }

        public IReadOnlyList<Theater> RetrieveTheaters(int movieID)
        {
            var theaters = new List<Theater>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveTheaters", connection))
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

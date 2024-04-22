using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System.Data;

namespace SharedResources.SqlInterfaces
{
    public class SqlManageTheater : IManageTheater
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageTheater(string task, int theaterId, string name, string address)
        {
            throw new NotImplementedException();
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

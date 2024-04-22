using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System.Data;

namespace SharedResources.SqlInterfaces
{
    public class SqlManageDirector : IManageDirector
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageDirector(string task, int directorId, string directorName, DateTimeOffset directorDoB)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Director> RetrieveDirectors()
        {
            var directors = new List<Director>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveDirectors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var directorIdOrdinal = reader.GetOrdinal("DirectorID");
                        var directorNameOrdinal = reader.GetOrdinal("DirectorName");
                        var directorDoBOrdinal = reader.GetOrdinal("DirectorDateOfBirth");

                        while (reader.Read())
                        {
                            directors.Add(new Director(
                               reader.GetInt32(directorIdOrdinal),
                               reader.GetString(directorNameOrdinal),
                               reader.GetDateTime(directorDoBOrdinal)));
                        }
                    }

                    connection.Close();

                    return directors;
                }
            }
        }
    }
}

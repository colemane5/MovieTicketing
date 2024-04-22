using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System.Data;

namespace SharedResources.SqlInterfaces
{    
    public class SqlManageActor : IManageActor
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageActor(string task, int actorId, string actorName, DateTimeOffset actorDoB)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Actor> RetrieveActors()
        {
            var actors = new List<Actor>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveActors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var actorIdOrdinal = reader.GetOrdinal("ActorID");
                        var actorNameOrdinal = reader.GetOrdinal("ActorName");
                        var actorDoBOrdinal = reader.GetOrdinal("ActorDateOfBirth");

                        while (reader.Read())
                        {
                            actors.Add(new Actor(
                               reader.GetInt32(actorIdOrdinal),
                               reader.GetString(actorNameOrdinal),
                               reader.GetDateTime(actorDoBOrdinal)));
                        }
                    }

                    connection.Close();

                    return actors;
                }
            }
        }
    }
}

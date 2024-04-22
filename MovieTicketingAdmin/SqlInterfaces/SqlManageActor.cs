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
    public class SqlManageActor : IManageActor
    {
        private readonly string connectionString;

        public SqlManageActor(string connectionString)
        {
            this.connectionString = connectionString;
        }

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
                        var actorNameOrdinal = reader.GetOrdinal("ActorName");
                        var actorDoBOrdinal = reader.GetOrdinal("ActorDateOfBirth");

                        while (reader.Read())
                        {
                            actors.Add(new Actor(
                               0, // REPLACE WITH actorID
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

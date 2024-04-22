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
    public class SqlManageDirector : IManageDirector
    {
        private readonly string connectionString;

        public SqlManageDirector(string connectionString)
        {
            this.connectionString = connectionString;
        }

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
                using (var command = new SqlCommand("RetrieveActors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var directorNameOrdinal = reader.GetOrdinal("DirectorName");
                        var directorDoBOrdinal = reader.GetOrdinal("DirectorDateOfBirth");

                        while (reader.Read())
                        {
                            directors.Add(new Director(
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

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
    public class SqlManageMovie : IManageMovie
    {
        private readonly string connectionString;

        public SqlManageMovie(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // MAKE SURE TO ONLY USE "ADD", "UPDATE", OR "DELETE" for task string
        // returns 0 for completion and -1 for failure for UPDATE and DELETE
        // returns the Primary Key of the inserted row if successful for ADD
        public int ManageMovie(string task, int movieId, string title, DateOnly releaseDate, string description, int genreId)
        {
            throw new NotImplementedException();
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
    }
}

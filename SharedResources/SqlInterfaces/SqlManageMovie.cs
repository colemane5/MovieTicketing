﻿using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Models;
using System.Data;

namespace SharedResources.SqlInterfaces
{
    public class SqlManageMovie : IManageMovie
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

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
                               reader.GetDateTime(releaseDateOrdinal),
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

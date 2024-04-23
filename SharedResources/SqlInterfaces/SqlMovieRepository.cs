using SharedResources.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using SharedResources.SqlInterfaces.Interfaces;
using System.Collections.ObjectModel;

namespace SharedResources.SqlInterfaces
{
    public class SqlMovieRepository : IMovieRepository
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        public IReadOnlyList<Movie> FilterMovies(string? movieTitle, string? actorNames, string? director, string? genre)
        {
            var movies = new List<Movie>();

            //Check to see if all fields are null, if so run Retrieve Movies instead
            if (movieTitle == null && actorNames == null && director == null && genre == null)
                return RetrieveMovies();

            else
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("FilterMovies", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (movieTitle != null) command.Parameters.AddWithValue("MovieTitle", movieTitle);
                        if (actorNames != null) command.Parameters.AddWithValue("ActorNames", actorNames);
                        if (director != null) command.Parameters.AddWithValue("Director", director);
                        if (genre != null) command.Parameters.AddWithValue("GenreName", genre);

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

        public List<Actor> RetrieveActors()
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

        public List<Director> RetrieveDirectors()
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

        public List<Genre> RetrieveGenres()
        {
            var genres = new List<Genre>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveGenres", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var genreIdOrdinal = reader.GetOrdinal("GenreID");
                        var genreNameOrdinal = reader.GetOrdinal("GenreName");

                        while (reader.Read())
                        {
                            genres.Add(new Genre(
                                reader.GetInt32(genreIdOrdinal),
                                reader.GetString(genreNameOrdinal)));
                        }
                    }

                    connection.Close();

                    return genres;
                }
            }
        }
    }
}

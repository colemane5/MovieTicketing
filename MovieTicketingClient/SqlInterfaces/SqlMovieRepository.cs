﻿using SharedResources.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection.Emit;
using System.Windows.Shapes;
using System.IO;

namespace MovieTicketingClient.SqlInterfaces
{
    class SqlMovieRepository : IMovieRepository
    {
        private readonly string connectionString;

        public SqlMovieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        IReadOnlyList<Movie> IMovieRepository.FilterMovies(string movieTitle, string actorNames, string director, string genre)
        {
            var movies = new List<Movie>();

            //Check to see if all fields are null, if so run Retrieve Movies instead
            if (movieTitle == null && actorNames == null && director == null && genre == null)
                return new List<Movie>();

            else
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("FilterMovies", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("MovieTitle", movieTitle);

                        command.Parameters.AddWithValue("ActorNames", actorNames);

                        command.Parameters.AddWithValue("Director", director);

                        command.Parameters.AddWithValue("GenreName", genre);

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
                                   reader.GetString(releaseDateOrdinal),
                                   reader.GetString(descriptionOrdinal)));
                            }
                        }

                        connection.Close();
                        
                        return movies;
                    }
                }
            }
        }

        IReadOnlyList<Movie> IMovieRepository.RetrieveMovies()
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

        List<Actor> IMovieRepository.RetrieveActors()
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
                               reader.GetString(actorNameOrdinal),
                               reader.GetDateTime(actorDoBOrdinal)));
                        }
                    }

                    connection.Close();

                    return actors;
                }
            }
        }

        List<Director> IMovieRepository.RetrieveDirectors()
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

        List<string> IMovieRepository.RetrieveGenres()
        {
            var genres = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("RetrieveActors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var genreNameOrdinal = reader.GetOrdinal("GenreName");

                        while (reader.Read())
                        {
                            genres.Add(reader.GetString(genreNameOrdinal));
                        }
                    }

                    connection.Close();

                    return genres;
                }
            }
        }
    }
}
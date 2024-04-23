﻿using Microsoft.Data.SqlClient;
using SharedResources.SqlInterfaces.Interfaces;
using SharedResources.Results;
using System.Data;
using System.Transactions;
using System.Collections.ObjectModel;

namespace SharedResources.SqlInterfaces
{
    public class SqlAggregateQueryRepo : IAggregateQueryRepo
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        public List<GenreRanksResult> GetGenreRanks()
        {
            var genreRanksList = new List<GenreRanksResult>();

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetGenreRanks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            var rankOrdinal = reader.GetOrdinal("Rank");
                            var genreNameOrdinal = reader.GetOrdinal("GenreName");
                            var ticketsSoldOrdinal = reader.GetOrdinal("TicketsSold");

                            while (reader.Read())
                            {
                                genreRanksList.Add(new GenreRanksResult(
                                    (int)reader.GetInt64(rankOrdinal),
                                    reader.GetString(genreNameOrdinal),
                                    (int)reader.GetInt64(ticketsSoldOrdinal)));
                            }
                        }
                        connection.Close();

                        return genreRanksList;
                    }
                }
            }
        }

        public List<HourlySalesResult> GetSalesPerHourOfTheDay(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var hourlySalesList = new List<HourlySalesResult>();

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetSalesPerHourOfTheDay", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("StartTime", startTime);
                        command.Parameters.AddWithValue("EndTime", endTime);

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            var hourOrdinal = reader.GetOrdinal("HourOfDay");
                            var moviesCountOrdinal = reader.GetOrdinal("UniqueMovies");
                            var theatersCountOrdinal = reader.GetOrdinal("UniqueTheaters");
                            var ticketSalesOrdinal = reader.GetOrdinal("TicketSales");

                            while (reader.Read())
                            {
                                hourlySalesList.Add(new HourlySalesResult(
                                    reader.GetInt32(hourOrdinal),
                                    reader.GetInt32(moviesCountOrdinal),
                                    reader.GetInt32(theatersCountOrdinal),
                                    reader.GetDecimal(ticketSalesOrdinal)));
                            }
                        }
                        connection.Close();

                        return hourlySalesList;
                    }
                }
            }
        }

        public List<TopTheatersResult> GetTopTheaters()
        {
            var topTheaterList = new List<TopTheatersResult>();

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetTopTheaters", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            var saleMonthOrdinal = reader.GetOrdinal("SaleMonth");
                            var theaterNameOrdinal = reader.GetOrdinal("TheaterName");
                            var theaterAddressOrdinal = reader.GetOrdinal("TheaterAddress");
                            var ticketSalesOrdinal = reader.GetOrdinal("TicketSales");
                            var rankOrdinal = reader.GetOrdinal("Rank");

                            while (reader.Read())
                            {
                                topTheaterList.Add(new TopTheatersResult(
                                    reader.GetInt32(saleMonthOrdinal),
                                    reader.GetInt32(rankOrdinal),
                                    reader.GetString(theaterNameOrdinal),
                                    reader.GetString(theaterAddressOrdinal),
                                    reader.GetDecimal(ticketSalesOrdinal)));
                            }
                        }
                        connection.Close();

                        return topTheaterList;
                    }
                }
            }
        }

        public List<TopMoviesResult> MovieStatistics()
        {
            var topMovieList = new List<TopMoviesResult>();

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("MovieStatistics", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            var movieTitleOrdinal = reader.GetOrdinal("MovieTitle");
                            var ticketSalesOrdinal = reader.GetOrdinal("TicketSales");
                            var showingsOrdinal = reader.GetOrdinal("TotalShowings");
                            var avgTicketSalesOrdinal = reader.GetOrdinal("AvgTicketsPerShowing");

                            while (reader.Read())
                            {
                                topMovieList.Add(new TopMoviesResult(
                                    reader.GetString(movieTitleOrdinal),
                                    reader.GetInt32(ticketSalesOrdinal),
                                    reader.GetInt32(showingsOrdinal),
                                    reader.GetFloat(avgTicketSalesOrdinal)));
                            }
                        }
                        connection.Close();

                        return topMovieList;
                    }
                }
            }
        }
    }
}

using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.SqlInterfaces
{
    class SqlMovieRepository : IMovieRepository
    {
        List<Movie> IMovieRepository.FilterMovies(string movieTitle, string actorNames, string director, string genre)
        {
            throw new NotImplementedException();
        }

        List<Actor> IMovieRepository.RetrieveActors()
        {
            throw new NotImplementedException();
        }

        List<Director> IMovieRepository.RetrieveDirectors()
        {
            throw new NotImplementedException();
        }

        List<string> IMovieRepository.RetrieveGenres()
        {
            throw new NotImplementedException();
        }
    }
}

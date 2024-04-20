using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.Delegates
{
    public interface IMovieRepository
    {
        /// <summary>
        /// A delegate function to feed in movie search filter parameters
        /// ** NOTE ** The values can be set to NULL to leave the filter empty ** NOTE **
        /// </summary>
        /// <param name="movieTitle">the title of the desired movie</param>
        /// <param name="actorNames">the actor names with only commas "," in between</param>
        /// <param name="director">the desired director filter</param>
        /// <param name="genre">the desired genre for the search</param>
        /// <returns>A read-only list with the desired movies from the database</returns>
        IReadOnlyList<Movie> FilterMovies(string movieTitle, string actorNames, string director, string genre);

        /// <summary>
        /// A function to return all actors on the db to use in the filter function
        /// </summary>
        /// <returns>A list of actors in the db</returns>
        List<Actor> RetrieveActors();

        /// <summary>
        /// A function to return all directors on the db to use in the filter function
        /// </summary>
        /// <returns>A list of directors in the db</returns>
        List<Director> RetrieveDirectors();

        /// <summary>
        /// A function to return all genres on the db to use in the filter function
        /// </summary>
        /// <returns>A list of genres in the db</returns>
        List<string> RetrieveGenres();
    }
}

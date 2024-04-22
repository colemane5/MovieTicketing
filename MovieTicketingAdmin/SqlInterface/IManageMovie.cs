using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace MovieTicketingAdmin.SqlInterface
{
    public interface IManageMovie
    {
        /// <summary>
        /// A function that allows the admin to Add, Edit, or Remove Movies in the db
        /// </summary>
        /// <param name="task">a string that contains the intended action
        /// VALUES: "ADD", "UPDATE", OR "REMOVE"</param>
        /// <param name="movieId">the Movie ID intended for an Edit or Removal</param>
        /// <param name="title">the new Movie title for Add or Edit</param>
        /// <param name="releaseDate">the new Movie release date for Add or Edit</param>
        /// <param name="description">the new Movie description for Add or Edit</param>
        /// <param name="genreId">the new Movie Genre Id for Add or Edit</param>
        /// <returns>an int indicating if an update or removal was successful or not</returns>
        public int ManageMovie(string task, int movieId, string title,
            DateOnly releaseDate, string description, int genreId);

        /// <summary>
        /// A function that returns all movies contained in the db for boot up and if no parameters are provided
        /// </summary>
        /// <returns>A read-only list containing all the movies in the db</returns>
        public IReadOnlyList<Movie> RetrieveMovies();
    }
}

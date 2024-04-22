using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace SharedResources.SqlInterfaces.Interfaces
{
    public interface IManageActor
    {
        /// <summary>
        /// A function that allows the admin to Add, Edit, or Remove Actors in the db
        /// </summary>
        /// <param name="task">a string that contains the intended action
        /// VALUES: "ADD", "UPDATE", OR "REMOVE"</param>
        /// <param name="actorId">the actor ID intended for an Edit or Removal</param>
        /// <param name="actorName">the new Actor name for Add or Edit</param>
        /// <param name="actorDoB">the new Actor date of birth for Add or Edit</param>
        /// <returns>an int indicating if an update or removal was successful or not</returns>
        public int ManageActor(string task, int actorId, string actorName, DateTimeOffset actorDoB);

        /// A function to return all actors on the db to use in the filter function
        /// </summary>
        /// <returns>A list of actors in the db</returns>
        public IReadOnlyList<Actor> RetrieveActors();
    }
}

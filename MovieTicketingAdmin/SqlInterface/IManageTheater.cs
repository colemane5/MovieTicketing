using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace MovieTicketingAdmin.SqlInterface
{
    public interface IManageTheater
    {
        /// <summary>
        /// A function that allows the admin to Add, Edit, or Remove Theaters in the db
        /// </summary>
        /// <param name="task">a string that contains the intended action
        /// VALUES: "ADD", "UPDATE", OR "REMOVE"</param>
        /// <param name="theaterId">the Theater ID intended for an Edit or Removal</param>
        /// <param name="name">the new Theater name for Add or Edit</param>
        /// <param name="address">the new Theater address for Add or Edit</param>
        /// <returns>an int indicating if an update or removal was successful or not</returns>
        public int ManageTheater(string task, int theaterId, string name, string address);

        /// <summary>
        /// A function to return all theaters on the db to use in the filter function
        /// </summary>
        /// <returns>A read-only list of theaters in the db</returns>
        public IReadOnlyList<Theater> RetrieveTheaters();
    }
}

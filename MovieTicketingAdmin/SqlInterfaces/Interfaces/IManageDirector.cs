﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace MovieTicketingAdmin.SqlInterfaces.Interfaces
{
    public interface IManageDirector
    {
        /// <summary>
        /// A function that allows the admin to Add, Edit, or Remove Directors in the db
        /// </summary>
        /// <param name="task">a string that contains the intended action
        /// VALUES: "ADD", "UPDATE", OR "REMOVE"</param>
        /// <param name="directorId">the director ID intended for an Edit or Removal</param>
        /// <param name="directorName">the new Director name for Add or Edit</param>
        /// <param name="directorDoB">the new Director date of birth for Add or Edit</param>
        /// <returns>an int indicating if an update or removal was successful or not</returns>
        public int ManageDirector(string task, int directorId, string directorName,
            DateTimeOffset directorDoB);

        /// <summary>
        /// A function to return all directors on the db to use in the filter function
        /// </summary>
        /// <returns>A list of directors in the db</returns>
        public IReadOnlyList<Director> RetrieveDirectors();
    }
}
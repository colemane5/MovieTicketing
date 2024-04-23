using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace SharedResources.SqlInterfaces.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// A function to verify is email is contained in user database a returns the user
        /// with that email and changes their loggedin value on the db
        /// </summary>
        /// <param name="email">the email of the desired user</param>
        /// <returns>returns the user with the given email, or null if the user cannot be logged in.</returns>
        User? LoginUser(string email);

        /// <summary>
        /// A function that flips the loggined value of the user with the given email to not logged in
        /// </summary>
        /// <param name="email">the email of the desired user</param>
        void LogoutUser(string email);
    }
}

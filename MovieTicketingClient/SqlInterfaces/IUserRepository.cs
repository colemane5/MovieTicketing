using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingClient.SqlInterfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// A function to verify is email is contained in user database
        /// </summary>
        /// <param name="email">the email to check if in database</param>
        /// <returns>returns a bool stating whether successful and an int containing user ID</returns>
        (bool, int) LoginUser(string email);
    }
}

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
    class SqlUserRepository : IUserRepository
    {
        (bool, int) IUserRepository.LoginUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}

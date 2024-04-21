using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace MovieTicketingClient.SqlInterfaces
{
    class SqlUserRepository : IUserRepository
    {
        private readonly string connectionString;

        public SqlUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        bool IUserRepository.LoginUser(string email)
        {
            string Bit = "0";

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Person.SavePersonAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("UserEmail", email);
                        command.Parameters.Add("IsLoggedIn", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        command.Parameters.Add("UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        connection.Open();

                        command.ExecuteNonQuery();

                        if (command.Parameters["UserID"].Value != DBNull.Value)
                            Bit = command.Parameters["IsLoggedIn"].Value.ToString();

                        transaction.Complete();

                        if (Bit.Equals("1")) return true;
                        else return false;
                    }
                }
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using SharedResources.Models;
using SharedResources.SqlInterfaces.Interfaces;

namespace SharedResources.SqlInterfaces
{
    public class SqlUserRepository : IUserRepository
    {
        // CHANGE THIS STRING TO MATCH THE LOCATION OF THE DB FOR YOUR MACHINE
        // THIS INSTANCE IS USED TO RUN THE DB FROM A LOCAL INSTANCE AT MovieDB
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MovieDB;Integrated Security=true;";

        public User? LoginUser(string email)
        {
            User? user = null;

            using(var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CheckLoginStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("UserEmail", email);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var userIDOrdinal = reader.GetOrdinal("UserID");
                            var userNameOrdinal = reader.GetOrdinal("UserName");
                            var isAdminOrdinal = reader.GetOrdinal("IsAdmin");

                            while (reader.Read())
                            {
                                user = new User(
                                   reader.GetInt32(userIDOrdinal),
                                   reader.GetString(userNameOrdinal),
                                   email,
                                   reader.GetBoolean(isAdminOrdinal));
                            } 
                        }
                    }

                    connection.Close();
                }
            }

            if (user is null) return null;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("LoginUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("UserEmail", email);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();
                    }
                }
            }

            return (User)user;
        }

        public void LogoutUser(string email)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("LogoutProcedure", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("UserEmail", email);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();
                    }
                }
            }
        }
    }
}

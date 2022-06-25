using Microsoft.Extensions.Options;
using Npgsql;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.User;
using SocialNetworkBackEnd.Properties;
using System;
using System.Collections.Generic;
using System.Data;

namespace SocialNetworkBackEnd.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly DbConnectionKeys ConnectionKeys;
        public UserRepository(IOptions<DbConnectionKeys> connectionKeys)
        {
            ConnectionKeys = connectionKeys.Value;
        }
        public IEnumerable<UserDB> GetUsers(bool showIsDeleted)
        {
            List<UserDB> users = new List<UserDB>();
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                using NpgsqlCommand selectAllUsers = new NpgsqlCommand("select * from users where is_deleted = @param", connection);
                selectAllUsers.Parameters.AddWithValue("@param", showIsDeleted);
                using NpgsqlDataReader reader = selectAllUsers.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(UserDbFromReader(reader));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN UserRepository.cs (GetUsers) >>> {error}");
            }
            return users;

        }
        public UserDB GetUserById(Guid id)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                using NpgsqlCommand command = new NpgsqlCommand($"select * from users where id = @id", conn);
                command.Parameters.AddWithValue("@id", id);
                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return UserDbFromReader(reader);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR in UserRepository.cs (GetUserById) >>> {error}");
            }
            return null;
        }



        public bool AddUser(UserDB user)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                using NpgsqlCommand command = new NpgsqlCommand($"insert into users " +
                    $"(id, name, surname, age, avatar, description, status, creation_date, email, password) " +
                    $"values(@id, @name, @surname, @age, @avatar, @description, @status, @creation_date, @email, @password)", conn)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", Guid.NewGuid()),
                        new NpgsqlParameter("@name", user.Name),
                        new NpgsqlParameter("@surname", user.Surname),
                        new NpgsqlParameter("@age", user.Age == null ? DBNull.Value : user.Age),
                        new NpgsqlParameter("@avatar", user.Avatar == null ? DBNull.Value : user.Avatar),
                        new NpgsqlParameter("@description", user.Description == null ? DBNull.Value : user.Description),
                        new NpgsqlParameter("@status", user.Status == null ? DBNull.Value : user.Status),
                        new NpgsqlParameter("@creation_date", user.CreationDate),
                        new NpgsqlParameter("@email", user.Email),
                        new NpgsqlParameter("@password", user.Password),
                    }
                };
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occured in UserRepository.cs (AddUser) >>> {error.Message}");
            }
            return false;
        }
        public bool DeleteUser(Guid id)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                using NpgsqlCommand command =
                    new NpgsqlCommand($"update users set is_deleted = true " +
                    $"where id = @id AND is_deleted = false AND is_admin = false", conn);

                command.Parameters.AddWithValue("@id", id);
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occured in UserRepository.cs (DeleteUser) >>> {error.Message}");
            }
            return false;
        }
        public bool EditUser(UserDB user)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                using NpgsqlCommand command = new NpgsqlCommand($"update users set " +
                    $"name = @name," +
                    $"surname = @surname," +
                    $"age = @age," +
                    $"avatar = @avatar," +
                    $"description = @description," +
                    $"status = @status," +
                    $"modified_date = @modified_date " +
                    $"where id = @id AND is_deleted = false;", conn)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@name", user.Name),
                        new NpgsqlParameter("@surname", user.Surname),
                        new NpgsqlParameter("@age", user.Age == null ? DBNull.Value : user.Age),
                        new NpgsqlParameter("@avatar", user.Avatar == null ? DBNull.Value : user.Avatar),
                        new NpgsqlParameter("@description", user.Description == null ? DBNull.Value : user.Description),
                        new NpgsqlParameter("@status", user.Status == null ? DBNull.Value : user.Status),
                        new NpgsqlParameter("@modified_date", user.ModifiedDate),
                        new NpgsqlParameter("@id", user.Id),
                    }
                };
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occured in UserRepository.cs (EditUser) >>> {error.Message}");
            }
            return false;
        }

        public UserDB FindUser(string email)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                string query = "select * from users where email = @email";
                using NpgsqlCommand command = new NpgsqlCommand(query, conn)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@email", email),
                    }
                };
                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return UserDbFromReader(reader);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occured in UserRepository.cs (FindUser) >>> {error.Message}");
            }
            return null;
        }

        public bool AdminCheck(Guid id)
        {
            try
            {
                using NpgsqlConnection conn = ConnectionKeys.ConfigureDbConnection();
                conn.Open();
                string query = "select is_admin from users where id = @id";
                using NpgsqlCommand command = new NpgsqlCommand(query, conn)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                    }
                };
                bool result = (bool)command.ExecuteScalar();
                return result;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occured in UserRepository.cs (FindUser) >>> {error.Message}");
            }
            return false;
        }

        UserDB UserDbFromReader(NpgsqlDataReader reader)
        {
            return new UserDB(
                (Guid)reader.GetValue("id"),
                (string)reader.GetValue("name"),
                (string)reader.GetValue("surname"),
                Utils.ConvertFromDBVal<int>(reader.GetValue("age")),
                Utils.ConvertFromDBVal<string>(reader.GetValue("avatar")),
                Utils.ConvertFromDBVal<string>(reader.GetValue("description")),
                Utils.ConvertFromDBVal<string>(reader.GetValue("status")),
                Utils.ConvertFromDBVal<DateTime>(reader.GetValue("creation_date")),
                Utils.ConvertFromDBVal<DateTime>(reader.GetValue("modified_date")),
                (bool)reader.GetValue("is_deleted"),
                (string)reader.GetValue("email"),
                (string)reader.GetValue("password")
                );
        }
    }
}

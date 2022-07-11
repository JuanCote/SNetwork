using Microsoft.Extensions.Options;
using Npgsql;
using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub;
using SocialNetworkBackEnd.Models.User;
using SocialNetworkBackEnd.Properties;
using System;
using System.Collections.Generic;
using System.Data;

namespace SocialNetworkBackEnd.Repository
{
    public class SubRepository : ISubRepository
    {
        readonly DbConnectionKeys ConnectionKeys;
        public SubRepository(IOptions<DbConnectionKeys> connectionKeys)
        {
            ConnectionKeys = connectionKeys.Value;
        }
        public bool AddSubscription(SubDB sub)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    $"insert into subscriptions values(@id, @userId, @subId, @isActive, @date)";
                using NpgsqlCommand addPost = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", sub.Id),
                        new NpgsqlParameter("@userId", sub.UserId),
                        new NpgsqlParameter("@subId", sub.SubId),
                        new NpgsqlParameter("@isActive", sub.IsActive),
                        new NpgsqlParameter("@date", sub.SubTime),
                    }
                };
                int rows = addPost.ExecuteNonQuery();
                return rows == 1 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN SubRepository.cs (subscription) >>> {error}");
            }
            return false;
        }
        public SubResult CheckForEntity(Guid? userId, Guid subId)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    $"select id, is_active from subscriptions where user_id = @id and sub_id = @sub_id";
                using NpgsqlCommand command = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", userId),
                        new NpgsqlParameter("@sub_id", subId),
                    }
                };
                SubResult subresult = new SubResult();
                subresult.SubId = null;
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    subresult.SubId =  reader.GetGuid(0);
                    subresult.isActive = reader.GetBoolean(1);
                }
                return subresult;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN SubRepository.cs (CheckForEntity) >>> {error}");
            }
            return null;
        }
        public bool UpdateSubStatus(Guid? id, bool subStatus)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    $"update subscriptions set is_active = @status where id = @id";
                using NpgsqlCommand command = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                        new NpgsqlParameter("@status", subStatus),
                    }
                };
                int rows = command.ExecuteNonQuery();
                return rows == 1 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN SubRepository.cs (UpdateSubStatus) >>> {error}");
            }
            return false;
        }
        public IEnumerable<UserDB> GetFollowers(Guid id)
        {
            List<UserDB> usersResult = new List<UserDB>();
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    $"select * from users where id in " +
                    $"(select user_id from subscriptions where sub_id = @id and is_active = true)";
                using NpgsqlCommand command = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                    }
                };
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usersResult.Add(UserDbFromReader(reader));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN SubRepository.cs (GetFollowers) >>> {error}");
            }
            return usersResult;
        }

        public IEnumerable<UserDB> GetSubscribers(Guid id)
        {
            List<UserDB> usersResult = new List<UserDB>();
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    $"select * from users where id in " +
                    $"(select sub_id from subscriptions where user_id = @id and is_active = true)";
                using NpgsqlCommand command = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                    }
                };
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usersResult.Add(UserDbFromReader(reader));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN SubRepository.cs (GetSubscribers) >>> {error}");
            }
            return usersResult;
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

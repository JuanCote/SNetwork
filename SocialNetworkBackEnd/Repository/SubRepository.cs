using Microsoft.Extensions.Options;
using Npgsql;
using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub;
using SocialNetworkBackEnd.Properties;
using System;

namespace SocialNetworkBackEnd.Repository
{
    public class SubRepository : ISubRepository
    {
        readonly DbConnectionKeys ConnectionKeys;
        public SubRepository(IOptions<DbConnectionKeys> connectionKeys)
        {
            ConnectionKeys = connectionKeys.Value;
        }
        public bool Subscription(SubDB sub)
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
    }
}

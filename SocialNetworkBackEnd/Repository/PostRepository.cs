using Microsoft.Extensions.Options;
using Npgsql;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.Post;
using SocialNetworkBackEnd.Properties;
using System;
using System.Collections.Generic;
using System.Data;

namespace SocialNetworkBackEnd.Repository
{
    public class PostRepository : IPostRepository
    {
        readonly DbConnectionKeys ConnectionKeys;
        public PostRepository(IOptions<DbConnectionKeys> connectionKeys)
        {
            ConnectionKeys = connectionKeys.Value;
        }
        public IEnumerable<PostDB> GetPosts(Guid id, bool showIsDeleted)
        {
            List<PostDB> posts = new List<PostDB>();
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                var query =
                    "select posts.*, name, surname, avatar " +
                    "from posts join users " +
                    "on users.id = posts.user_id " +
                    $"where posts.is_deleted = @param and posts.user_id = @id";
                using NpgsqlCommand selectPosts = new NpgsqlCommand(query, connection);
                selectPosts.Parameters.AddWithValue("@param", showIsDeleted);
                selectPosts.Parameters.AddWithValue("@id", id);
                using NpgsqlDataReader reader = selectPosts.ExecuteReader();
                while (reader.Read())
                {
                    posts.Add(new PostDB(
                        (Guid)reader.GetValue("id"),
                        (Guid)reader.GetValue("user_id"),
                        (string)reader.GetValue("post_text"),
                        (DateTime)reader.GetValue("creation_date"),
                        Utils.ConvertFromDBVal<DateTime>(reader.GetValue("modified_date")),
                        (bool)reader.GetValue("is_deleted"),
                        (Guid)reader.GetValue("post_owner"),
                        (string)reader.GetValue("avatar"),
                        (string)reader.GetValue("name"),
                        (string)reader.GetValue("surname")
                     ));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN PostRepository.cs (GetPosts) >>> {error.Message}");
            }
            return posts;
        }
        public bool AddPost(PostDB post)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    "insert into posts " +
                    "values (@id, @user_id, @post_text, @creation_date, @modified_date)";
                using NpgsqlCommand addPost = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", post.Id),
                        new NpgsqlParameter("@user_id", post.UserId),
                        new NpgsqlParameter("@post_text", post.Text),
                        new NpgsqlParameter("@creation_date", post.CreationDate),
                        new NpgsqlParameter("@modified_date", post.ModifiedDate == null ? DBNull.Value : post.ModifiedDate),
                    }
                };
                int rows = addPost.ExecuteNonQuery();
                return rows == 1 ? true : false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN PostRepository.cs (AddPost) >>> {error.Message}");
            }
            return false;
        }
        public PostDB getPostById(Guid id)
        {

            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    "select posts.*, name, surname, avatar " +
                    "from posts join users " +
                    "on users.id = posts.user_id " +
                    $"where posts.id = @id";
                using NpgsqlCommand addPost = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                    }
                };
                using NpgsqlDataReader reader = addPost.ExecuteReader();
                if (reader.Read())
                {
                    return new PostDB(
                         (Guid)reader.GetValue("id"),
                         (Guid)reader.GetValue("user_id"),
                         (string)reader.GetValue("post_text"),
                         (DateTime)reader.GetValue("creation_date"),
                         Utils.ConvertFromDBVal<DateTime>(reader.GetValue("modified_date")),
                         (bool)reader.GetValue("is_deleted"),
                         (Guid)reader.GetValue("post_owner"),
                         (string)reader.GetValue("avatar"),
                         (string)reader.GetValue("name"),
                         (string)reader.GetValue("surname")
                     );
                }

            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN PostRepository.cs (getPostById) >>> {error.Message}");
            }
            return null;
        }
        public Guid? deletePost(Guid id)
        {
            try
            {
                using NpgsqlConnection connection = ConnectionKeys.ConfigureDbConnection();
                connection.Open();
                string query =
                    "update posts " +
                    "set is_deleted = true " +
                    "where id = @id";
                using NpgsqlCommand addPost = new NpgsqlCommand(query, connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("@id", id),
                    }
                };
                int rows = addPost.ExecuteNonQuery();
                return rows == 1 ? id : null;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR IN PostRepository.cs (deletePost) >>> {error}");
            }
            return null;
        }
    }
}

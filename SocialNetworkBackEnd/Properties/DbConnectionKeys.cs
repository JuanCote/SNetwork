using Npgsql;

namespace SocialNetworkBackEnd.Properties
{
    public class DbConnectionKeys
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string DbName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public DbConnectionKeys()
        {
            Host = "localhost";
            User = "postgres";
            DbName = "SocialNetwork";
            Password = "123";
        }
        public NpgsqlConnection ConfigureDbConnection()
        {
            return new NpgsqlConnection(
                $"Server={Host};" +
                $"Username={User};" +
                $"Database={DbName};" +
                $"Password={Password};");
        }
    }
}
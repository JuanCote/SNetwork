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
            Host = "ec2-3-248-121-12.eu-west-1.compute.amazonaws.com";
            User = "qynzfqrqxauonc";
            DbName = "d1h1m0vvlodabd";
            Password = "b3c57f808175d3f0d895eddd39688391e9ccdc62aca6c6fa69170cd10cb2d95d";
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
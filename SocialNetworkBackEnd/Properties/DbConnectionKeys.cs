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
            Host = "ec2-54-155-110-181.eu-west-1.compute.amazonaws.com";
            User = "hnllzdssfvcuce";
            DbName = "d3esmgdf72no9u";
            Password = "72b65e2ea938462cc406731a6871033e2c6cb681e62d7962c0c0f14867868d74";
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
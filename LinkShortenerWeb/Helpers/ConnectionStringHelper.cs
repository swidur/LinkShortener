using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortenerWeb.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionsStringFromEnvironment(string envVarName = "DATABASE_URL")
        {
            var databaseUrl = Environment.GetEnvironmentVariable(envVarName);
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }
    }
}

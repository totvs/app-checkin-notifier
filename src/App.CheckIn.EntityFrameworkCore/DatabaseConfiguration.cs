using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace App.CheckIn.EntityFrameworkCore
{
    /// <summary>
    /// Type responsible for building a ConnectionString
    /// </summary>
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; }
        public IConfiguration Configuration { get; }

        public DatabaseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;

            var host = configuration["DB_HOST"];

            var exceptions = new List<NotSupportedException>();

            if (string.IsNullOrWhiteSpace(host))
                exceptions.Add(new NotSupportedException($"Invalid database host configuration. Please set \"DB_HOST\" environment variable"));

            var database = configuration["DB_DATABASE"];

            if (string.IsNullOrWhiteSpace(database))
                exceptions.Add(new NotSupportedException($"Invalid database name configuration. Please set \"DB_DATABASE\" environment variable"));

            var username = configuration["DB_USERNAME"];

            if (string.IsNullOrWhiteSpace(username))
                exceptions.Add(new NotSupportedException($"Invalid database username configuration. Please set \"DB_USERNAME\" environment variable"));

            var password = configuration["DB_PASSWORD"];

            if (string.IsNullOrWhiteSpace(password))
                exceptions.Add(new NotSupportedException($"Invalid database password configuration. Please set \"DB_PASSWORD\" environment variable"));

            var port = configuration["DB_PORT"];

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            var connectionStringBuilder = new DbConnectionStringBuilder
            {
                ["Server"] = host,
                ["Port"] = port,
                ["Database"] = database,
                ["User Id"] = username,
                ["Password"] = password
            };

            ConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}

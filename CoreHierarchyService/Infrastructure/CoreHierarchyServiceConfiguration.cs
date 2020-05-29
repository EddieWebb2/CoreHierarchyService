using System;
using System.Data;
using System.Data.SqlClient;
using Serilog.Events;

namespace CoreHierarchyService.Infrastructure
{
    public class CoreHierarchyServiceConfiguration : ICoreHierarchyServiceConfiguration
    {
        public string DBConnection { get; set; }
        
        public Uri LoggingEndpoint { get; set; }
        public bool EnableElasticLogging { get; set; }
        public LogEventLevel ElasticLoggingLevel { get; set; }
        public bool EnableConsoleLogging { get; set; }
        public bool WriteToTempPath { get; set; }
        public bool EnableSelfLog { get; set; }
        public string SoftwareName { get; set; }
        

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(DBConnection);
            connection.Open();

            return connection;
        }

    }
}

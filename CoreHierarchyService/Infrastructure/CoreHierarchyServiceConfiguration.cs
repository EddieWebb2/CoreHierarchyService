using System;
using Serilog.Events;

namespace CoreHierarchyService.Infrastructure
{
    public class CoreHierarchyServiceConfiguration : ICoreHierarchyServiceConfiguration
    {
        public Uri LoggingEndpoint { get; set; }
        public bool EnableElasticLogging { get; set; }
        public LogEventLevel ElasticLoggingLevel { get; set; }
        public bool EnableConsoleLogging { get; set; }
        public bool WriteToTempPath { get; set; }
        public bool EnableSelfLog { get; set; }
        public string SoftwareName { get; set; }
        

        public CoreHierarchyServiceConfiguration()
        {
            // Add my ORM initialise... EF6 or Dapper either or
        }
    }
}

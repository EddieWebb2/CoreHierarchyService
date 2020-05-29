using System.Data;
using SerilogHelpers.Infrustructure;

namespace CoreHierarchyService.Infrastructure
{
    public interface ICoreHierarchyServiceConfiguration: ILoggingConfiguration
    {
        // TODO - Build my config out
        public string DBConnection { get; set; }

        
        IDbConnection OpenConnection();
    }
}

using System.Collections.Generic;
using System.Data;
using System.Linq;
using CoreHierarchyService.Infrastructure;
using CoreHierarchyService.Models;
using Dapper;

namespace CoreHierarchyService.Cqrs.Queries
{
    public class GetAllUsers
    {
        private ICoreHierarchyServiceConfiguration _config;

        public GetAllUsers(ICoreHierarchyServiceConfiguration config) => _config = config;

        public IEnumerable<User> GetUserList()
        {
            string sql = "select " +
                         "  Title, " +
                         "  EmployeeFirstNames, " +
                         "  EmployeeSurname " +
                         "from GAT_MASTERDATA.org.Employee as e" +
                         " left join GAT_MASTERDATA.Org.EmployeeTenure as et on e.EmployeeId = et.Employee ";
            //+
            //             "where EmployeeId = 21";


            using (var connection = _config.OpenConnection())
            {
                return connection
                    .Query<User>(sql, commandType: CommandType.Text).ToList();
            }
        }
    }
}

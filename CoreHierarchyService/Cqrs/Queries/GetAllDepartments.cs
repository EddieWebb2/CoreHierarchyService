using System.Collections.Generic;
using System.Data;
using System.Linq;
using CoreHierarchyService.Infrastructure;
using CoreHierarchyService.Models;
using CoreHierarchyService.Types;
using Dapper;

namespace CoreHierarchyService.Cqrs.Queries
{
    public class GetAllDepartments
    {
        private ICoreHierarchyServiceConfiguration _config;

        public GetAllDepartments(ICoreHierarchyServiceConfiguration config) => _config = config;

        public IEnumerable<Department> GetDepartments()
        {
            string sql = "select " +
                         "  oul.ParentOrganisationalUnit as 'ParentId',	" +
                         "  ou2.OrganisationalUnitName as 'ParentName',	" +
                         "  oul.OrganisationalUnit as 'ChildId', " +
                         "  ou1.OrganisationalUnitName as 'ChildName' " +
                         "from GAT_MASTERDATA.Org.OrganisationalUnitLink as oul " +
                         "left Join GAT_MASTERDATA.Org.OrganisationalUnit as ou1 on oul.OrganisationalUnit = ou1.OrganisationalUnitId	" +
                         "left Join GAT_MASTERDATA.Org.OrganisationalUnit as ou2 on oul.ParentOrganisationalUnit = ou2.OrganisationalUnitId " +
                         "where	ou1.OrganisationalUnitEndDate is null " +
                         "and ou2.OrganisationalUnitEndDate is null " +
                         "and ou1.OrganisationalUnitMasterSource = 2 " +
                         //"and ou2.OrganisationalUnitMasterSource = 2 " +
                         "order by oul.ParentOrganisationalUnit";

            using (var connection = _config.OpenConnection())
            {
                return connection
                    .Query<Department>(sql, commandType: CommandType.Text).ToList();
            }
        }


        public IEnumerable<DepartmentHierarchy> GetDepartmentHierarchy(int master)
        {
            string sql = "select " +
                         "  IDENTITY(int, 1, 1) AS 'RowID', " +
                         "  oul.ParentOrganisationalUnit as 'ParentId',	" +
                         "  ou2.OrganisationalUnitName as 'ParentName'," +
                         "	oul.OrganisationalUnit as 'ChildId'," +
                         "  ou1.OrganisationalUnitName as 'ChildName' " +
                         "into #temp1 " +
                         "from GAT_MASTERDATA.Org.OrganisationalUnitLink as oul " +
                         "left Join GAT_MASTERDATA.Org.OrganisationalUnit as ou1 on oul.OrganisationalUnit = ou1.OrganisationalUnitId	" +
                         "left Join GAT_MASTERDATA.Org.OrganisationalUnit as ou2 on oul.ParentOrganisationalUnit = ou2.OrganisationalUnitId " +
                         "where	ou1.OrganisationalUnitEndDate is null " +
                         "and ou2.OrganisationalUnitEndDate is null " +
                         $"and ou1.OrganisationalUnitMasterSource = {master} " +
                         "order by oul.ParentOrganisationalUnit " +
                         "" +
                         ";with FullPathHierarchy as (" +
                         "  select ChildId, ParentId, ChildName, 1 as 'Level', CAST(ChildName as varchar(max)) as 'Path' " +
                         "from #temp1	" +
                         "where ParentId is null " +
                         "" +
                         "union all " +
                         "" +
                         "select t.ChildId, t.ParentId, t.ChildName, fph.[Level] + 1, CAST(fph.Path + '/' + t.ChildName AS VARCHAR(MAX)) " +
                         "from FullPathHierarchy as fph " +
                         "inner join #temp1 as t on t.ParentId = fph.ChildId " +
                         "where t.ParentId <> t.ChildId) " +
                         "" +
                         "select * from FullPathHierarchy";

            using (var connection = _config.OpenConnection())
            {
                return connection
                    .Query<DepartmentHierarchy>(sql, commandType: CommandType.Text).ToList();
            }
        }


    }
}
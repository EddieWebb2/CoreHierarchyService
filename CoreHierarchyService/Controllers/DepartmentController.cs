using System.Collections.Generic;
using CoreHierarchyService.Cqrs.Queries;
using CoreHierarchyService.Infrastructure;
using CoreHierarchyService.Models;
using CoreHierarchyService.Types;
using Microsoft.AspNetCore.Mvc;

namespace CoreHierarchyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ICoreHierarchyServiceConfiguration _config;

        public DepartmentController(ICoreHierarchyServiceConfiguration config)
        {
            _config = config;
        }
        
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            var result = new GetAllDepartments(_config);

            return result.GetDepartments();
        }

        [HttpGet]
        [Route("hierarchy")]
        public IEnumerable<DepartmentHierarchy> GetHierarchy(int master)
        {
            var result = new GetAllDepartments(_config);

            return result.GetDepartmentHierarchy(master);
        }


    }
}
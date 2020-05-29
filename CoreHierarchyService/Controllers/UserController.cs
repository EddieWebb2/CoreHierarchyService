using System.Collections.Generic;
using CoreHierarchyService.Cqrs.Queries;
using CoreHierarchyService.Infrastructure;
using CoreHierarchyService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace CoreHierarchyService.Controllers
{
    // TODO - Build my controller set with CQRS model

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ICoreHierarchyServiceConfiguration _config;

        //// GET: api/User
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public UserController(ICoreHierarchyServiceConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public IEnumerable<User> Get()
        {
            var result = new GetAllUsers(_config);
            
            
            return result.GetUserList();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

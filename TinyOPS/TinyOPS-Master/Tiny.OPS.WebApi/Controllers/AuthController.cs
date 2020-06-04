using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// jwt权限验证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
    }
}

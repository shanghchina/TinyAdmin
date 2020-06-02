using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiny.OPS.Common;
using Tiny.OPS.WebApi.Filter;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// Values的Demo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Description("Values的Demo")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [NoCheck]
        public ActionResult<IEnumerable<string>> Get()
        {
            _Log4Net.Info("123");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Exodus3.Api.Controllers
{
    [Route("api/[controller]")]
    public class SermonsController : Controller
    {
        // GET: /<controller>/
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}

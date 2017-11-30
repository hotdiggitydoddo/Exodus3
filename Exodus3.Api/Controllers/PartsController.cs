using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Exodus3.Api.Controllers
{
    [Route("api/series/{seriesId}/parts")]
    public class PartsController : Controller
    {
        public PartsController()
        {
        }

        public async Task<IActionResult> Get(int seriesId)
        {
            return Json($"All parts for series {seriesId}!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int seriesId, int id)
        {
            return Json($"Series id: {seriesId}, sermonId {id}");
        }
    }
}

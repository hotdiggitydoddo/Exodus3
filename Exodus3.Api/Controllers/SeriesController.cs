using System;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Api.Data;
using Exodus3.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Exodus3.Api.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly IRepository<Series> _series;

        public SeriesController(IRepository<Series> series)
        {
            _series = series;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var allSeries = _series.Find(x => !x.IsDeleted, x => x.Sermons).Result.ToList();

            return Json(allSeries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var series = await _series.GetById(id);
            return Json(series);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Series series)
        {
            var newSermon = new Series
            {
                Description = series.Description,
                Name = series.Name
            };

            var res = await _series.Add(series);
            return Json(res);
        }
    }
}

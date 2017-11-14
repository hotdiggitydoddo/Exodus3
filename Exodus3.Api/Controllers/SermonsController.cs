using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Api.Data;
using Exodus3.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Exodus3.Api.Controllers
{
    [Route("api/[controller]")]
    public class SermonsController : Controller
    {
        private readonly IRepository<Sermon> _sermons;

        public SermonsController(IRepository<Sermon> sermons)
        {
            _sermons = sermons;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sermons = await _sermons.Find(x => !x.IsDeleted, x => x.Series);

            foreach(var s in sermons)
            {
                s.Series = new Series { Id = s.Series.Id, Name = s.Series.Name, Description = s.Series.Description };
            }

            return Json(sermons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sermon = await _sermons.GetById(id, x => x.Series);


            return Json(new { sermon.Id, sermon.Name, sermon.Summary, sermon.AudioSrcUrl, series = new { sermon.Series.Id, sermon.Series.Name, sermon.Series.Description } });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sermon sermon)
        {
            var newSermon = new Sermon
            {
                Summary = sermon.Summary,
                Name = sermon.Name,
                SeriesId = sermon.SeriesId,
                AudioSrcUrl = sermon.AudioSrcUrl,
                Date = sermon.Date
            };

            var res = await _sermons.Add(sermon);
            return Json(res);
        }
    }
}

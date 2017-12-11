using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Api.Data;
using Exodus3.Api.Data.Entities;
using Exodus3.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exodus3.Api.Controllers
{
    [Route("api/sermons")]
    public class SermonsController : Controller
    {
        private readonly IRepository<Sermon> _sermons;
        private readonly IRepository<Season> _Season;

        public SermonsController(IRepository<Sermon> sermons, IRepository<Season> Season)
        {
            _sermons = sermons;
            _Season = Season;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sermons = await _sermons.Get(x => x.Season);

            foreach(var s in sermons)
            {
              //  s.Season = new Season { Id = s.Season.Id, Name = s.Season.Name, Description = s.Season.Description };
            }

            return Json(sermons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sermon = await _sermons.GetById(id, x => x.Season);


            return Json(new { sermon.Id, sermon.Name, sermon.Summary, sermon.AudioSrcUrl, Season = new { sermon.Season.Number, sermon.Season.Series } });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewSermonDto sermon)
        {
            var newSermon = new Sermon
            {
                Summary = sermon.Summary,
                Name = sermon.Name,
                AudioSrcUrl = sermon.AudioSrcUrl,
                Date = sermon.Date,
                SeasonId = sermon.SeasonId
            };

            var res = await _sermons.Add(newSermon);
            return Json(res);
        }

        private async Task<bool> UpdateSeasonUpdatedOn(Guid SeasonId)
        {
            var Season = await _Season.GetById(SeasonId);
            return true;
           // return await _Season.Update(Season);
        }


    }
}

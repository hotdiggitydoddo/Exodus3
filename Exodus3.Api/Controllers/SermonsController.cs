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
        private readonly IRepository<Series> _series;

        public SermonsController(IRepository<Sermon> sermons, IRepository<Series> series)
        {
            _sermons = sermons;
            _series = series;
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
        public async Task<IActionResult> Get(Guid id)
        {
            var sermon = await _sermons.GetById(id, x => x.Series);


            return Json(new { sermon.Id, sermon.Name, sermon.Summary, sermon.AudioSrcUrl, series = new { sermon.Series.Id, sermon.Series.Name, sermon.Series.Description } });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewSermonDto sermon)
        {
            
            var newSermon = new Sermon
            {
                Summary = sermon.Summary,
                Name = sermon.Name,
                SeriesId = sermon.SeriesId,
                AudioSrcUrl = sermon.AudioSrcUrl,
                Date = sermon.Date
            };

            var res = await _sermons.Add(newSermon);
            UpdateSeriesUpdatedOn(sermon.SeriesId);
            return Json(res);
        }

        private async Task<bool> UpdateSeriesUpdatedOn(Guid seriesId)
        {
            var series = await _series.GetById(seriesId);
            return true;
           // return await _series.Update(series);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Api.Data;
using Exodus3.Api.Models;
using Exodus3.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Exodus3.Api.Services;
using Microsoft.Extensions.Logging;

namespace Exodus3.Api.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly ISeriesService _seriesService;
        private readonly ILogger<SeriesController> _logger;

        public SeriesController(ISeriesService seriesService, ILogger<SeriesController> logger)
        {
            _seriesService = seriesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var allSeries = await _seriesService.GetAllSeries();

                return Ok(allSeries.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all series: {ex}.");
                return BadRequest("Failed to get all series");
            }

        }

      

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var series = await _seriesService.GetSeriesById(id);

            if (series == null)
                return NotFound();

            return Json(series);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewSeriesDto seriesModel)
        {
            var newSeries = await _seriesService.CreateNewSeries(seriesModel);

            return CreatedAtRoute("GetById", new { id = newSeries.Id }, newSeries);
        }

        //[HttpPost("{id}/parts")]
        //public async Task<IActionResult> CreateParts(Guid id, [FromBody] List<Guid> sermonIds)
        //{
        //    var series = await _series.GetById(id, x => x.Sermons);
        //    var allSeriesParts = await _seriesParts.Find(x => x.SeriesId == series.Id);

        //    var newPart = new SeriesPart
        //    {
        //        Number = !allSeriesParts.Any() ? 1 : allSeriesParts.Count() + 1,
        //        SeriesId = series.Id,
        //        //SermonIds = sermonIds.Distinct().Where(x => series.Sermons.Select(o => o.Id).Contains(x)).ToArray()
        //    };

        //    var result = await _seriesParts.Add(newPart);

        //    return Json(result);
        //}

        //private SeriesDto CreateSeriesVm(Series series, IEnumerable<Season> seasons)
        //{
        //    var seriesModel = new SeriesDto
        //    {
        //        Id = series.Id,
        //        Name = series.Name,
        //        Description = series.Description
        //    };

        //    if (!seasons.Any())
        //    {
        //        seriesModel.Parts.Add(new SeriesPartDto
        //        {
        //            Number = 1,
        //            Sermons = series.Sermons.Select(x => new SermonDto
        //            {
        //                Id = x.Id,
        //                Name = x.Name,
        //                Summary = x.Summary,
        //                AudioSrcUrl = x.AudioSrcUrl,
        //                Date = x.Date
        //            }).ToList()
        //        });
        //    }
        //    else
        //    {
        //        foreach (var part in seasons)
        //        {
        //            var sermons = series.Sermons.Where(x => part.Sermons.Contains(x));

        //            seriesModel.Parts.Add(new SeriesPartDto
        //            {
        //                Number = part.Number,
        //                Sermons = sermons.Select(x => new SermonDto
        //                {
        //                    Id = x.Id,
        //                    Name = x.Name,
        //                    Summary = x.Summary,
        //                    AudioSrcUrl = x.AudioSrcUrl,
        //                    Date = x.Date
        //                }).ToList()
        //            });
        //        }
        //    }

        //    return seriesModel;
        //}
    }
}

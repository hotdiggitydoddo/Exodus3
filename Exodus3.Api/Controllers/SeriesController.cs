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
    [Route("api/series")]
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
            var allSeries = await _seriesService.GetAllSeries();
            return Ok(allSeries.ToList());
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateSeriesDto model)
        {
            var res = await _seriesService.UpdateSeries(id, model);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool hard = false)
        {
            await _seriesService.DeleteSeries(id, hard);
            return NoContent();
        }
    }
}

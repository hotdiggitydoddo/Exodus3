using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Api.Data;
using Exodus3.Api.Data.Entities;
using Exodus3.Api.Models;
using Microsoft.Extensions.Logging;

namespace Exodus3.Api.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IRepository<Series> _series;
        private readonly ILogger<SeriesService> _logger;

        public SeriesService(IRepository<Series> series, ILogger<SeriesService> logger)
        {
            _series = series;
            _logger = logger;
        }

        public async Task<SeriesDto> GetSeriesById(Guid id)
        {
            var series = await _series.GetById(id, x => x.Seasons);

            //if (series == null)
            //{
            //    _logger.LogError(500, $"Can't find series with id {id}.");
            //    return null;
            //}

            return ToDto(series);

        }

        public async Task<IEnumerable<SeriesDto>> GetAllSeries()
        {
            var all = await _series.Get(x => x.Seasons);
            var seriesList = all.Select(x => ToDto(x));

            return seriesList;
        }

        public async Task<SeriesDto> CreateNewSeries(NewSeriesDto dto)
        {
            var newSeries = new Series
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _series.Add(newSeries);

            return ToDto(newSeries);
        }

        private SeriesDto ToDto(Series series)
        {
            var dto = new SeriesDto
            {
                Id = series.Id,
                Name = series.Name,
                Description = series.Description,
                Seasons = series.Seasons.Select(x => new SeasonDto
                {
                    Number = x.Number,
                    Sermons = x.Sermons.Select(o => new SermonDto
                    {
                        Id = o.Id
                    }).ToList()
                }).ToList()
            };

            return dto;
        }
    }
}

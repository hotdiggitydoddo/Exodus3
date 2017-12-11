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
        private readonly IRepository<Sermon> _sermons;
        private readonly IRepository<Season> _seasons;
        private readonly ILogger<SeriesService> _logger;

        public SeriesService(IRepository<Series> series, IRepository<Sermon> sermons,
            IRepository<Season> seasons, ILogger<SeriesService> logger)
        {
            _series = series;
            _sermons = sermons;
            _seasons = seasons;
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
                Description = dto.Description,
            };
            newSeries.Seasons.Add(new Season { Number = 1 });
            await _series.Add(newSeries);

            return ToDto(newSeries);
        }

        public async Task<SeriesDto> UpdateSeries(Guid id, UpdateSeriesDto dto)
        {
            var series = await _series.GetById(id);
            if (!string.IsNullOrWhiteSpace(dto.Name))
                series.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description))
                series.Description = dto.Description;
            var updated = await _series.Update(series);
            
            return ToDto(series);
        }

        public async Task DeleteSeries(Guid id, bool hard = false)
        {
            var series = await _series.GetById(id, x => x.Seasons);

            foreach (var season in series.Seasons)
            {
                season.Sermons.Clear();
                foreach (var sermon in season.Sermons)
                {
                    sermon.Season = null;
                    await _sermons.Update(sermon);
                }
                await _seasons.Delete(season);
            }
            await _series.Delete(series);
        }

        public async Task<SeriesDto> AddSermon(NewSermonDto dto)
        {
            var newSeries = await _sermons.Add(new Sermon
            {
                SeasonId = dto.SeasonId,
                Name = dto.Name,
                Summary = dto.Summary,
                Date = dto.Date
            });
            
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
                    Id = x.Id,
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

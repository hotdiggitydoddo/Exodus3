using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exodus3.Api.Models;

namespace Exodus3.Api.Services
{
    public interface ISeriesService
    {
        Task<IEnumerable<SeriesDto>> GetAllSeries();
        Task<SeriesDto> GetSeriesById(Guid id);
        Task<SeriesDto> CreateNewSeries(NewSeriesDto dto);
    }
}

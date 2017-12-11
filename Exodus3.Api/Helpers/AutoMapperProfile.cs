using System;
using AutoMapper;
using Exodus3.Api.Data.Entities;
using Exodus3.Api.Models;

namespace Exodus3.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Sermon, SermonDto>()
                .ReverseMap();
            CreateMap<NewSermonDto, SermonDto>();
        }
    }
}

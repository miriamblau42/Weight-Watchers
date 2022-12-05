using AutoMapper;
using Measure.Services.Models;
using Measure.WebAPI.DTO;

namespace Measure.WebAPI
{
    public class AutoMapperMeasure:Profile
    {
        public AutoMapperMeasure()
        {
            CreateMap<MeasurePostDTO, MeasureModel>();
        }
    }
}

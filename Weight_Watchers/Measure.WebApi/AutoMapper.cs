using AutoMapper;
using Measure.Services.Models;
using Measure.WebApi.DTO;

namespace Measure.WebApi;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Data.Entities.Measure, MeasureModel>().ReverseMap();
        CreateMap<MeasureModel, PostMeasureDTO>().ReverseMap();
    }

}

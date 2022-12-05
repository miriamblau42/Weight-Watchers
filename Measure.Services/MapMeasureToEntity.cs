using AutoMapper;
using Measure.Services.Models;


namespace Measure.Services
{
    public class MapMeasureToEntity :Profile
    {
        public MapMeasureToEntity()
        {
            CreateMap<MeasureModel, Measure.Data.Entities.Measure>().ReverseMap();
        }
    }
}

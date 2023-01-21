using AutoMapper;
using Measure.Data;
using Measure.Data.Entities;
using Measure.Services.Models;

namespace Measure.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IMeasureData _measureData;
        private readonly IMapper _mapper;
        public MeasureService(IMeasureData measureData, IMapper mapper)
        {
            _measureData = measureData;
            _mapper = mapper;
        }
        public Task<int> AddNewMeasure(MeasureModel newMeasure)
        {
            var dbMeasure = _mapper.Map<Data.Entities.Measure>(newMeasure);
            dbMeasure.Status = EStatus.InProcess;
            dbMeasure.Date = DateTime.Now;
            return  _measureData.AddNewMeasure(dbMeasure);
        }

        public void UpdateMeasureStatus(int measureId , bool succeeded)
        {
            EStatus status = succeeded switch
            {
                true => EStatus.Success,
                false => EStatus.Failed,
            };
            _measureData.UpdateMeasureStatus(measureId, status);
        }
    }
}

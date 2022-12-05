using AutoMapper;
using Measure.Data.Interfaces;
using Measure.Services.Interfaces;
using Measure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Services.Services
{
    public class MeasureService : IMeasureService
    {
        private IMeasureDal _measureDal;
        private IMapper _mapper;

        public MeasureService(IMeasureDal measureDal)
        {
            _measureDal = measureDal;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapMeasureToEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<int> AddMeasure(MeasureModel newMeasure)
        {
            newMeasure.Status = Status.InProgress;
            newMeasure.Date = DateTime.UtcNow;
            Measure.Data.Entities.Measure measure = _mapper.Map<Measure.Data.Entities.Measure>(newMeasure);
            int id = await _measureDal.AddMeasure(measure);
            return id;
        }
        public Task UpdateStatus(int ID, Status status)
        {
            Data.Entities.Status cnvrtStatus;
            cnvrtStatus = ((Data.Entities.Status)(int)status);
            return _measureDal.UpdateStatus(ID,cnvrtStatus);
        }
    }
}

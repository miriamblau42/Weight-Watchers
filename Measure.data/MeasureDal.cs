using Measure.Data.Entities;
using Measure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Data
{
    public class MeasureDal : IMeasureDal
    {
        private IDbContextFactory<MeasureDBContext> _measureDBContextFactory;

        public MeasureDal(IDbContextFactory<MeasureDBContext> measureDBContextFactory)
        {
            _measureDBContextFactory = measureDBContextFactory;
        }
        public async Task<int> AddMeasure(Entities.Measure measure)
        {
          
            try
            {
                using var _measureDBContext = _measureDBContextFactory.CreateDbContext();
                await _measureDBContext.Measures.AddAsync(measure); 
                await _measureDBContext.SaveChangesAsync();
                return measure.ID; 
            }
            catch
            {
                return -1;
            };
        }
        public async Task UpdateStatus(int ID, Status status)//try & catch?
        {
            using var context = _measureDBContextFactory.CreateDbContext();
            var measure = await context.Measures.FirstAsync(measure => measure.ID == ID);
            measure.Status = status;
            context.Measures.Update(measure);
            await context.SaveChangesAsync();
        }


    }
}

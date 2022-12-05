using Measure.Data;
using Measure.Data.Entities;
using Measure.Data.Interfaces;
using Measure.Messages;
using Measure.Services.Interfaces;
using Measure.Services.Models;
using Measure.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;

namespace Measure.NSB.handlers
{
    public class UpdateStatusHandler:IHandleMessages<UpdateStatus>
    {
        static ILog log = LogManager.GetLogger<UpdateStatusHandler>();
        private ServiceProvider _servicesFactory;

        public UpdateStatusHandler()
        {
            var databaseConnection = "Data Source=localhost\\sqlexpress;Initial Catalog=Measure;Integrated Security=True";
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IMeasureService, MeasureService>();
            serviceCollection.AddScoped<IMeasureDal, MeasureDal>();
            serviceCollection.AddDbContextFactory<MeasureDBContext>(opt => opt.UseSqlServer());
            _servicesFactory = serviceCollection.BuildServiceProvider();
        }
        public Task Handle(UpdateStatus message, IMessageHandlerContext context)
        {
           var measureService= _servicesFactory.GetService<IMeasureService>();
            log.Info("UpdateStatus message received.");
            Services.Models.Status status;
            if (message.Success)
                status = Services.Models.Status.Succsesed;
            else
                status = Services.Models.Status.Failed;
            measureService.UpdateStatus(message.MeasureID, status);
            return Task.CompletedTask;
        }
    }
}

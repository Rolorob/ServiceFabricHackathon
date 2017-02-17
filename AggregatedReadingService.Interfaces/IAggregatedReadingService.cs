using Common.Model;
using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AggregatedReadingService.Interfaces
{
    internal interface IAggregatedReadingService : IService
    {
        Task AddAggregatedReading(AggregatedReading aggregatedReading);

        Task<IEnumerable<AggregatedReading>> GetAggregatedReadings();
    }
}
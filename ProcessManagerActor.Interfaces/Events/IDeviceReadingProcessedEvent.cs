using Common.Model;
using Microsoft.ServiceFabric.Actors;

namespace ProcessManagerActor.Interfaces.Events
{
    public interface IDeviceReadingProcessedEvent : IActorEvents
    {
        void DeviceReadingProcessed(ReadingResult readingResult);
    }
}
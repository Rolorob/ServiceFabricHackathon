using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Common.Events;
using ProcessManagerActor.Interfaces.Events;

namespace ProcessManagerActor.Interfaces
{
    public interface IProcessManagerActor : IActor, IActorEventPublisher<IDeviceReadingProcessedEvent>
    {
        Task ProcessDeviceReadEventAsync(DeviceRead deviceReadEVent);
    }
}

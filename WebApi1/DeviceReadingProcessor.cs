using System;
using Common.Model;
using ProcessManagerActor.Interfaces.Events;
using Microsoft.AspNet.SignalR;

namespace UISocketStatelessService
{
    public class DeviceReadingProcessor : IDeviceReadingProcessedEvent
    {
        public void DeviceReadingProcessed(ReadingResult readingResult)
        {
            var ctx = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            ctx.Clients.All.SendData(readingResult);
            return;
        }
    }
}
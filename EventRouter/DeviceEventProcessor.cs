using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRouter
{
    public class DeviceEventProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            return Task.FromResult(true);
        }

        public Task OpenAsync(PartitionContext context)
        {
            return Task.FromResult(true);
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            // TODO: set checkpoint when message is processed correctly
            // await context.CheckpointAsync();



            await Task.FromResult(true);
        }
    }
}

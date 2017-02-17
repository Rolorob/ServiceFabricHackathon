using Common.Events;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json;
using ProcessManagerActor.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRouter
{
    public class DeviceEventProcessor : IEventProcessor
    {
        private static Type DeviceReadEventType = typeof(DeviceRead);
        private readonly JsonSerializer _serializer;


        public DeviceEventProcessor()
        {
            _serializer = new JsonSerializer();
        }

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
            var deviceReadEvents = 
                messages
                .Where(m => IsDeviceRead(m))
                .Select(m => ParseDeviceEvent(m))
                .ToList();

            var processTasks = deviceReadEvents
                .Select(dr => 
                    new
                    {
                        Actor = ActorProxy.Create<IProcessManagerActor>(CreateActorId(dr), "ServiceFabricHackathon", "ProcessManagerActorService"),
                        DeviceRead = dr
                    })
                .Select(x => x.Actor.ProcessDeviceReadEventAsync(x.DeviceRead));

            await Task.WhenAll(processTasks);

            // TODO: set checkpoint when message is processed correctly
            // await context.CheckpointAsync();
        }

        private ActorId CreateActorId(DeviceRead dr)
        {
            // ChannelId is in format: profile://heating/export/etc im not sure an actor id will accept :// and /, therefore we replace them with an _
            var flatChannel = dr.Reading.ChannelId
                .ToString()
                .Replace("://", "_")
                .Replace("/", "_");

            return new ActorId($"{dr.DeviceId}_{flatChannel}");
        }

        private bool IsDeviceRead(EventData message)
        {
            if (!(message?.Properties?.ContainsKey("eventName") ?? false))
            {
                return false;
            }

            var eventName = message.Properties["eventName"].ToString();

            return eventName.ToLowerInvariant() == "deviceread";
        }

        public DeviceRead ParseDeviceEvent(EventData message)
        {
            using (var stream = message.GetBodyStream())
            {
                if (stream.Length == 0)
                {
                    return null;
                }


                using (var streamReader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return _serializer.Deserialize(jsonReader, DeviceReadEventType) as DeviceRead;
                }
            }
        }
    }
}

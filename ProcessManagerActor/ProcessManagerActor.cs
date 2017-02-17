using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using ProcessManagerActor.Interfaces;
using Common.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace ProcessManagerActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class ProcessManagerActor : Actor, IProcessManagerActor, IRemindable
    {
        private const string StartProcessingReminder = "StartProcessingReminder";
        private const string DEVICE_READS_QUEUE = "DeviceReadsQueue";

        /// <summary>
        /// Initializes a new instance of ProcessManagerActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ProcessManagerActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync(DEVICE_READS_QUEUE, new List<DeviceRead>());
        }

        public async Task ProcessDeviceReadEventAsync(DeviceRead deviceReadEvent)
        {
            var deviceReadQueue = await this.StateManager.GetStateAsync<List<DeviceRead>>(DEVICE_READS_QUEUE);
            deviceReadQueue.Add(deviceReadEvent);
            await this.StateManager.SetStateAsync(DEVICE_READS_QUEUE, deviceReadQueue);

            await RegisterReminderAsync(StartProcessingReminder, null, TimeSpan.FromSeconds(-1), TimeSpan.FromSeconds(-1));
        }

        public async Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
        {
            if (reminderName == StartProcessingReminder)
            {
                // Get next DeviceReadEvent 
                var deviceReadQueue = await this.StateManager.GetStateAsync<List<DeviceRead>>(DEVICE_READS_QUEUE);
                var deviceReadEvent = deviceReadQueue.First();

                //TODO: Start processing the DeviceReadEvent

                // Remove processed DeviceReadEvent from queue
                deviceReadQueue.RemoveAt(0);
                await this.StateManager.SetStateAsync(DEVICE_READS_QUEUE, deviceReadQueue);
            }
        }
    }
}
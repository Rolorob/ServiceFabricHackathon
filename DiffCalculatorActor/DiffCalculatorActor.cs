using Common.Model;
using DiffCalculatorActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Threading.Tasks;

namespace DiffCalculatorActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class DiffCalculatorActor : Actor, IDiffCalculatorActor
    {
        /// <summary>
        /// Initializes a new instance of DiffCalculatorActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public DiffCalculatorActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "DiffCalculatorActor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        public async Task<ReadingDiff> CalculateDiffAsync(DeviceReading reading)
        {
            var previousReading = await GetPreviousReadingAsync();
            await PersistPreviousReadingAsync(reading);
            if (previousReading == null)
            {
                return null;
            }

            var diff = reading.Value - previousReading.Value;
            return new ReadingDiff(previousReading.Timestamp, reading.Timestamp, diff);
        }

        private Task PersistPreviousReadingAsync(DeviceReading reading)
        {
            this.StateManager.AddOrUpdateStateAsync<DeviceReading>("PreviousReading", reading, (key, value) => reading);
            return this.StateManager.SaveStateAsync();
        }

        private async Task<DeviceReading> GetPreviousReadingAsync()
        {
            var result = await this.StateManager.TryGetStateAsync<DeviceReading>("PreviousReading");
            return result.HasValue ? result.Value : null;
        }
    }
}

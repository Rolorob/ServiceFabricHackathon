using Common.Model;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using ReadingSplitterActor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingSplitterActor
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
    internal class ReadingSplitterActor : Actor, IReadingSplitterActor
    {
        /// <summary>
        /// Initializes a new instance of ReadingSplitterActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ReadingSplitterActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "ActorReadingSplitterActor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        public Task<IEnumerable<SplitReading>> SplitAsync(ReadingDiff reading)
        {
            var splitReadings = new List<SplitReading>();

            var period = reading.EndTime.Subtract(reading.StartTime);
            var totalMinutes = (int)Math.Ceiling(period.TotalMinutes);
            var readingPerMinute = reading.DiffValue / totalMinutes;

            for (int minute = 0; minute < totalMinutes; minute++)
            {
                splitReadings.Add(new SplitReading(reading.StartTime.AddMinutes(minute), readingPerMinute));
            }

            return Task.FromResult((IEnumerable<SplitReading>)splitReadings);
        }
    }
}

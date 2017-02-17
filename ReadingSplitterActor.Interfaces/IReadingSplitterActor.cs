using Common.Model;
using Microsoft.ServiceFabric.Actors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingSplitterActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IReadingSplitterActor : IActor
    {
        Task<IEnumerable<SplitReading>> SplitAsync(ReadingDiff reading);
    }
}

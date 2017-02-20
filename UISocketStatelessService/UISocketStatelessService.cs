using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ProcessManagerActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using ProcessManagerActor.Interfaces.Events;
using Microsoft.AspNet.SignalR;

namespace UISocketStatelessService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class UISocketStatelessService : StatelessService
    {
        public UISocketStatelessService(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var ctx = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            RequestSubscription("099aee3f-1cff-4c41-9391-aaf45ea55ebe","profile_heat_import_energy_heating");
            await Task.Delay(-1, cancellationToken);
        }

        private void RequestSubscription(string deviceId, string channel)
        {
            // Subscribe to the right actor
            var proxy = ActorProxy.Create<IProcessManagerActor>(new ActorId($"{deviceId}_{channel}"), "ServiceFabricHackathon", "ProcessManagerActorService");
            proxy.SubscribeAsync<IDeviceReadingProcessedEvent>(new DeviceReadingProcessor());
        }
    }
}
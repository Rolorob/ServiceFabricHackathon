using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ProcessManagerActor.Interfaces;
using ProcessManagerActor.Interfaces.Events;
using UISocketStatelessService;

namespace WebApi1
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class WebApi1 : StatelessService
    {
        public WebApi1(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext => new OwinCommunicationListener(Startup.ConfigureApp, serviceContext, ServiceEventSource.Current, "ServiceEndpoint"))
            };
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
            RequestSubscription("5f3ea292-9e71-4999-aab0-a0f6ee337975", "profile_heat_import_energy_heating");
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

using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace EventRouter
{
    public class EventProcessorHostListener : ICommunicationListener
    {
        private EventProcessorHost _eventProcessorHost;

        public void Abort()
        {
            return;
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public async Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            try
            {
                _eventProcessorHost = new EventProcessorHost(
                           "ProjectionWorker-" + Guid.NewGuid(),
                           "fdr-event-hub",
                           "hackathon",
                           "Endpoint=sb://eventhubv3yp56oxvvqee.servicebus.windows.net/;SharedAccessKeyName=Listen;SharedAccessKey=UjUOeCv2xytSTfkK9i6bJ+qNQ5MnF/51owqUI8YTpYE=",
                           "DefaultEndpointsProtocol=https;AccountName=ehsnf6blvne6t7j6;AccountKey=rHrwcOBuJfH9xYCUU27R1V0UtU7D1+P959m+EhjZNIgs8rdh+THc+5VeJZgYLcYZJUs74D5hwAOh+RryX0SGSQ==;")
                {
                    PartitionManagerOptions = new PartitionManagerOptions
                    {
                        AcquireInterval = TimeSpan.FromSeconds(10), // Default is 10 seconds
                        RenewInterval = TimeSpan.FromSeconds(10), // Default is 10 seconds
                        LeaseInterval = TimeSpan.FromSeconds(30) // Default value is 30 seconds
                    }
                };
                var eventProcessorOptions = new EventProcessorOptions
                {
                    InvokeProcessorAfterReceiveTimeout = true,
                    ReceiveTimeOut = TimeSpan.FromSeconds(30)
                };
                eventProcessorOptions.ExceptionReceived += EventProcessorOptions_ExceptionReceived;

                var factory = new DeviceEventProcessorFactory();

                await _eventProcessorHost.RegisterEventProcessorFactoryAsync(factory, eventProcessorOptions);

                return "fdr-event-hub";
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void EventProcessorOptions_ExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e?.Exception == null)
            {
                return;
            }
        }
    }
}

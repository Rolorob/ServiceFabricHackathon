using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISocketStatelessService
{
    public class SignalRHub : Hub
    {
        public void SendData(string data)
        {
            //Clients.All.broadcastMessage(data);
        }

        public void RequestSubscription(string deviceId, string channel)
        {
            // Subscribe to the right actor
        }
    }
}

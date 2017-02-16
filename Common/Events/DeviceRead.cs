using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Model;
using Newtonsoft.Json;

namespace Common.Events
{
    [DataContract]
    public class DeviceRead : DeviceEventBase
    {
        public DeviceRead(
            string deviceId,
            DeviceReading reading,
            string protocolType,
            string connectivityZone,
            DeviceProperties properties)
        {
            DeviceId = deviceId;
            ProtocolType = protocolType;
            ConnectivityZone = connectivityZone;
            Reading = reading;
            Properties = properties;
            Timestamp = DateTime.UtcNow;
        }

        [JsonConstructor]
        private DeviceRead()
        {
        }

        [JsonProperty]
        [DataMember]
        public override string DeviceId { get; protected set; }

        [JsonProperty]
        [DataMember]
        public DeviceReading Reading { get; private set; }

        [JsonProperty]
        [DataMember]
        public string ProtocolType { get; private set; }

        [JsonProperty]
        [DataMember]
        public string ConnectivityZone { get; private set; }

        [JsonProperty]
        [DataMember]
        public override DeviceProperties Properties { get; protected set; }

        [JsonProperty]
        [DataMember]
        public override DateTime Timestamp { get; protected set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Common.Model
{
    [DataContract]
    public class DeviceReading
    {

        public DeviceReading(
            DateTime timestamp,
            decimal value,
            Uri channelId,
            string description,
            string unit,
            IDictionary<string, object> properties)
        {
            Timestamp = timestamp;
            Value = value;
            ChannelId = channelId;
            Description = description;
            Unit = unit;
            Properties = properties;
        }

        [JsonConstructor]
        private DeviceReading()
        {
        }

        [JsonProperty]
        [DataMember]
        public DateTime Timestamp { get; private set; }

        [JsonProperty]
        [DataMember]
        public decimal Value { get; private set; }

        [JsonProperty]
        [DataMember]
        public Uri ChannelId { get; private set; }

        [JsonProperty]
        [DataMember]
        public string Description { get; private set; }

        [JsonProperty]
        [DataMember]
        public string Unit { get; private set; }

        [JsonProperty]
        [DataMember]
        public IDictionary<string, object> Properties { get; private set; }

        public override string ToString()
        {
            var dictionaryString = string.Join(", ", Properties
                .Select(kv => string.Format("{0} = {1}", kv.Key, kv.Value)));

            string baseChannelId = string.Empty;

            return $"[Channel = {ChannelId}{baseChannelId}] {Timestamp}: {Value} {Unit} [Description = {Description}, {dictionaryString}]";
        }
    }
}

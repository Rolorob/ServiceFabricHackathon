using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class AggregatedReading
    {
        public AggregatedReading(DateTime startDate, DateTime endDate, decimal reading)
        {
            StartDate = startDate;
            EndDate = endDate;
            Reading = reading;
        }

        [JsonConstructor]
        private AggregatedReading()
        {
        }

        [JsonProperty]
        [DataMember]
        public Uri ChannelId { get; private set; }

        [JsonProperty]
        [DataMember]
        public DateTime StartDate { get; private set; }

        [JsonProperty]
        [DataMember]
        public DateTime EndDate { get; private set; }

        [JsonProperty]
        [DataMember]
        public decimal Reading { get; private set; }
    }
}
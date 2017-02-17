using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class SplitReading
    {
        public SplitReading(DateTime minute, decimal reading)
        {
            // Dit rond de DateTime af naar hele minuten
            Minute = minute.AddTicks(-(minute.Ticks % TimeSpan.TicksPerMinute));
            Reading = reading;
        }

        [JsonConstructor]
        private SplitReading()
        {
        }

        [JsonProperty]
        [DataMember]
        public DateTime Minute { get; private set; }

        [JsonProperty]
        [DataMember]
        public decimal Reading { get; private set; }
    }
}

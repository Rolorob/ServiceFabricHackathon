using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    public class SplitReading
    {
        public SplitReading(DateTime minute, decimal reading)
        {
            Minute = minute;
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

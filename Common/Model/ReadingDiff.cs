using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class ReadingDiff
    {
        public ReadingDiff(
            DateTime startTime,
            DateTime endTime,
            decimal diffValue)
        {
            StartTime = startTime;
            EndTime = endTime;
            DiffValue = diffValue;
        }

        [JsonConstructor]
        private ReadingDiff()
        {
        }

        [JsonProperty]
        [DataMember]
        public DateTime StartTime { get; set; }

        [JsonProperty]
        [DataMember]
        public DateTime EndTime { get; set; }

        [JsonProperty]
        [DataMember]
        public decimal DiffValue { get; set; }
    }
}

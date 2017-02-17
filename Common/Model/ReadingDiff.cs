using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

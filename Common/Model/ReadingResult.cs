using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class ReadingResult
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string ChannelId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public decimal Value { get; set; }
    }
}

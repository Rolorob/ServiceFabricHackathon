using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class ReadingDiff
    {
        public ReadingDiff(
            DateTime startTime,
            DateTime endTime,
            decimal diffValue)

            //Uri channelId,
            //string description,
            //string unit,
            //IDictionary<string, object> properties)
        {
            StartTime = startTime;
            EndTime = endTime;
            DiffValue = diffValue;
            //ChannelId = channelId;
            //Description = description;
            //Unit = unit;
            //Properties = properties;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal DiffValue { get; set; }
    }
}

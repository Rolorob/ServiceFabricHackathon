using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using static Common.Events.DeviceRead;

namespace Common.Events
{
    [DataContract]
    [KnownType(nameof(GetKnownTypes))]
    public abstract class DeviceEventBase
    {
        [JsonProperty]
        [DataMember]
        public virtual string DeviceId { get; protected set; }

        [JsonProperty]
        [DataMember]
        public virtual DeviceProperties Properties { get; protected set; }

        [JsonProperty]
        [DataMember]
        public virtual DateTime Timestamp { get; protected set; }

        public static IEnumerable<Type> GetKnownTypes()
        {
            var type = typeof(DeviceEventBase);
            return type.GetDerivedTypes(type.Assembly);
        }
    }

    public class DeviceProperties : Dictionary<string, string>
    {
        public DeviceProperties()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public DeviceProperties(IDictionary<string, string> dictionary)
            : base(dictionary, StringComparer.InvariantCultureIgnoreCase)
        {
        }
    }
}

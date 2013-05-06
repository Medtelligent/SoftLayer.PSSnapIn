using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SoftLayer.API.Services.Domain
{
    #region [Models]

    public class DnsDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public string UpdateDate { get; set; }
        public List<DnsResourceRecord> ResourceRecords { get; set; }
    }

    public class DnsResourceRecord
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "domainId")]
        public int DomainId { get; set; }
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "host")]
        public string Host { get; set; }
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        [JsonProperty(PropertyName = "responsiblePerson", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ResponsiblePerson { get; set; }
        [JsonProperty(PropertyName = "ttl", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int TTL { get; set; }
        [JsonProperty(PropertyName = "minimum", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Minimum { get; set; }
        [JsonProperty(PropertyName = "refresh", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Refresh { get; set; }
        [JsonProperty(PropertyName = "retry", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Retry { get; set; }
        [JsonProperty(PropertyName = "expire", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Expire { get; set; }
        [JsonProperty(PropertyName = "mxPriority", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int MxPriority { get; set; }
    }

    #endregion
}
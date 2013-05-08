using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using SoftLayer.API.Services.Domain;

namespace SoftLayer.API.PowerShell.Commands.Services.Domain
{
    [Cmdlet(VerbsCommon.Set, "SoftLayerResourceRecord", DefaultParameterSetName = "ResourceRecordObjectParameterSet")]
    public class SetResourceRecordCommand : ServiceCommand
    {
        #region [Parameters]

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ResourceRecordObjectParameterSet")]
        public DnsResourceRecord ResourceRecord { get; set; }

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ResourceRecordDataParameterSet")]
        public int DomainId { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "ResourceRecordDataParameterSet")]
        public int Id { get; set; }

        [Parameter(Position = 2, ParameterSetName = "ResourceRecordDataParameterSet")]
        public string Type { get; set; }

        [Parameter(Position = 3, ParameterSetName = "ResourceRecordDataParameterSet")]
        public string Host { get; set; }

        [Parameter(Position = 4, ParameterSetName = "ResourceRecordDataParameterSet")]
        public string Data { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int TTL { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int MxPriority { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public string ResponsiblePerson { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int Minimum { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int Refresh { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int Retry { get; set; }

        [Parameter(ParameterSetName = "ResourceRecordDataParameterSet")]
        public int Expire { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            // generate resource record
            var resourceRecord = ResourceRecord;
            if (resourceRecord == null)
            {
                resourceRecord = new DnsResourceRecord
                {
                    Id = Id,
                    DomainId = DomainId,
                    Type = Type,
                    Host = Host,
                    Data = Data,
                    TTL = TTL,
                    ResponsiblePerson = ResponsiblePerson,
                    MxPriority = MxPriority,
                    Minimum = Minimum,
                    Refresh = Refresh,
                    Retry = Retry,
                    Expire = Expire
                };
            }

            // validate values
            ValidateResourceRecord(resourceRecord);

            // make request
            var result = Client.Put<bool>("SoftLayer_Dns_Domain/{domainId}/ResourceRecords/{resourceRecordId}.json", r =>
            {
                // add url segments
                r.AddUrlSegment("domainId", resourceRecord.DomainId.ToString());
                r.AddUrlSegment("resourceRecordId", resourceRecord.Id.ToString());

                // set body
                r.AddBody(new Dictionary<string, List<DnsResourceRecord>> { { "parameters", new List<DnsResourceRecord> { resourceRecord } } });
            }, e => ThrowTerminatingError(e));

            WriteObject(result);
        }

        private void ValidateResourceRecord(DnsResourceRecord resourceRecord)
        {
            // required values
            if (resourceRecord.DomainId == default(long))
            {
                ThrowTerminatingError(new ErrorRecord(new ArgumentNullException("ResourceRecord.DomainId cannot be null"), "NullResourceRecordDomainId", ErrorCategory.InvalidArgument, resourceRecord));
            }
            if (resourceRecord.Id == default(long))
            {
                ThrowTerminatingError(new ErrorRecord(new ArgumentNullException("ResourceRecord.Id cannot be null"), "NullResourceRecordId", ErrorCategory.InvalidArgument, resourceRecord));
            }
            if (!string.IsNullOrWhiteSpace(resourceRecord.Type) && !new string[] { "a", "aaaa", "cname", "mx", "ns", "ptr", "soa", "spf", "srv", "txt" }.Contains(resourceRecord.Type))
            {
                ThrowTerminatingError(new ErrorRecord(new ArgumentNullException("ResourceRecord.Type unknown"), "UnknownResourceRecordType", ErrorCategory.InvalidArgument, resourceRecord));
            }
        }
    }
}

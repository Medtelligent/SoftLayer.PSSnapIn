using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using SoftLayer.API.Services.Domain;
using System.Threading.Tasks;

namespace SoftLayer.API.PowerShell.Commands.Services.Domain
{
    [Cmdlet(VerbsCommon.Get, "SoftLayerDnsDomain", DefaultParameterSetName = "DomainNameParameterSet")]
    public class GetDnsDomainCommand : ServiceCommand
    {
        #region [Parameters]

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "DomainNameParameterSet")]
        [ValidateNotNullOrEmpty]
        public string[] Name { get; set; }

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "DomainIdParameterSet")]
        [ValidateNotNullOrEmpty]
        public int[] Id { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            // get domain records first
            var domains = new List<DnsDomain>();
            if (Id != null)
            {
                Parallel.ForEach<int>(Id, domainId =>
                {
                    lock (domains)
                    {
                        domains.Add(Client.Get<DnsDomain>("SoftLayer_Dns_Domain/{domainId}.json", (r) =>
                        {
                            r.AddUrlSegment("domainId", domainId.ToString());
                        }, e => ThrowTerminatingError(e)));
                    }
                });
            }
            else
            {
                Parallel.ForEach<string>(Name, domainName =>
                {
                    lock (domains)
                    {
                        domains.AddRange(Client.Get<List<DnsDomain>>("SoftLayer_Dns_Domain/ByDomainName/{domainName}.json", (r) =>
                        {
                            r.AddUrlSegment("domainName", domainName.Trim());
                        }, e => ThrowTerminatingError(e)));
                    }
                });
            }

            // get resource records for each domain
            Parallel.ForEach<DnsDomain>(domains, d =>
            {
                d.ResourceRecords = Client.Get<List<DnsResourceRecord>>("SoftLayer_Dns_Domain/{domainId}/ResourceRecords.json", (r) => r.AddUrlSegment("domainId", d.Id.ToString()), e => ThrowTerminatingError(e));
            });

            // done
            WriteObject(domains);
        }
    }
}

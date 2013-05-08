using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace SoftLayer.API
{
    [Cmdlet(VerbsCommon.Get, "SoftLayerClient")]
    public class CreateClientCommand : PSCmdlet
    {
        #region [Parameters]

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ApiInfoParameterSet")]
        public string APIUsername { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "ApiInfoParameterSet")]
        public string APIKey { get; set; }

        [Parameter(Position = 3, ParameterSetName = "ApiInfoParameterSet")]
        public SwitchParameter UsePrivateNetwork { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            WriteObject(new Client(APIUsername, APIKey, UsePrivateNetwork.ToBool()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SoftLayer.API.Services.Domain;
using Newtonsoft.Json;

namespace SoftLayer.API.PowerShell.Commands.Services
{
    public abstract class ServiceCommand : PSCmdlet
    {
        #region [Parameters]

        [Parameter(Mandatory = true)]
        public Client Client { get; set; }

        #endregion
    }   
}

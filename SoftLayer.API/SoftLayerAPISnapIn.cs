using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.ComponentModel;

namespace SoftLayer.API.PowerShell
{
    [RunInstaller(true)]
    public class SoftLayerAPISnapIn : PSSnapIn
    {
        public override string Name
        {
            get { return "SoftLayerAPISnapIn"; }
        }

        public override string Description
        {
            get { return "This is a PowerShell snap-in that provides access to the SoftLayer services through their API"; }
        }

        public override string Vendor
        {
            get { return "Medtelligent"; }
        }
    }
}

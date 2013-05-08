SoftLayer.PSSnapIn
==================

A PowerShell snap-in for the SoftLayer API to manage SoftLayer services

# Installation

## Requirements

This snap-in has been built with .NET Framework v4.0.  So if you're running PowerShell v2.0 then you must configure it to support framework v4.0.
If you do not perform this step then the snap-in registration steps below will not work and PowerShell will complain about the runtime of the snap-in
assembely being built by a runtime newer than the one supported by PowerShell.

You can determine the version of PowerShell by running ```$host.version``` in a PowerShell window.

To add support for framework v4.0 to PowerShell v2.0 do the following:

1. Create or update the ```powershell.exe.config``` file in the following directories:
   * ```%windir%\Windows\System32\WindowsPowerShell\v1.0\```
   * ```%windir%\Windows\SysWOW64\WindowsPowerShell\v1.0\``` (only on x64 systems)
2. With the following configuration:

```xml
<?xml version="1.0"?> 
<configuration> 
    <startup useLegacyV2RuntimeActivationPolicy="true"> 
        <supportedRuntime version="v4.0.30319"/> 
        <supportedRuntime version="v2.0.50727"/> 
    </startup> 
</configuration>
```

3. Restart your PowerShell session and follow the registration steps below

## Registering the snap-in

You must first install the snap-in assembly for it to be available to add in your PowerShell sessions.

1. Open a Windows Command Prompt (cmd.exe)
2. Run the InstallUtil.exe depending on the architecture of your machine passing it the path to the assembly file

x86:
```
%windir%\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil %path_to_snapin_folder%\SoftLayer.API.dll
```

x64:
```
%windir%\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil %path_to_snapin_folder%\SoftLayer.API.dll
```

3. Verify the snap-in has been registered by running the following PS command:

```
Get-PSSnapIn -Registered
```

4. Add the snap-in to your active PS session by running this command:

```
Add-PSSnapIn SoftLayerAPISnapIn
```

# Usage

Comming soon!

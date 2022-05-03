# Revit_SwitchToWorkspaceShortcut
> By: InkRF (InkRF.net)

## Abstract
This repo is to enable the use of a keyboard shortcut to switch the active workset in Autodesk Revit to the one that is programmed.

Built using Visual Studio 2022 & .NET 4.8 Framework

Note that worksharing must be enabled for workets to be used/created. This step must be done manually prior to using the add-in

Additionally, note this code is a relativly "quick and dirty" solution to a problem that needed solving to make switching worksets more efficant. More work can (and should) be done to make it a bit more robust, and to have additional functionality.

## Revit API
When building this in Visual Studio, make sure that the ``RevitAPI`` and ``REVITAPIUI`` are added to the project references, additional information on how to do this may be found [here](https://knowledge.autodesk.com/search-result/caas/simplecontent/content/lesson-1-the-basic-plug.html)

## AddIn Manifest
Note that an additional file must be added to the Revit addin folder to add the plugin to be used in revit, 

```xml
<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
 <AddIn Type="Command">
       <Name>Revit_SwitchToWorkspaceShortcut</Name>
       <FullClassName>Revit_SwitchToWorkspaceShortcut.Class1</FullClassName>
       <Text>Switch workset</Text>
       <Description>Switches to (and/or creates) the programmed workset</Description>
       <VisibilityMode>AlwaysVisible</VisibilityMode>
       <Assembly>C:\...\bin\Release\Revit_SwitchToWorkspaceShortcut.dll</Assembly>
       <AddInId>502fe383-2648-4e98-adf8-5e6047f9dc35</AddInId>
    <VendorId>InkRF_Labs</VendorId>
    <VendorDescription>InkRF, www.InkRF.net</VendorDescription>
 </AddIn>
</RevitAddIns>
```

This file should be saved to the following subfolder (if using Windows 10): 
``
C:\ProgramData\Autodesk\Revit\Addins\20xx\ 
``
Note that ``\ProgramData\`` is hidden by default on Windows 10
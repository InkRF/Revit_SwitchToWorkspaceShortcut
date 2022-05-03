using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit_SwitchToWorkspaceShortcut
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /** WorksetID to ultimatly switch to */
            WorksetId switchTo;

            /** Name of the workset to switch to */
            string worksetName = "WORKSET_NAME";

            /** New workset to create (if one does not exist)*/
            Workset newWorkset;

            //Get application and documnet objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Get the workset table from the document
            WorksetTable worksetTable = doc.GetWorksetTable();

            // If the document is under worksharing
            if (doc.IsWorkshared)
            {

                // If worksetName is not currently in the workset table
                if (WorksetTable.IsWorksetNameUnique(doc, worksetName))
                {
                    // Create a new workset and switch to it
                    using (Transaction worksetTransaction = new Transaction(doc, "Set preview view id"))
                    {
                        worksetTransaction.Start();
                        newWorkset = Workset.Create(doc, worksetName);
                        switchTo = newWorkset.Id;
                        worksetTable.SetActiveWorksetId(switchTo);
                        worksetTransaction.Commit();
                    }
                }

                // Otherwise (the worksetName is already in the table)
                else
                {

                    // Get the workset id of the worksetName
                    switchTo = GetWorksetID(doc, worksetName);

                    // If there ID is null (no woksetTable entry (should be impossible))
                    // throw error
                    // TODO: If this section is needed, handle case where a nullReferenceException is thrown
                    //if (switchTo.Equals(null))
                    //{
                    //    TaskDialog.Show("Error", "Workset does not exist");
                    //    return Result.Failed;
                    //}

                    // Switch to the workset
                    using (Transaction worksetTransaction = new Transaction(doc, "set preview ID"))
                    {
                        worksetTransaction.Start();
                        worksetTable.SetActiveWorksetId(switchTo);
                        worksetTransaction.Commit();
                    }
                }
            }

            // Return success
            return Result.Succeeded;
        }

        public WorksetId GetWorksetID(Document doc, String str)
        {
            FilteredWorksetCollector fsc = new FilteredWorksetCollector(doc);

            // Iterate though each Workset in the workset table and check if the name matches the input string
            foreach (Workset ws in fsc)
            {
                if (ws.Name == str)
                {
                    return ws.Id;
                }
            }

            return null;
        }
    }
}

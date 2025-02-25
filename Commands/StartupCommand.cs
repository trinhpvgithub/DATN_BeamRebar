﻿using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;

namespace DATN_BeamRebar.Commands
{
    /// <summary>
    ///     External command entry point invoked from the Revit interface
    /// </summary>
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class StartupCommand : ExternalCommand
    {
        public override void Execute()
        {
        }
    }
}
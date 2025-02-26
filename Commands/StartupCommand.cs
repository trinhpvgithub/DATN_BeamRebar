using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using DATN_BeamRebar.View;

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
            //var view=new View();
            var view = new DATN_BeamRebar.View.View();
			view.ShowDialog();
		}
    }
}
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
			
			try
			{
				var view = new View.View();
				var viewModel = new ViewModel.RebarBeamViewModel(Document, view);
				viewModel.Run();
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
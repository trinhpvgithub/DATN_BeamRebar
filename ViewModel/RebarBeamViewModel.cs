using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Autodesk.Revit.DB.Structure;
using DATN_BeamRebar.View;
using System.Windows;
using Autodesk.Revit.UI;
using DATN_BeamRebar.Model;

namespace DATN_BeamRebar.ViewModel
{
	public partial class RebarBeamViewModel : ObservableObject
	{
		#region promp
		private Document Document { get; set; }
		private UIDocument UiDocument { get; set; }
		private View.View MainView { get; set; }
		private BeamInfo BeamInfo { get; set; }
		public List<RebarBarType> TypeList { get; set; }
		[ObservableProperty] private RebarBarType _top1;
		[ObservableProperty] private RebarBarType _top2;
		[ObservableProperty] private RebarBarType _top3;
		[ObservableProperty] private RebarBarType _bot1;
		[ObservableProperty] private RebarBarType _bot2;
		[ObservableProperty] private RebarBarType _bot3;
		[ObservableProperty] private RebarBarType _stirrupCenter;
		[ObservableProperty] private RebarBarType _stirrup;
		[ObservableProperty] private int _top1Count = 3;
		[ObservableProperty] private int _top2Count = 3;
		[ObservableProperty] private int _top3Count = 0;
		[ObservableProperty] private int _bot1Count = 3;
		[ObservableProperty] private int _bot2Count = 3;
		[ObservableProperty] private int _bot3Count = 0;
		[ObservableProperty] private int _stirrupCenterSpacing = 200;
		[ObservableProperty] private int _stirrupSpacing = 100;
		[ObservableProperty] private int _topAnchor = 300;
		[ObservableProperty] private int _botAnchor = 300;
		[ObservableProperty] private int _cover = 50;

		#endregion

		[RelayCommand]
		private void Ok()
		{
			//MessageBox.Show("Developping......");
			var tran=new Transaction(Document);
			tran.Start("CreateRebar");
			CreateRebar();
			tran.Commit();
			MainView.Close();
		}

		[RelayCommand]
		private void Cancel()
		{
			MainView.Close();
		}

		public RebarBeamViewModel(UIDocument uiDocument, View.View view)
		{
			UiDocument = uiDocument;
			Document = uiDocument.Document;
			MainView = view;
			MainView.DataContext = this;
			InitData();
		}

		private void InitData()
		{

			TypeList = new FilteredElementCollector(Document)
			  .OfClass(typeof(RebarBarType))
			  .Cast<RebarBarType>()
			  .ToList();
			if (TypeList.Count == 0)
				return;
			Top1 = TypeList.FirstOrDefault();
			Top2 = TypeList.FirstOrDefault();
			Top3 = TypeList.FirstOrDefault();
			Bot1 = TypeList.FirstOrDefault();
			Bot2 = TypeList.FirstOrDefault();
			Bot3 = TypeList.FirstOrDefault();
			StirrupCenter = TypeList.FirstOrDefault();
			Stirrup = TypeList.FirstOrDefault();
		}

		public void Run()
		{
			if (MainView == null)
				return;
			var beams = UiDocument.PickBeams();
			if (beams.Count > 0)
			{
				BeamInfo = new BeamInfo(beams);
				MainView.ShowDialog();
			}
			else
			{
				MessageBox.Show("Error");
			}
		}
		private void CreateRebar()
		{
			if (BeamInfo == null)
				return;
			//var top1 = new UpperRebar(RebarBeamType.Top1, BeamInfo.StartPoint, BeamInfo.EndPoint, TopAnchor,Top1Count);
			//var top2 = new UpperRebar(RebarBeamType.Top2, BeamInfo.StartPoint, BeamInfo.EndPoint, TopAnchor,Top2Count);
			//var top3 = new UpperRebar(RebarBeamType.Top3, BeamInfo.StartPoint, BeamInfo.EndPoint, TopAnchor, Top3Count);
			var bot1 = new LowerRebar(RebarBeamType.Bottom1, Bot1,BeamInfo, BotAnchor, Bot1Count);
			bot1.RebarCreation();
			var bot2 = new LowerRebar(RebarBeamType.Top2, Bot2,BeamInfo, BotAnchor,Bot2Count);
			bot2.RebarCreation();
			var bot3 = new LowerRebar(RebarBeamType.Bottom3, Bot3,BeamInfo, BotAnchor,Bot3Count);
			bot3.RebarCreation();
			//var stirrupCenter = new Stirrup(BeamInfo, BeamInfo.StartPoint, BeamInfo.EndPoint,
			//	StirrupCenterSpacing);
			//var stirrup = new Stirrup(Stirrup, BeamInfo.StartPoint, BeamInfo.EndPoint,
			//	StirrupSpacing);
		}
	}
}
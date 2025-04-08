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
    private Document Document{ get; set; }
    private UIDocument UiDocument{ get; set; }
    private View.View MainView{ get; set; }
    private BeamInfo BeamInfo{ get; set; }
    public List<RebarBarType> TypeList{ get; set; }
    [ ObservableProperty ] private RebarBarType _top1;
    [ ObservableProperty ] private RebarBarType _top2;
    [ ObservableProperty ] private RebarBarType _top3;
    [ ObservableProperty ] private RebarBarType _bot1;
    [ ObservableProperty ] private RebarBarType _bot2;
    [ ObservableProperty ] private RebarBarType _bot3;
    [ ObservableProperty ] private RebarBarType _stirrupCenter;
    [ ObservableProperty ] private RebarBarType _stirrup;

    #endregion

    [ RelayCommand ]
    private void Ok()
    {
      MessageBox.Show( "Developping......" );
    }

    [ RelayCommand ]
    private void Cancel()
    {
      MainView.Close();
    }

    public RebarBeamViewModel( UIDocument uiDocument, View.View view )
    {
      UiDocument = uiDocument;
      Document = uiDocument.Document;
      MainView = view;
      MainView.DataContext = this;
      InitData();
    }

    private void InitData()
    {
      
      TypeList = new FilteredElementCollector( Document )
        .OfClass( typeof( RebarBarType ) )
        .Cast<RebarBarType>()
        .ToList();
      if ( TypeList.Count == 0 )
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
      if ( MainView == null )
        return;
      var beams = UiDocument.PickBeams();
      if ( beams.Count > 0 )
      {
        BeamInfo = new BeamInfo( beams );
        MainView.ShowDialog();
      }
      else
      {
        MessageBox.Show( "Error" );
      }
    }
  }
}
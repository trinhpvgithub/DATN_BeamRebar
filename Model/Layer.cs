using Autodesk.Revit.DB.Structure;

namespace DATN_BeamRebar.Model;

public class Layer(
  List<Element> elements,
  Document document,
  int quantity,
  RebarBarType rebarBarType,
  RebarBeamType rebarBeamType )
  : BeamInfo( elements )
{
  private Document Document{ get; set; } = document;
  private int Quantity{ get; set; } = quantity;
  private RebarBarType RebarBarType{ get; set; } = rebarBarType;
  private RebarBeamType RebarBeamType{ get; set; } = rebarBeamType;


  private void CreateRebar()
  {
    if ( Quantity < 0 ) return;
    var curves = RebarAnalysis();
    if ( curves.Count < 0 ) return;
    var host = DirectShape.CreateElement( Document, new ElementId( BuiltInCategory.OST_StructuralFraming ) );
    Document.CreateRebarSingle( RebarStyle.Standard, RebarBarType, host, CrossDirection, curves );
  }

  private List<Curve> RebarAnalysis()
  {
    var result = new List<Curve>();
    if ( RebarBeamType is RebarBeamType.Top1 or RebarBeamType.Top2 or RebarBeamType.Top3 )
    {
      var mainCurve = Line.CreateBound( StartPoint, EndPoint )
        .OffSetCurve( Width - 50 / 304.8, CrossDirection )
        .OffSetCurve( 50 / 308.4, -XYZ.BasisZ );
      var distance = ( Width - 100 / 304.8 ) / ( Quantity - 1 );
      for ( var i = 0; i < Quantity; i++ )
      {
        var curve = mainCurve.OffSetCurve( distance * i, -CrossDirection );
        if ( curve != null ) result.Add( curve );
      }
    }
    else
    {
      var mainCurve = Line.CreateBound( StartPoint, EndPoint )
        .OffSetCurve( Width - 50 / 304.8, CrossDirection )
        .OffSetCurve( Height + 50 / 308.4, -XYZ.BasisZ );
      var distance = ( Width - 100 / 304.8 ) / ( Quantity - 1 );
      for ( var i = 0; i < Quantity; i++ )
      {
        var curve = mainCurve.OffSetCurve( distance * i, -CrossDirection );
        if ( curve != null ) result.Add( curve );
      }
    }

    return result;
  }
}
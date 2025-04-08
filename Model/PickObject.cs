using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace DATN_BeamRebar.Model;

public static class PickObject
{
  public static List<Element> PickBeams( this UIDocument uiDocument )
  {
    var eles = uiDocument.Selection.PickObjects( ObjectType.Element, new BeamFilter() )
      .Select(x=>uiDocument.Document.GetElement(x) as Element).ToList();
    if( !eles.Any() ) return null;
    return eles.CheckBeams() ? eles : null;
  }
  private static bool CheckBeams(this List<Element> eles)
  {
    var heights = new List<double>();
    var widths = new List<double>();
    foreach ( var beam in eles )
    {
      var h = beam.LookupParameter( "h" ).AsDouble();
      var w = beam.LookupParameter( "b" ).AsDouble();
      heights.Add(h);
      widths.Add(w);
    }
    return heights.Count == 1 && widths.Count == 1;
  }
}
public class BeamFilter: ISelectionFilter
{
  public bool AllowElement( Element elem )
  {
    if ( elem is not FamilyInstance familyInstance ) return false ;
    
    if((familyInstance.Location as LocationCurve)?.Curve ==null) return false ;
    return familyInstance.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming;
  }

  public bool AllowReference( Reference reference, XYZ position )
  {
    throw new NotImplementedException();
  }
}
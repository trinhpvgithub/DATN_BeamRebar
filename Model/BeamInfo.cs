﻿namespace DATN_BeamRebar.Model;

public class BeamInfo
{
  private List<FamilyInstance> Families { get; }
  protected double Height { get; private set; }
  protected double Width { get; private set; }
  protected XYZ StartPoint { get; private set; }
  protected XYZ EndPoint { get; private set; }
  protected XYZ Direction { get; private set; }
  protected XYZ CrossDirection { get; private set; }

  public BeamInfo(List<Element>  elements )
  {
    if (elements == null) return;
    Families = elements.Select(x=>x as  FamilyInstance).ToList();
  }

  private void GetHeightAndWidth()
  {
    var heights = new List<double>();
    var widths = new List<double>();
    foreach ( var beam in Families )
    {
      var h = beam.LookupParameter( "h" ).AsDouble();
      var w = beam.LookupParameter( "b" ).AsDouble();
      heights.Add(h);
      widths.Add(w);
    }

    if ( heights.Count != 1 || widths.Count != 1 ) return;
    Height = heights[0];
    Width = widths[0];
  }

  private void GetStartAndEndPoint()
  {
    Direction = ( ( Families[ 0 ].Location as LocationCurve )?.Curve as Line )?.Direction;
    CrossDirection = Direction!.CrossProduct(XYZ.BasisZ);
    var curves = new List<Curve>();
    foreach ( var beam in Families )
    {
      var locationCurve=(beam.Location as LocationCurve)?.Curve;
      curves.Add(locationCurve);
    }
    var curs= curves.OrderBy(x=>(x as Line)?.Direction.DotProduct(Direction) ?? 0);
    if( !curs.Any() ) return;
    StartPoint= curs.First().GetEndPoint(0);
    EndPoint= curs.Last().GetEndPoint(1);
  }
}
namespace DATN_BeamRebar.Model;

public static class LineUtils
{
  public static Curve OffSetCurve( this Curve curve ,double offset ,XYZ direction )
  {
    if ( curve == null ) return null;
    var start = curve.GetEndPoint( 0 );
    var end = curve.GetEndPoint( 1 );
    var newStart = start.Add( direction * offset );
    var newEnd = end.Add( direction * offset );
    return newStart == newEnd ? null : Line.CreateBound( newStart, newEnd );
  }
}
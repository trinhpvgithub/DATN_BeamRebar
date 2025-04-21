namespace DATN_BeamRebar.Model;

public class LowerRebar
{
  private RebarBeamType RebarBeamType{ get; set; }
  private XYZ Start{ get; set; }
  private XYZ End{ get; set; }
  private double Anchor{ get; set; }
  public List<XYZ> Curves{ get; set; } = new();

  public LowerRebar( RebarBeamType rebarBeamType, XYZ start, XYZ end, double anchor )
  {
    RebarBeamType = rebarBeamType;
    Start = start;
    End = end;
    Anchor = anchor;
    RebarAnalys();
  }

  private void RebarAnalys()
  {
    if ( Anchor == 0 )
    {
      Curves.Add( Start );
      Curves.Add( End );
    }
    else
    {
      var start = Start.Add( Anchor * XYZ.BasisZ );
      var end = End.Add( Anchor * XYZ.BasisZ );
      Curves.Add( start );
      Curves.Add( Start );
      Curves.Add( End );
      Curves.Add( end );
    }
  }
}
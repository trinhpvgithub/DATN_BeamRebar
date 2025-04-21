namespace DATN_BeamRebar.Model;

public class StirrupRebar
{
  private BeamInfo beamInfo{ get; set; }
  public RebarBeamType RebarBeamType{ get; set; } = RebarBeamType.Stirrup;
  public double Spacing{ get; set; }
  public XYZ Start{ get; set; }
  public XYZ End{ get; set; }
  public XYZ Direction{ get; set; }
  public List<List<XYZ>> Curves{ get; set; } = new();

  public StirrupRebar( double spacing, XYZ start, XYZ end, XYZ direction )
  {
    Spacing = spacing;
    Start = start;
    End = end;
    Direction = direction;
  }

  private void RebarAnalys()
  {
  }
}
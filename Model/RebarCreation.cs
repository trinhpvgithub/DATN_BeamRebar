using Autodesk.Revit.DB.Structure;

namespace DATN_BeamRebar.Model;

public class RebarCreation
{
  public Document Document { get; set; }
  public XYZ StartPoint { get; set; }
  public XYZ EndPoint { get; set; }
  public RebarBarType  BarType { get; set; }
  

  public RebarCreation()
  {
    
  }

  public void Create()
  {
    
  }
}
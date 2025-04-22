namespace DATN_BeamRebar.Model;

public class UpperRebar
{
	private RebarBeamType RebarBeamType { get; set; }
	private XYZ Start { get; set; }
	private XYZ End { get; set; }
	private double Anchor { get; set; }
	public List<XYZ> Curves { get; set; } = new();
	public int Quantity { get; set; } = 0;

	public UpperRebar(RebarBeamType rebarBeamType, XYZ start, XYZ end, double anchor, int quatity)
	{
		RebarBeamType = rebarBeamType;
		Start = start;
		End = end;
		Anchor = anchor;
		Quantity = quatity;
		RebarAnalys();
	}

	private void RebarAnalys()
	{
		if (Quantity == 0) return;
		switch (RebarBeamType)
		{
			case RebarBeamType.Top1:
				Start = Start.Add(50.0.MmToFeet() * -XYZ.BasisZ);
				End = End.Add(50.0.MmToFeet() * -XYZ.BasisZ);
				break;
			case RebarBeamType.Top2:
				Start = Start.Add(130.0.MmToFeet() * -XYZ.BasisZ);
				End = End.Add(130.0.MmToFeet() * -XYZ.BasisZ);
				break;
			case RebarBeamType.Top3:
				Start = Start.Add(210.0.MmToFeet() * -XYZ.BasisZ);
				End = End.Add(210.0.MmToFeet() * -XYZ.BasisZ);
				break;
			default:
				break;
		}
		if (Anchor == 0)
		{
			Curves.Add(Start);
			Curves.Add(End);
		}
		else
		{
			var start = Start.Add(Anchor * -XYZ.BasisZ);
			var end = End.Add(Anchor * -XYZ.BasisZ);
			Curves.Add(start);
			Curves.Add(Start);
			Curves.Add(End);
			Curves.Add(end);
		}
	}

}
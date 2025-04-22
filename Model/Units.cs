using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_BeamRebar.Model
{
	public static class Units
	{
		public static double MmToFeet(this double mm)
		{
			return mm / 304.8;
		}
		public static double FeetToMm(this double feet)
		{
			return feet * 304.8;
		}
	}
}

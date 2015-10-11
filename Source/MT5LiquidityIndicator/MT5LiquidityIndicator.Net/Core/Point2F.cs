using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal struct Point2F
	{
		internal float X;
		internal float? Y;

		internal Point2F(float x, float? y) : this()
		{
			this.X = x;
			this.Y = y;
		}
		public override string ToString()
		{
			string y = Y.HasValue ? Y.ToString() : "null";
			string result = string.Format("{0}, {1}", X, y);
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal struct RectF
	{
		internal float MinX;
		internal float MaxX;
		internal float MinY;
		internal float MaxY;

		internal RectF(float minX, float minY, float maxX, float maxY) : this()
		{
			this.MinX = minX;
			this.MinY = minY;
			this.MaxX = maxX;
			this.MaxY = maxY;
		}
		internal float Width
		{
			get
			{
				return (MaxX - MinX);
			}
		}
		internal float Height
		{
			get
			{
				return (MaxY - MinY);
			}
		}
	}
}

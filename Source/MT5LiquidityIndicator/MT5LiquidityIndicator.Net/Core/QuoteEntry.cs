using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT5LiquidityIndicator.Net.Core
{
	internal struct QuoteEntry
	{
		internal double Price;
		internal double Volume;

		internal QuoteEntry(double price, double volume) : this()
		{
			this.Price = price;
			this.Volume = volume;
		}
	}
}

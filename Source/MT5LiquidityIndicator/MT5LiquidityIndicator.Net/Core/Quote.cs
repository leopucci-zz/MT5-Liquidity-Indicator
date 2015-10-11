using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class Quote
	{
		internal DateTime CreatingTime { get; private set; }
		internal QuoteEntry[] Bids { get; private set; }
		internal QuoteEntry[] Asks { get; private set; }
		internal double Bid { get; private set; }
		internal double Ask { get; private set; }
	}
}

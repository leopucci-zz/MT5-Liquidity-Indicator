using MT5LiquidityIndicator.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT5LiquidityIndicator.Net.MQL5
{
	internal class TickEventArgs : EventArgs
	{
		internal TickEventArgs(Quote quote)
		{
			this.Tick = quote;
		}
		internal Quote Tick { get; private set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal struct QuoteEx
	{
		#region construction
		internal QuoteEx(Quote quote) : this()
		{
			m_time = EnvironmentEx.GetTickCountEx();
			m_quote = quote;
		}
		internal QuoteEx(long time, Quote quote): this()
		{
			m_time = time;
			m_quote = quote;
		}
		#endregion
		#region properties
		internal long Time
		{
			get
			{
				return m_time;
			}
		}
		internal Quote Quote
		{
			get
			{
				return m_quote;
			}
		}
		internal static QuoteEx Empty
		{
			get
			{
				return new QuoteEx();
			}
		}
		#endregion
		#region members
		private long m_time;

		private Quote m_quote;
		#endregion
	}
}

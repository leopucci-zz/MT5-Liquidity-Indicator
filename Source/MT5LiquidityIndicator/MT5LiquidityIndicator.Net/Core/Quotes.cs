using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class Quotes
	{
		#region construction
		internal Quotes(int intervalInSeconds)
		{
			m_intervalInMs = intervalInSeconds * 1000;
		}
		#endregion
		#region methods
		internal void Add(Quote quote)
		{
			lock (m_synchronizer)
			{
				long now = EnvironmentEx.GetTickCountEx();
				QuoteEx entry = new QuoteEx(now, quote);
				m_first.Add(entry);
			}
		}

		internal void Set(Quote[] quotes)
		{
			m_quotes.Clear();

			Quote last = quotes.Last();
			m_time = 0;

			foreach (var element in quotes)
			{
				long time = (long)(element.CreatingTime - last.CreatingTime).TotalMilliseconds;
				long delta = (m_time - time);
				if (delta <= m_intervalInMs)
				{
					QuoteEx quote = new QuoteEx(time, element);
					m_quotes.Enqueue(quote);
				}
			}
		}

		internal void Refresh()
		{
			lock (m_synchronizer)
			{
				Algorithm.Swap(ref m_first, ref m_second);
				m_first.Clear();
			}
			foreach (var element in m_second)
			{
				m_quotes.Enqueue(element);
				m_last = element;
			}
			long now = EnvironmentEx.GetTickCountEx();
			for (; m_quotes.Count > 0; )
			{
				QuoteEx entry = m_quotes.Peek();
				long delta = (now - entry.Time);
				if (delta > m_intervalInMs)
				{
					m_quotes.Dequeue();
				}
				else
				{
					break;
				}
			}
			m_time = now;
		}
		#endregion
		#region properties
		internal bool Empty
		{
			get
			{
				if (m_quotes.Count > 0)
				{
					return false;
				}
				bool result = (null == m_last.Quote);
				return result;
			}
		}
		internal Queue<QuoteEx> Items
		{
			get
			{
				return m_quotes;
			}
		}
		internal long Time
		{
			get
			{
				return m_time;
			}
		}
		internal int Interval
		{
			get
			{
				return (m_intervalInMs / 1000);
			}
			set
			{
				m_intervalInMs = 1000 * value;
			}
		}
		internal QuoteEx Last
		{
			get
			{
				return m_last;
			}
		}
		#endregion

		#region members
		private int m_intervalInMs;
		private long m_time;
		private readonly Queue<QuoteEx> m_quotes = new Queue<QuoteEx>();
		private QuoteEx m_last;
		private List<QuoteEx> m_first = new List<QuoteEx>();
		private List<QuoteEx> m_second = new List<QuoteEx>();
		private readonly object m_synchronizer = new object();
		#endregion
	}
}

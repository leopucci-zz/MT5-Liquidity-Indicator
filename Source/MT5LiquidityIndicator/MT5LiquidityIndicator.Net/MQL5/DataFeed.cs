using MT5LiquidityIndicator.Net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT5LiquidityIndicator.Net.MQL5
{
	internal unsafe class DataFeed : IDisposable
	{
		internal DataFeed(IntPtr handle, IntPtr func2)
		{
			m_handle = handle;
			if (IntPtr.Zero != func2)
			{
				m_func = (WaitForLevel2)Marshal.GetDelegateForFunctionPointer(func2, typeof(WaitForLevel2));
			}
			m_continue = true;
			m_thread = new Thread(Loop);
			m_thread.Start();
		}

		internal event EventHandler<TickEventArgs> Tick;

		public void Dispose()
		{
			m_continue = false;
			if (null != m_thread)
			{
				m_thread.Join();
			}
		}

		private void Loop()
		{
			if (null != m_func)
			{
				DoLoop();
			}
		}

		private void DoLoop()
		{
			for (; m_continue;)
			{
				if (null == m_func)
				{
					Thread.Sleep(256);
					continue;
				}

				byte* ptr = m_func(m_handle, 256);
				if (null == ptr)
				{
					continue;
				}
				var func = Tick;
				if (null == func)
				{
					continue;
				}
				Quote quote = new Quote(ptr);
				if ((quote.Bids.Length > 0) || (quote.Asks.Length > 0))
				{
					TickEventArgs e = new TickEventArgs(quote);
					func(this, e);
				}
			}

			m_func(m_handle, 0);
		}

		#region types
		private delegate byte* WaitForLevel2(IntPtr pThis, uint timeoutInMs);
		#endregion

		#region members
		private IntPtr m_handle;
		private WaitForLevel2 m_func;
		private volatile bool m_continue;
		private Thread m_thread;

		#endregion
	}
}

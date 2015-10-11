using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MT5LiquidityIndicator.Net.Core
{
	internal static class EnvironmentEx
	{
		internal static long GetTickCountEx()
		{
			Monitor.Enter(m_synchronizer);
			uint currentTime = GetTickCount();
			uint lastUpdateTime = m_lsastUpdatedTime;
			if (currentTime >= lastUpdateTime)
			{
				m_currentTimeInMilliseconds += (currentTime - lastUpdateTime);
			}
			else
			{
				m_currentTimeInMilliseconds += currentTime + (uint.MaxValue - lastUpdateTime) + 1;
			}
			m_lsastUpdatedTime = currentTime;
			long result = (long)m_currentTimeInMilliseconds;
			Monitor.Exit(m_synchronizer);
			return result;
		}
		#region extern functions
		[DllImport("kernel32.dll")]
		static extern uint GetTickCount();
		#endregion

		#region members
		private static long m_currentTimeInMilliseconds = 0;
		private static uint m_lsastUpdatedTime = 0;
		private readonly static object m_synchronizer = new object();
		#endregion
	}
}

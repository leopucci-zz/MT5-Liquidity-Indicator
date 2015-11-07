using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MT5LiquidityIndicator.Net.View;

namespace MT5LiquidityIndicator.Net
{
	internal static class ChartBuilder
	{
		public static int Run(string argument)
		{
			try
			{
				Debug.WriteLine(argument);
				Parameters parameters = new Parameters(argument);
				Chart indicator = new Chart();
				Debug.WriteLine(parameters.Symbol);
				indicator.Construct(parameters);
				indicator.Show();

				return indicator.Handle.ToInt32();
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return 0;
			}
		}
	}
}

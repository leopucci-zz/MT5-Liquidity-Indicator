using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MT5LiquidityIndicator.Net
{
	internal class Parameters
	{
		internal Parameters(string st)
		{
			Dictionary<string, string> args = ParseString(st);
			this.Symbol = args[cSymbol];
			this.Digits = int.Parse(args[cDigits], CultureInfo.InvariantCulture);
			this.LotSize = double.Parse(args[cLotSize], CultureInfo.InvariantCulture);
			this.PricePip = Math.Pow(10, -Digits);
			this.PriceFactor = Math.Pow(10, Digits);
			this.RoundingStepOfPrice = 5 * this.PricePip;
			this.PriceFormat = string.Format("F{0}", this.Digits);
			this.This = new IntPtr(long.Parse(args[cThis]));
			this.Func = new IntPtr(long.Parse(args[cFunc]));
			if (IntPtr.Zero != this.Func)
			{
				m_setHeight = (SetHeightFunc)Marshal.GetDelegateForFunctionPointer(this.Func, typeof(SetHeightFunc));
			}
		}
		#region internal methods
		internal void SetHeight(int height)
		{
			if (null != m_setHeight)
			{
				m_setHeight(this.This, height);
			}
		}
		#endregion
		#region private methods
		private static Dictionary<string, string> ParseString(string st)
		{
			char[] characters = new char[] { '|' };
			string[] args = st.Split(characters, StringSplitOptions.RemoveEmptyEntries);
			Regex pattern = new Regex(@"^(\w+)=(.*)$");
			Dictionary<string, string> result = new Dictionary<string, string>();
			foreach (var element in args)
			{
				Match match = pattern.Match(element);
				if (!match.Success)
				{
					throw new ArgumentException(st, "st");
				}
				string key = match.Groups[1].Value;
				string value = match.Groups[2].Value;
				result[key] = value;
			}
			return result;
		}
		#endregion
		#region properties
		internal string Symbol { get; private set; }
		internal int Digits { get; private set; }
		internal double LotSize { get; private set; }
		internal double PricePip { get; private set; }
		internal double PriceFactor { get; private set; }
		internal double RoundingStepOfPrice { get; private set; }
		internal string PriceFormat { get; private set; }
		internal IntPtr This { get; private set; }
		internal IntPtr Func { get; private set; }
		internal IntPtr Handle { get; private set; }
		#endregion
		#region constants
		private const string cDllPath = "DllPath";
		private const string cSymbol = "Symbol";
		private const string cTimeFrame = "TimeFrame";
		private const string cDigits = "Digits";
		private const string cLotSize = "LotSize";
		private const string cPrecision = "Precision";
		private const string cThis = "This";
		private const string cFunc = "Func";
		#endregion
		#region types
		private delegate void SetHeightFunc(IntPtr pThis, int height);
		#endregion
		#region members
		private SetHeightFunc m_setHeight;
		#endregion
	}
}

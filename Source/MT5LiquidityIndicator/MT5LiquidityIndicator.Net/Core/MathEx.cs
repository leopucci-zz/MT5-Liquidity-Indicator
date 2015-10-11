using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal static class MathEx
	{
		internal static double Round(double value, double step)
		{
			double integer = Math.Floor(value);
			double fractional = value - integer;
			double rounded = DoRound(fractional, step);
			double result = integer + rounded;
			return result;
		}
		internal static double RoundUp(double value, double step)
		{
			double integer = Math.Floor(value);
			double fractional = value - integer;
			double rounded = DoRoundUp(fractional, step);
			double result = integer + rounded;
			return result;
		}
		internal static double RoundDown(double value, double step)
		{
			double integer = Math.Floor(value);
			double fractional = value - integer;
			double rounded = DoRoundDown(fractional, step);
			double result = integer + rounded;
			return result;
		}

		private static double DoRoundUp(double value, double step)
		{
			long number = (long)(value / step);
			if (number * step < value)
			{
				number++;
			}
			if ((number - 1) * step >= value)
			{
				number--;
			}

			double result = number * step;
			return result;
		}

		private static double DoRoundDown(double value, double step)
		{
			long number = (long)(value / step);
			if (number * step > value)
			{
				number--;
			}
			if ((number + 1) * step <= value)
			{
				number++;
			}

			double result = number * step;
			return result;
		}
		private static double DoRound(double value, double step)
		{
			long number = (long)(value / step + 0.5);
			double result = number * step;
			return result;
		}
		internal static float? CalculateWAVP(double requiredVolume, QuoteEntry[] entries)
		{
			if ((null == entries) || (0 == entries.Length))
			{
				return null;
			}
			if (0 == requiredVolume)
			{
				return (float)entries[0].Price;
			}
			double cost = 0;
			double remainingVolume = requiredVolume;

			foreach (var element in entries)
			{
				if (element.Volume < remainingVolume)
				{
					cost += element.Price * element.Volume;
					remainingVolume -= element.Volume;
				}
				else
				{
					cost += element.Price * remainingVolume;
					remainingVolume = 0;
				}
			}
			if (remainingVolume > 0)
			{
				return null;
			}
			float result = (float)(cost / requiredVolume);
			return result;
		}
	}
}

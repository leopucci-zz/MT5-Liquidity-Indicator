using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MT5LiquidityIndicator.Net.Settings
{
	internal static class ColorSerializer
	{
		internal static string StringFromColor(Color value)
		{
			string result = string.Format("Name={0};Alpha={1};Red={2};Green={3};Blue={4};", value.Name, value.A, value.R, value.G, value.B);
			return result;
		}
		internal static Color ColorFromString(string value)
		{
			Match match = m_pattern.Match(value);
			if (!match.Success)
			{
				string message = string.Format("Invalid color format = {0}", value);
				throw new ArgumentException(message, "value");
			}
			string name = match.Groups[1].Value;
			if (!string.IsNullOrEmpty(name))
			{
				return Color.FromName(name);
			}
			byte alpha = byte.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
			byte red = byte.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
			byte green = byte.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);
			byte blue = byte.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
			Color result = Color.FromArgb(alpha, red, green, blue);
			return result;
		}
		private static Regex m_pattern = new Regex("Name=(.*);Alpha=(.*);Red=(.*);Green=(.*);Blue=(.*);");
	}
}

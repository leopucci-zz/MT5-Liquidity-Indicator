using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MT5LiquidityIndicator.Net.Settings
{
	[DisplayName("Line Settings")]
	public class LineSettings
	{
		#region contruction
		public LineSettings()
		{
			this.Volume = 0;
			this.m_bidColor = Color.Black;
			this.m_askColor = Color.Black;
		}
		internal LineSettings(LineSettings settings)
		{
			this.Volume = settings.Volume;
			this.m_bidColor = settings.m_bidColor;
			this.m_askColor = settings.m_askColor;
		}
		internal LineSettings(double volume, Color bidColor, Color askColor)
		{
			this.Volume = volume;
			this.m_bidColor = bidColor;
			this.m_askColor = askColor;
		}
		#endregion
		#region properties
		[DefaultValue(1)]
		public double Volume
		{
			get
			{
				return m_volume;
			}
			set
			{
				if ((value < m_minVolume) || (value > m_maxVolume))
				{
					string message = string.Format("Volum can be from {0} to {1}", m_minVolume, m_maxVolume);
					throw new ArgumentOutOfRangeException("value", value, message);
				}
				m_volume = value;
			}
		}
		[DisplayName("Bid Color")]
		[DefaultValue(typeof(Color), "Black")]
		public Color BidColor
		{
			get
			{
				return m_bidColor;
			}
			set
			{
				m_bidColor = NormalizeColor(value);
			}
		}
		[DisplayName("Ask Color")]
		[DefaultValue(typeof(Color), "Black")]
		public Color AskColor
		{
			get
			{
				return m_askColor;
			}
			set
			{
				m_askColor = NormalizeColor(value);
			}
		}
		#endregion
		#region private members
		private static Color NormalizeColor(Color value)
		{
			if (255 == value.A)
			{
				return value;
			}
			Color result = Color.FromArgb(255, value.R, value.G, value.B);
			return result;
		}
		#endregion

		#region overrode methods
		public override string ToString()
		{
			string result = string.Format("Volume = {0}", this.Volume);
			return result;
		}
		#endregion

		#region members
		private double m_volume;
		private const double m_minVolume = 0;
		private const double m_maxVolume = 10000;

		private Color m_bidColor;
		private Color m_askColor;

		#endregion

	}
}

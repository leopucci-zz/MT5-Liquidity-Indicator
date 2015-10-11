using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MT5LiquidityIndicator.Net.Core;

namespace MT5LiquidityIndicator.Net.Settings
{
	internal class ChartSettings
	{
		#region construction
		internal ChartSettings()
		{
			Initialize();
		}
		internal ChartSettings(ChartSettings settings)
		{
			if (null == settings)
			{
				throw new ArgumentNullException("settings");
			}
			this.m_backgroundColor = settings.m_backgroundColor;
			this.m_foregoundColor = settings.m_foregoundColor;
			this.Mode = settings.Mode;
			this.Type = settings.Type;
			this.m_duration = settings.m_duration;
			this.m_height = settings.m_height;
			this.m_updateInterval = settings.m_updateInterval;
			this.Grid = settings.Grid;

			this.Lines = new List<LineSettings>();
			this.Lines.Capacity = settings.Lines.Count;

			foreach (var element in settings.Lines)
			{
				LineSettings line = new LineSettings(element);
				this.Lines.Add(line);
			}
		}
		internal void ResetToDefault()
		{
			Initialize();

			this.Lines.Add(new LineSettings(10, Color.Green, Color.Green));
			this.Lines.Add(new LineSettings(100, Color.Blue, Color.Blue));
			this.Lines.Add(new LineSettings(200, Color.Red, Color.Red));
		}
		private void Initialize()
		{
			this.m_backgroundColor = Color.White;
			this.m_foregoundColor = Color.Black;
			this.Mode = RenderingMode.Quality;
			this.m_duration = 60;
			this.m_height = 250;
			this.m_updateInterval = 500;
			this.Grid = false;

			this.Lines = new List<LineSettings>();
		}
		#endregion
		#region properties
		[Category("Chart")]
		[DefaultValue(RenderingMode.Quality)]
		[Description("Specifies smoothing mode for the lines rendering")]
		public RenderingMode Mode { get; set; }

		[Category("Chart")]
		[DefaultValue(LineType.Straight)]
		[Description("Specifies type of the lines")]
		public LineType Type { get; set; }

		[Category("Chart")]
		[DefaultValue(false)]
		[DisplayName("Show Grid")]
		[Description("Displays or hides chart's grid")]
		public bool Grid { get; set; }

		[Category("Chart")]
		[DefaultValue(typeof(Color), "White")]
		[DisplayName("Background Color")]
		[Description("Specifies background color of the chart section")]
		public Color BackgroundColor
		{
			get
			{
				return m_backgroundColor;
			}
			set
			{
				if (255 == value.A)
				{
					m_backgroundColor = value;
				}
				else
				{
					m_backgroundColor = Color.FromArgb(255, value.R, value.G, value.B);
				}
			}
		}

		[Category("Chart")]
		[DisplayName("Foreground Color")]
		[Description("Specifies foreground color of the chart section")]
		[DefaultValue(typeof(Color), "Black")]
		public Color ForegroundColor
		{
			get
			{
				return m_foregoundColor;
			}
			set
			{
				if (255 == value.A)
				{
					m_foregoundColor = value;
				}
				else
				{
					m_foregoundColor = Color.FromArgb(255, value.R, value.G, value.B);
				}
			}
		}

		[Category("Chart")]
		[DefaultValue(60)]
		[Description("Specifies time range for chart's rendering")]
		public int Duration
		{
			get
			{
				return m_duration;
			}
			set
			{
				if ((value < m_minDuration) || (value > m_maxDuration))
				{
					string message = string.Format("Duration can be from {0} to {1}", m_minDuration, m_maxDuration);
					throw new ArgumentOutOfRangeException("value", value, message);
				}
				m_duration = value;
			}
		}
		[Category("Chart")]
		[DefaultValue(250)]
		[Description("Specifies height of indicator window")]
		public int Height
		{
			get
			{
				return m_height;
			}
			set
			{
				if ((value < m_minHeight) || (value > m_maxHeight))
				{
					string message = string.Format("Height can be from {0} to {1}", m_minHeight, m_maxHeight);
					throw new ArgumentOutOfRangeException("value", value, message);
				}
				m_height = value;
			}
		}

		[Category("Chart")]
		[DefaultValue(500)]
		[DisplayName("Update Interval")]
		[Description("Specifies chart update interval in ms")]
		public int UpdateInterval
		{
			get
			{
				return m_updateInterval;
			}
			set
			{
				if ((value < m_minUpdateInterval) || (value > m_maxUpdateInterval))
				{
					string message = string.Format("Update interval can be from {0} to {1}", m_minUpdateInterval, m_maxUpdateInterval);
					throw new ArgumentOutOfRangeException("value", value, message);
				}
				m_updateInterval = value;
			}
		}
		[Category("Lines")]
		[Description("Configures bid and ask lines visualization")]
		public List<LineSettings> Lines { get; private set; }
		#endregion
		#region methods
		internal static ChartSettings MakeDefault()
		{
			ChartSettings result = new ChartSettings();
			result.ResetToDefault();
			return result;
		}
		#endregion

		#region members
		private int m_duration;
		private const int m_minDuration = 10;
		private const int m_maxDuration = 600;

		private int m_height;
		private const int m_minHeight = 200;
		private const int m_maxHeight = 2048;

		private int m_updateInterval;
		private const int m_minUpdateInterval = 10;
		private const int m_maxUpdateInterval = 10000;
		private Color m_backgroundColor;
		private Color m_foregoundColor;

		#endregion
	}
}

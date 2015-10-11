using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MT5LiquidityIndicator.Net.Settings.Version0
{
	public class ChartSettings0
	{
		[XmlAttribute("Symbol")]
		public string Symbol { get; set; }
		[XmlAttribute("Mode")]
		public Mode0 Mode { get; set; }
		[XmlAttribute("Type")]
		public LineType0 Type { get; set; }
		[XmlAttribute("Grid")]
		public bool Grid { get; set; }
		[XmlAttribute("BackgroundColor")]
		public string BackgroundColor { get; set; }
		[XmlAttribute("ForegroundColor")]
		public string ForegroundColor { get; set; }
		[XmlAttribute("Duration")]
		public int Duration { get; set; }
		[XmlAttribute("Height")]
		public int Height { get; set; }
		[XmlAttribute("UpdateInterval")]
		public int UpdateInterval { get; set; }

		[XmlArray("Lines")]
		public List<LineSettings0> Lines { get; set; }
	}
}

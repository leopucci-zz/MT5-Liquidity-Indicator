using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MT5LiquidityIndicator.Net.Settings.Version0
{
	public class LineSettings0
	{
		[XmlAttribute("Volume")]
		public double Volume { get; set; }
		[XmlAttribute("BidColor")]
		public string BidColor { get; set; }
		[XmlAttribute("AskColor")]
		public string AskColor { get; set; }
	}
}

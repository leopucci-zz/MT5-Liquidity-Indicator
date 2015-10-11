using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MT5LiquidityIndicator.Net.Core;
using MT5LiquidityIndicator.Net.Settings.Version0;

namespace MT5LiquidityIndicator.Net.Settings
{
	public class Configuration
	{
		#region construction
		public Configuration()
		{
		}
		internal Configuration(Dictionary<string, ChartSettings> symbolToSettings)
		{
			if (null == symbolToSettings)
			{
				throw new ArgumentNullException("symbolToSettings");
			}
			this.Settings0 = new Settings0();
			this.Settings0.Charts = CreateCharts(symbolToSettings);
		}
		private static List<ChartSettings0> CreateCharts(Dictionary<string, ChartSettings> symbolToSettings)
		{
			List<ChartSettings0> result = new List<ChartSettings0>();
			result.Capacity = symbolToSettings.Count;
			foreach (var element in symbolToSettings)
			{
				ChartSettings0 item = CreateChart(element.Key, element.Value);
				result.Add(item);
			}
			return result;
		}
		private static ChartSettings0 CreateChart(string symbol, ChartSettings settings)
		{
			ChartSettings0 result = new ChartSettings0();
			result.Symbol = symbol;
			result.Mode = (Mode0)settings.Mode;
			result.Type = (LineType0)settings.Type;
			result.Grid = settings.Grid;
			result.BackgroundColor = ColorSerializer.StringFromColor(settings.BackgroundColor);
			result.ForegroundColor = ColorSerializer.StringFromColor(settings.ForegroundColor);
			result.Duration = settings.Duration;
			result.Height = settings.Height;
			result.UpdateInterval = settings.UpdateInterval;

			result.Lines = CreateLines(settings.Lines);

			return result;
		}
		private static List<LineSettings0> CreateLines(List<LineSettings> settings)
		{
			List<LineSettings0> result = new List<LineSettings0>();
			result.Capacity = settings.Count;
			foreach (var element in settings)
			{
				LineSettings0 item = CreateLine(element);
				result.Add(item);
			}
			return result;
		}
		private static LineSettings0 CreateLine(LineSettings settings)
		{
			LineSettings0 result = new LineSettings0();
			result.BidColor = ColorSerializer.StringFromColor(settings.BidColor);
			result.AskColor = ColorSerializer.StringFromColor(settings.AskColor);
			result.Volume = settings.Volume;

			return result;
		}
		#endregion
		#region converting
		internal Dictionary<string, ChartSettings> ToSettings()
		{
			Dictionary<string, ChartSettings> result = new Dictionary<string, ChartSettings>();
			foreach (var element in this.Settings0.Charts)
			{
				string key = element.Symbol;
				ChartSettings settings = CreateChart(element);
				result[key] = settings;
			}
			return result;
		}
		private static ChartSettings CreateChart(ChartSettings0 settings)
		{
			ChartSettings result = new ChartSettings();
			result.Mode = (RenderingMode)settings.Mode;
			result.Type = (LineType)settings.Type;
			result.Grid = settings.Grid;
			result.BackgroundColor = ColorSerializer.ColorFromString(settings.BackgroundColor);
			result.ForegroundColor = ColorSerializer.ColorFromString(settings.ForegroundColor);
			result.Duration = settings.Duration;
			result.Height = settings.Height;
			result.UpdateInterval = settings.UpdateInterval;
			CreateLines(settings.Lines, result.Lines);
			return result;
		}
		private static void CreateLines(List<LineSettings0> settings, List<LineSettings> lines)
		{
			lines.Capacity = settings.Count;

			foreach (var element in settings)
			{
				LineSettings item = CreateLine(element);
				lines.Add(item);
			}
		}
		private static LineSettings CreateLine(LineSettings0 settings)
		{
			LineSettings result = new LineSettings();
			result.Volume = settings.Volume;
			result.BidColor = ColorSerializer.ColorFromString(settings.BidColor);
			result.AskColor = ColorSerializer.ColorFromString(settings.AskColor);
			return result;
		}
		#endregion
		#region properties
		public Settings0 Settings0 { get; set; }
		#endregion
		#region saving and loading
		internal static void Save(string path, Configuration config)
		{
			CreateDirectoriesFromPath(path);
			XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
			using (StreamWriter stream = new StreamWriter(path))
			{
				serializer.Serialize(stream, config);
			}
		}
		internal static Configuration Load(string path)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
			using (StreamReader stream = new StreamReader(path))
			{
				object obj = serializer.Deserialize(stream);
				Configuration result = (Configuration)obj;
				return result;
			}
		}
		private static void CreateDirectoriesFromPath(string path)
		{
			string directory = Path.GetDirectoryName(path);
			string[] items = directory.Split('\\');
			path = items[0] + "\\";
			int count = items.Length;
			for (int index = 1; index < count; ++index)
			{
				string item = items[index];
				path = Path.Combine(path, item);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
		}
		#endregion
	}
}

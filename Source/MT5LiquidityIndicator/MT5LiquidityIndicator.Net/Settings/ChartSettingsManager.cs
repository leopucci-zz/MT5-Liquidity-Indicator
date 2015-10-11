using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Settings
{
	internal class ChartSettingsManager
	{
		#region construction
		static ChartSettingsManager()
		{
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			m_path = Path.Combine(appData, @"MT5LiquidityIndicator\MataTrader4\{B45C85CC-961D-4667-95A1-491765FE2599}.xml");
			SafeLoad();
		}
		private static void SafeLoad()
		{
			try
			{
				Load();
			}
			catch (System.Exception)
			{
				m_symbolToSettings = new Dictionary<string, ChartSettings>();
			}
		}
		private static void Load()
		{
			Configuration config = Configuration.Load(m_path);
			m_symbolToSettings = config.ToSettings();
		}
		#endregion
		#region getting and setting methods
		internal static ChartSettings GetSettings(string symbol)
		{
			ChartSettings result = null;
			if (!m_symbolToSettings.TryGetValue(symbol, out result))
			{
				result = ChartSettings.MakeDefault();
			}
			return result;
		}
		internal static void Save(string symbol, ChartSettings newSettings)
		{
			m_symbolToSettings[symbol] = newSettings;
			Configuration config = new Configuration(m_symbolToSettings);
			Configuration.Save(m_path, config);
		}
		internal static void ResetToDefault(string symbol)
		{
			m_symbolToSettings.Remove(symbol);
			Configuration config = new Configuration(m_symbolToSettings);
			Configuration.Save(m_path, config);
		}
		#endregion
		#region members
		private static string m_path;
		private static Dictionary<string, ChartSettings> m_symbolToSettings = new Dictionary<string, ChartSettings>();
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT5LiquidityIndicator.Setup
{
	internal class MetaTrader4
	{
		#region construction
		internal MetaTrader4(string name, string root)
		{
			m_name = name;
			m_root = root;
		}

		internal static MetaTrader4[] GetAllInstances()
		{
			string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			root = Path.Combine(root, cTerminalRelativePath);
			string[] directories = Directory.GetDirectories(root);
			List<MetaTrader4> instances = new List<MetaTrader4>(directories.Length);

			foreach (var element in directories)
			{
				MetaTrader4 instance = TryToCreate(element);
				if (null != instance)
				{
					instances.Add(instance);
				}
			}

			MetaTrader4[] result = instances.ToArray();
			return result;
		}

		private static MetaTrader4 TryToCreate(string root)
		{
			try
			{
				string directoryName = Path.GetFileName(root);
				Match match = m_pattern.Match(directoryName);
				if (!match.Success)
				{
					return null;
				}
				string indicatorsPath = Path.Combine(root, cIndicatorsRelativePath);
				if (!Directory.Exists(indicatorsPath))
				{
					return null;
				}
				string librariesPath = Path.Combine(root, cLibrariesRelativePath);
				if (!Directory.Exists(librariesPath))
				{
					return null;
				}
				string originPath = Path.Combine(root, cOriginFileName);
				if (!File.Exists(originPath))
				{
					return null;
				}
				string name;
				using (StreamReader reader = new StreamReader(originPath))
				{
					name = reader.ReadLine();
				}
				if (string.IsNullOrWhiteSpace(name))
				{
					return null;
				}

				MetaTrader4 result = new MetaTrader4(name, root);
				return result;
			}
			catch (Exception)
			{
				return null;
			}
		}

		#endregion

		#region methods
		internal void TryToInstall()
		{
			try
			{
				Install();
				m_suffix = "installed";
			}
			catch (Exception ex)
			{
				m_suffix = ex;
			}
		}

		internal void Install()
		{
			Save(cLibrariesRelativePath, "MT5LiquidityIndicator.dll", Artifacts.MT5LiquidityIndicatorDllx64);
			Save(cLibrariesRelativePath, "MT5LiquidityIndicator.Net.dll", Artifacts.MT5LiquidityIndicatorNet);
			Save(cIndicatorsRelativePath, "MT5LiquidityIndicator.mq5", Artifacts.MT5LiquidityIndicatorMql);
			string path = Path.Combine(m_root, cIndicatorsRelativePath, "MT5LiquidityIndicator.ex5");
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		private void Save(string directory ,string name, byte[] data)
		{
			string path = Path.Combine(m_root, directory);
			Directory.CreateDirectory(path);
			path = Path.Combine(path, name);
			using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				stream.Write(data, 0, data.Length);
			}
		}

		public override string ToString()
		{
			if(null != m_suffix)
			{
				string result = string.Format("{0} - {1}", m_name, m_suffix);
				return result;
			}
			else
			{
				return m_name;
			}
		}
		#endregion

		#region properties
		public string Home
		{
			get
			{
				return m_root;
			}
		}
		[DisplayName("Indicators directory")]
		public string IndicatorDir
		{
			get
			{
				string result = Path.Combine(m_root, cIndicatorsRelativePath);
				return result;
			}
		}
		[DisplayName("Libraries directory")]
		public string LibraryDir
		{
			get
			{
				string result = Path.Combine(m_root, cLibrariesRelativePath);
				return result;
			}
		}
		[DisplayName("Terminal directory")]
		public string TerminalDIr
		{
			get
			{
				return m_name;
			}
		}
		#endregion

		#region members
		private readonly string m_name;
		private readonly string m_root;
		private object m_suffix;
		#endregion

		#region constants
		private const string cIndicatorsRelativePath = @"MQL5\Indicators";
		private const string cLibrariesRelativePath = @"MQL5\Libraries";
		private const string cTerminalRelativePath = @"MetaQuotes\Terminal";
		private const string cOriginFileName = "origin.txt";
		#endregion
		#region patterns
		private static readonly Regex m_pattern = new Regex(@"^[\da-fA-F]{32}$");
		#endregion
	}
}

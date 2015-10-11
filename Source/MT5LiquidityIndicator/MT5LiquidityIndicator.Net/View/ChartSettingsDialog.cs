using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MT5LiquidityIndicator.Net.Settings;

namespace MT5LiquidityIndicator.Net.View
{
	internal partial class ChartSettingsDialog : Form
	{
		internal ChartSettingsDialog(Chart chart)
		{
			if (null == chart)
			{
				throw new ArgumentNullException("chart");
			}
			InitializeComponent();

			this.Text += chart.Parameters.Symbol;
			m_chart = chart;
			m_propertyGrid.SelectedObject = chart.Settings;
			m_propertyGrid.PropertyValueChanged += OnChanged;
		}


		#region event handlers
		private void OnChanged(object sender, EventArgs e)
		{
			m_chart.ReloadSettings();
		}

		private void OnReset(object sender, EventArgs e)
		{
			m_chart.Settings.ResetToDefault();
			m_propertyGrid.Refresh();
			m_chart.ReloadSettings();
		}
		#endregion

		#region members
		private readonly Chart m_chart;
		#endregion
	}
}

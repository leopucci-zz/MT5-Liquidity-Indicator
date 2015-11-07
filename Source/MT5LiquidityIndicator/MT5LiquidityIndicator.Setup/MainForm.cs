using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT5LiquidityIndicator.Setup
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			try
			{
				MetaTrader4[] instances = MetaTrader4.GetAllInstances();
				foreach (var element in instances)
				{
					m_metaTraders.Items.Add(element);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void OnInstall(object sender, EventArgs e)
		{
			foreach (var element in m_metaTraders.CheckedItems)
			{
				MetaTrader4 trader = (MetaTrader4)element;
				Install(trader);
			}
		}

		private void Install(MetaTrader4 trader)
		{
			trader.TryToInstall();
			m_metaTraders.Refresh();
		}

		private void OnCheck(object sender, ItemCheckEventArgs e)
		{
			if(m_metaTraders.CheckedItems.Count > 1)
			{
				m_install.Enabled = true;
			}
			else if(CheckState.Checked == e.NewValue)
			{
				m_install.Enabled = true;
			}
			else
			{
				m_install.Enabled = false;
			}
		}

		private void OnSelected(object sender, EventArgs e)
		{
			int index = m_metaTraders.SelectedIndex;
			object obj = null;
			if (-1 != index)
			{
				obj = m_metaTraders.Items[index];
			}
			m_propertyGrid.SelectedObject = obj;
		}

		private void OnExtract(object sender, EventArgs e)
		{
			DialogResult result = m_dialog.ShowDialog();
			if (DialogResult.OK != result)
			{
				return;
			}

			try
			{
				string root = m_dialog.SelectedPath;
				MetaTrader4 MT5 = new MetaTrader4(root, root);
				MT5.TryToInstall();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}

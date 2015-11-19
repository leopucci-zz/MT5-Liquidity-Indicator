using MT5LiquidityIndicator.Net;
using MT5LiquidityIndicator.Net.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT5LiquidityIndicator.Launcher
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Parameters parameters = new Parameters(c_params);
			Chart chart = new Chart();
			chart.Dock = DockStyle.Fill;
			chart.Construct(parameters);
			this.Controls.Add(chart);
		}
		private const string c_params = "This=0|Symbol=Si-12.15|Period=16385|Digits=0|LotSize=1000|Func=0|Func2=0|";
}
}

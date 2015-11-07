using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MT5LiquidityIndicator.Net.Core;
using MT5LiquidityIndicator.Net.Settings;
using System.Drawing.Drawing2D;
using System.IO;
using MT5LiquidityIndicator.Net.MQL5;

namespace MT5LiquidityIndicator.Net.View
{
	internal partial class Chart : UserControl
	{
		#region construction
		public Chart()
		{
			InitializeComponent();
			m_spreads.Visible = true;
			m_spreads.Location = Point.Empty;
			this.Text = string.Empty;
		}
		public void Construct(Parameters parameters)
		{
			m_parameters = parameters;
			m_realTimeState = new ChartState(m_parameters, m_realTimeQuotes);

			m_currentState = m_realTimeState;
			m_settings = ChartSettingsManager.GetSettings(parameters.Symbol);
			m_realTimeQuotes.Interval = m_settings.Duration;

			m_parameters.SetHeight(m_settings.Height);
			m_spreads.Height = (int)(m_settings.Height - cTopOffset - cBottomOffset);
			m_timer.Interval = m_settings.UpdateInterval;
			m_proxy = new DataFeed(parameters.This, parameters.Func2);
			m_proxy.Tick += OnTick;
		}
		#endregion
		#region event handlers
		private void OnClick(object sender, EventArgs e)
		{
			m_spreads.Deselect();
		}
		private void OnMove(object sender, MouseEventArgs e)
		{
			if (MouseButtons.Left == e.Button)
			{
				if (!m_disableMouseMove)
				{
					m_disableMouseMove = true;
					m_spreads.Location = e.Location;
					m_disableMouseMove = false;
					this.Invalidate();
				}
			}
		}
		private void OnTick(object sender, TickEventArgs e)
		{
			m_realTimeQuotes.Add(e.Tick);
		}
		private void OnTick(object sender, EventArgs e)
		{
			m_realTimeQuotes.Refresh();
			this.Invalidate();
			m_spreads.Update(m_parameters, m_settings, m_realTimeQuotes);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw(e.Graphics);
		}
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
		protected override void WndProc(ref Message m)
		{
			if (WM_DESTROY == m.Msg)
			{
				Destroy();
			}
			base.WndProc(ref m);
		}
		private void Destroy()
		{
			DataFeed proxy = m_proxy;
			m_proxy = null;
			if (null != proxy)
			{
				proxy.Tick -= OnTick;
				proxy.Dispose();
			}
		}
		private void OnOptions(object sender, EventArgs e)
		{
			try
			{
				ChartSettings oldSettings = new ChartSettings(m_settings);
				ChartSettingsDialog dialog = new ChartSettingsDialog(this);
				DialogResult result = dialog.ShowDialog();
				if (DialogResult.Cancel == result)
				{
					m_settings = oldSettings;
				}
				else if (DialogResult.Yes == result)
				{
					ChartSettingsManager.Save(m_parameters.Symbol, m_settings);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void OnResetPricesWindowPosition(object sender, EventArgs e)
		{
			m_spreads.Location = Point.Empty;
		}
		private void OnSaveAsCSV(object sender, EventArgs e)
		{
			DialogResult result = m_saveFileDialog.ShowDialog();
			if (DialogResult.OK != result)
			{
				return;
			}
			try
			{
				string path = m_saveFileDialog.FileName;

				List<double> volumes = new List<double>();
				foreach (var element in m_settings.Lines)
				{
					volumes.Add(element.Volume);
				}

				Quotes quotes = m_currentState.Quotes;

				string text = CsvBuilder.Format(m_parameters.LotSize, m_parameters.RoundingStepOfPrice, volumes, quotes);
				using (StreamWriter stream = new StreamWriter(path))
				{
					stream.Write(text);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
		#region properties
		internal ChartSettings Settings
		{
			get
			{
				return m_settings;
			}
		}
		internal Parameters Parameters
		{
			get
			{
				return m_parameters;
			}
		}
		#endregion
		#region methods
		internal void ReloadSettings()
		{
			try
			{
				m_realTimeQuotes.Interval = m_settings.Duration;
				m_timer.Interval = m_settings.UpdateInterval;
				m_parameters.SetHeight(m_settings.Height);
				m_realTimeQuotes.Refresh();
				m_spreads.Update(m_parameters, m_settings, m_realTimeQuotes);
				this.Invalidate();
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
		#region prepare to draw
		private void Draw(Graphics g)
		{
			int width = this.Width;
			int height = this.Height;
			using (Bitmap image = new Bitmap(width, height, g))
			{
				using (Graphics g2 = Graphics.FromImage(image))
				{
					GraphicsEx gEx = new GraphicsEx(g2);
					gEx.FillRectangle(m_settings.BackgroundColor, 0, 0, width, height);
					gEx.SetRenderingMode(m_settings.Mode);
					DoDraw(gEx);
					g.DrawImage(image, 0, 0);
				}
			}
		}
		private void DoDraw(GraphicsEx g)
		{
			if (!m_currentState.Empty)
			{
				DoDrawData(g);
			}
			else
			{
				DoDrawEmpty(g);
			}
		}
		private void DoDrawEmpty(GraphicsEx g)
		{
			SizeF size = g.MeasureString(cOffQuotes, this.Font);
			float w = (float)this.Width;
			float h = (float)this.Height;

			float x = (w - size.Width) / 2;
			float y = (h - size.Height) / 2;
			g.DrawString(cOffQuotes, this.Font, m_settings.ForegroundColor, x, y);
		}
		#endregion

		#region data drawing
		private void DoDrawData(GraphicsEx g)
		{
			m_currentState.Refresh(m_settings);
			RectF area = m_currentState.PhysicalArea;

			float w = (float)this.Width - cLeftOffset - cRightOffset;
			float h = (float)this.Height - cBottomOffset - cTopOffset;

			float kx = w / area.Width;
			float ky = h / area.Height;

			RectF physical = area;

			RectF logical = new RectF(cLeftOffset, cTopOffset, cLeftOffset + w, cTopOffset + h);

			g.Physical = physical;
			g.Logical = logical;

			int count = m_currentState.Graphs.Count;
			for (int index = 0; index < count; ++index)
			{
				Graph graph = m_currentState.Graphs[index];
				Color bidColor = m_settings.Lines[index].BidColor;
				Color askColor = m_settings.Lines[index].AskColor;
				DrawLine(g, graph, bidColor, askColor);
			}

			using (Pen pen = new Pen(m_settings.ForegroundColor))
			{
				using (Brush brush = new SolidBrush(m_settings.ForegroundColor))
				{
					DoDrawAxes(g, pen, brush, area);
				}
				if (m_settings.Grid)
				{
					pen.DashPattern = new float[] { 10, 10 };
					pen.Width = 0.5F;
					DoDrawGrid(g, pen, area);
				}
			}

		}
		private void DrawLine(GraphicsEx g, Graph graph, Color bidColor, Color askColor)
		{
			LineType type = m_settings.Type;
			if (LineType.Straight == type)
			{
				graph.DrawStraight(g, bidColor, askColor);
			}
			else if (LineType.Stepped == type)
			{
				graph.DrawStepped(g, bidColor, askColor);
			}
			else if (LineType.Interpolation == type)
			{
				graph.DrawInterpolation(g, bidColor, askColor);
			}
		}
		private void DoDrawAxes(GraphicsEx g, Pen pen, Brush brush, RectF area)
		{
			float w = (float)this.Width;
			float h = (float)this.Height;


			g.DrawLine(pen, 0, h - cBottomOffset, w, h - cBottomOffset);
			g.DrawLine(pen, w - cArrowOffset, h - cBottomOffset - cArrowOffset / 2, w, h - cBottomOffset);
			g.DrawLine(pen, w - cArrowOffset, h - cBottomOffset + cArrowOffset / 2, w, h - cBottomOffset);

			foreach(var element in m_currentState.XPhysicalDashes)
			{
				float x = g.TransformX(element.X);
				float y = g.TransformY(element.Y);

				g.DrawLine(pen, x, y - cArrowSize, x, y + cArrowSize);
				g.DrawString(element.X.ToString(), this.Font, brush, x - cLeftOffset, y + cBottomOffset / 3);
			}

			g.DrawLine(pen, w - cRightOffset, 0, w - cRightOffset, h);
			g.DrawLine(pen, w - cRightOffset - cArrowOffset / 2, cArrowOffset, w - cRightOffset, 0);
			g.DrawLine(pen, w - cRightOffset + cArrowOffset / 2, cArrowOffset, w - cRightOffset, 0);


			
			foreach (var element in m_currentState.YPhysicalDashes)
			{
				float x = g.TransformX(element.X);
				float y = g.TransformY(element.Y);

				g.DrawLine(pen, x - cArrowSize, y, x + cArrowSize, y);
				g.DrawString(element.Y.ToString(m_parameters.PriceFormat), this.Font, brush, x, y + +cBottomOffset / 3);
			}
		}
		private void DoDrawGrid(GraphicsEx g, Pen pen, RectF area)
		{
			foreach (var element in m_currentState.XPhysicalDashes)
			{
				float x = g.TransformX(element.X);
				float y = g.TransformY(element.Y);

				g.DrawLine(pen, x, y, x, 0);
			}

			foreach (var element in m_currentState.YPhysicalDashes)
			{
				float x = g.TransformX(element.X);
				float y = g.TransformY(element.Y);

				g.DrawLine(pen, 0, y, x, y);
			}
		}

		#endregion
		#region members
		private bool m_disableMouseMove;
		private DataFeed m_proxy;
		private Parameters m_parameters;
		private ChartSettings m_settings;
		private readonly Quotes m_realTimeQuotes = new Quotes(60);
		private ChartState m_currentState;
		private ChartState m_realTimeState;
		private ChartState m_historyState;
		private const int WM_DESTROY = 0x0002;
		private const string cOffQuotes = "Off quotes";
		private const float cLeftOffset = 10;
		private const float cRightOffset = 50;
		private const float cBottomOffset = 20;
		private const float cTopOffset = 10;
		private const float cArrowOffset = 5;
		private const float cArrowSize = 2.5F;
		private static readonly float[] cDashPattern = new float[] { 10, 10};
		#endregion
	}
}

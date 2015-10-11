using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MT5LiquidityIndicator.Net.Core;
using MT5LiquidityIndicator.Net.Settings;
using System.Globalization;

namespace MT5LiquidityIndicator.Net.View
{
	public partial class Prices : UserControl
	{
		public Prices()
		{
			InitializeComponent();
		}
		internal void Deselect()
		{
			m_grid.ClearSelection();
		}
		private static Color CalculateColor(Color bidColor, Color askColor, Color foregroundColor)
		{
			if (bidColor == askColor)
			{
				return bidColor;
			}
			return foregroundColor;
		}
		internal void Update(Parameters parameters, ChartSettings settings, Quotes quotes)
		{
			if (m_grid.BackgroundColor != settings.BackgroundColor)
			{
				m_grid.BackgroundColor = settings.BackgroundColor;
			}
			Quote quote = quotes.Last.Quote;

			int count = (null == quote) ? 0 : settings.Lines.Count;
			Update(count);


			for (int index = 0; index < count; ++index )
			{
				LineSettings line = settings.Lines[index];
				Color volumeColor = CalculateColor(line.BidColor, line.AskColor, settings.ForegroundColor);
				Update(settings, index, 0, (float)line.Volume, volumeColor);

				double volume = line.Volume * parameters.LotSize;
				float? bid = MathEx.CalculateWAVP(volume, quote.Bids);
				float? ask = MathEx.CalculateWAVP(volume, quote.Asks);
				if (bid.HasValue)
				{
					bid = (float)MathEx.RoundDown(bid.Value, parameters.PricePip);
				}
				if (ask.HasValue)
				{
					ask = (float)MathEx.RoundUp(ask.Value, parameters.PricePip);
				}
				Update(settings, index, 1, bid, line.BidColor);
				Update(settings, index, 2, ask, line.AskColor);

				float? spread = null;
				if (bid.HasValue && ask.HasValue)
				{
					spread = (float)Math.Round(parameters.PriceFactor * (ask.Value - bid.Value));
				}
				Color spreadColor = CalculateColor(line.BidColor, line.AskColor, settings.ForegroundColor);
				Update(settings, index, 3, spread, spreadColor);
			}
			Size size = m_grid.PreferredSize;
			if (this.Size != size)
			{
				this.Size = size;
			}
			//if (!this.Visible)
			//{
			//	this.Visible = true;
			//}
		}
		private void Update(int count)
		{
			for (; count > m_grid.Rows.Count; )
			{
				int index = m_grid.Rows.Count;
				m_grid.Rows.Insert(index, string.Empty, string.Empty, string.Empty, string.Empty);
			}
			for (; m_grid.Rows.Count > count; )
			{
				int index = m_grid.Rows.Count;
				m_grid.Rows.RemoveAt(index - 1);
			}
		}
		private void Update(ChartSettings settings, int row, int column, float? value, Color color)
		{
			DataGridViewCell cell = m_grid.Rows[row].Cells[column];

			if (settings.BackgroundColor != cell.Style.BackColor)
			{
				cell.Style.BackColor = settings.BackgroundColor;
			}
			LineSettings line = settings.Lines[row];
			if (cell.Style.ForeColor != color)
			{
				cell.Style.ForeColor = color;
			}
			string st = cell.Value as string;
			string text = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : cNA;
			if (st != text)
			{
				cell.Value = text;
			}
		}
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			m_location = e.Location;
		}
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (MouseButtons.Left == e.Button)
			{
				Point location = this.Location;
				location.X += (e.X - m_location.X);
				location.Y += (e.Y - m_location.Y);
				this.Location = location;
			}
		}
		#region members
		private Point m_location;
		private const string cNA = "N/A";
		#endregion
	}
}

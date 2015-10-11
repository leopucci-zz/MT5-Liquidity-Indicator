using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MT5LiquidityIndicator.Net.Core;
using MT5LiquidityIndicator.Net.Settings;

namespace MT5LiquidityIndicator.Net.View
{
	internal class ChartState
	{
		internal ChartState(Parameters parameters, Quotes quotes)
		{
			m_parameters = parameters;
			m_quotes = quotes;
			this.Graphs = new List<Graph>();
			this.XPhysicalDashes = new List<PointF>();
			this.YPhysicalDashes = new List<PointF>();
		}
		#region properties
		internal List<Graph> Graphs { get; private set; }
		internal RectF PhysicalArea { get; private set; }
		internal List<PointF> XPhysicalDashes { get; private set; }
		internal List<PointF> YPhysicalDashes { get; private set; }
		internal Quotes Quotes
		{
			get
			{
				return m_quotes;
			}
		}
		internal bool Empty
		{
			get
			{
				return m_quotes.Empty;
			}
		}
		#endregion

		#region methods
		internal void Refresh(ChartSettings settings)
		{
			CalculateGraphs(settings);
			CalculateArea();
			CalcualteXPhysicalDashes();
			CalcualteYPhysicalDashes();
		}
		private void CalculateGraphs(ChartSettings settings)
		{
			Graphs.Clear();
			foreach (var element in settings.Lines)
			{
				double volume = element.Volume * m_parameters.LotSize;

				Graph graph = new Graph(volume, m_quotes);
				Graphs.Add(graph);
			}
		}
		private void CalculateArea()
		{
			RectF area = new RectF();
			area.MinX = -(float)m_quotes.Interval;

			float yMin = float.MaxValue;
			float yMax = float.MinValue;

			QuoteEx last = m_quotes.Last;
			if (null != last.Quote)
			{
				yMin = (float)Math.Min(last.Quote.Bid, last.Quote.Ask);
				yMax = (float)Math.Max(last.Quote.Bid, last.Quote.Ask);
			}

			foreach (var element in m_quotes.Items)
			{
				float bid = (float)element.Quote.Bid;
				float ask = (float)element.Quote.Ask;

				if (bid < yMin)
				{
					yMin = bid;
				}
				if (bid > yMax)
				{
					yMax = bid;
				}

				if (ask < yMin)
				{
					yMin = ask;
				}
				if (ask > yMax)
				{
					yMax = ask;
				}
			}

			foreach (var element in Graphs)
			{
				float? min = element.CalculateMinimum();
				float? max = element.CalculateMaximum();

				if (min.HasValue)
				{
					yMin = Math.Min(yMin, min.Value);
				}

				if (max.HasValue)
				{
					yMax = Math.Max(yMax, max.Value);
				}
			}

			area.MinY = (float)MathEx.RoundDown(yMin, m_parameters.RoundingStepOfPrice);
			area.MaxY = (float)MathEx.RoundUp(yMax, m_parameters.RoundingStepOfPrice);

			this.PhysicalArea = area;
		}
		private void CalcualteXPhysicalDashes()
		{
			RectF area = this.PhysicalArea;
			XPhysicalDashes.Clear();
			float? previous = null;
			for (int index = 0; index <= cNumberOfXDashes; ++index)
			{
				float value = (float)Math.Round(area.MinX + area.Width * index / cNumberOfXDashes);
				if (!previous.HasValue || (previous.Value != value))
				{
					previous = value;
					XPhysicalDashes.Add(new PointF(value, area.MinY));
				}
			}
		}
		private void CalcualteYPhysicalDashes()
		{
			RectF area = this.PhysicalArea;
			YPhysicalDashes.Clear();
			float? previous = null;
			for (int index = 0; index <= cNumberOfYDashes; ++index)
			{
				float value = (float)MathEx.Round(area.MinY + area.Height * index / cNumberOfYDashes, m_parameters.PricePip);
				if (!previous.HasValue || (previous.Value != value))
				{
					previous = value;
					YPhysicalDashes.Add(new PointF(area.MaxX, value));
				}
			}
		}
		#endregion


		#region members
		private readonly Parameters m_parameters;
		private readonly Quotes m_quotes;
		private const int cNumberOfYDashes = 5;
		private const int cNumberOfXDashes = 10;
		#endregion
	}
}

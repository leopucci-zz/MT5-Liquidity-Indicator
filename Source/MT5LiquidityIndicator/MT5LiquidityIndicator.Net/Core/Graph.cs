using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class Graph
	{
		#region construction
		internal Graph(double requiredVolume, Quotes quotes)
		{
			m_bids = GraphPart.CreateBids(requiredVolume, quotes);
			m_asks = GraphPart.CreateAsks(requiredVolume, quotes);
		}
		#endregion
		#region internal methods
		internal float? CalculateMinimum()
		{
			float? bid = m_bids.CalculateMinimum();
			float? ask = m_asks.CalculateMinimum();
			if (bid.HasValue && ask.HasValue)
			{
				return Math.Min(bid.Value, ask.Value);
			}
			if (bid.HasValue)
			{
				return bid.Value;
			}
			if (ask.HasValue)
			{
				return ask.Value;
			}
			return null;
		}
		internal float? CalculateMaximum()
		{
			float? bid = m_bids.CalculateMaximum();
			float? ask = m_asks.CalculateMaximum();
			if (bid.HasValue && ask.HasValue)
			{
				return Math.Max(bid.Value, ask.Value);
			}
			if (bid.HasValue)
			{
				return bid.Value;
			}
			if (ask.HasValue)
			{
				return ask.Value;
			}
			return null;
		}
		internal void DrawInterpolation(GraphicsEx g, Color bidColor, Color askColor)
		{
			using (Pen pen = new Pen(bidColor, 1))
			{
				m_bids.DrawInterpolation(g, pen);
			}
			using (Pen pen = new Pen(askColor, 1))
			{
				m_asks.DrawInterpolation(g, pen);
			}
		}
		internal void DrawStraight(GraphicsEx g, Color bidColor, Color askColor)
		{
			using (Pen pen = new Pen(bidColor, 1))
			{
				m_bids.DrawStraight(g, pen);
			}
			using (Pen pen = new Pen(askColor, 1))
			{
				m_asks.DrawStraight(g, pen);
			}
		}
		internal void DrawStepped(GraphicsEx g, Color bidColor, Color askColor)
		{
			using (Pen pen = new Pen(bidColor, 1))
			{
				m_bids.DrawStepped(g, pen);
			}
			using (Pen pen = new Pen(askColor, 1))
			{
				m_asks.DrawStepped(g, pen);
			}
		}
		#endregion
		#region members
		private readonly GraphPart m_bids;
		private readonly GraphPart m_asks;
		#endregion
	}
}

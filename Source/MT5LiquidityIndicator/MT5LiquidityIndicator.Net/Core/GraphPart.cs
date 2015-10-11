using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class GraphPart
	{
		#region construction
		private GraphPart(Quotes quotes)
		{
			m_points.Capacity = 1 + quotes.Items.Count;
		}
		internal static GraphPart CreateBids(double requiredVolume, Quotes quotes)
		{
			GraphPart result = new GraphPart(quotes);
			List<Point2F> points = result.m_points;
			long now = quotes.Time;

			foreach (var element in quotes.Items)
			{
				Point2F point = new Point2F();
				point.X = (element.Time - now) / 1000.0F;
				point.Y = MathEx.CalculateWAVP(requiredVolume, element.Quote.Bids);
				points.Add(point);
			}

			if (0 == quotes.Items.Count)
			{
				quotes.Items.Clear();
			}

			QuoteEx quote = quotes.Last;
			if (null != quote.Quote)
			{
				Point2F point = new Point2F();
				point.X = (quote.Time - now) / 1000.0F;
				if (point.X < - quotes.Interval)
				{
					point.X = -(float)quotes.Interval;
				}
				point.Y = MathEx.CalculateWAVP(requiredVolume, quote.Quote.Bids);
				points.Add(point);
			}
			return result;
		}
		internal static GraphPart CreateAsks(double requiredVolume, Quotes quotes)
		{
			GraphPart result = new GraphPart(quotes);
			List<Point2F> points = result.m_points;
			long now = quotes.Time;

			foreach (var element in quotes.Items)
			{
				Point2F point = new Point2F();
				point.X = (element.Time - now) / 1000.0F;
				point.Y = MathEx.CalculateWAVP(requiredVolume, element.Quote.Asks);
				points.Add(point);
			}

			QuoteEx quote = quotes.Last;
			if (null != quote.Quote)
			{
				Point2F point = new Point2F();
				point.X = (quote.Time - now) / 1000.0F;
				if (point.X < - quotes.Interval)
				{
					point.X = -(float)quotes.Interval;
				}
				point.Y = MathEx.CalculateWAVP(requiredVolume, quote.Quote.Asks);
				points.Add(point);
			}
			return result;
		}
		#endregion
		#region area methods
		internal float? CalculateMinimum()
		{
			var it = m_points.GetEnumerator();

			float? result = null;

			for (; it.MoveNext(); )
			{
				if (it.Current.Y.HasValue)
				{
					result = it.Current.Y;
					break;
				}
			}

			if (!result.HasValue)
			{
				return null;
			}

			float value = result.Value;

			for (; it.MoveNext(); )
			{
				if (it.Current.Y.HasValue)
				{
					float y = it.Current.Y.Value;
					if (y < value)
					{
						value = y;
					}
				}
			}

			result = value;
			return result;
		}
		internal float? CalculateMaximum()
		{
			var it = m_points.GetEnumerator();

			float? result = null;

			for (; it.MoveNext(); )
			{
				if (it.Current.Y.HasValue)
				{
					result = it.Current.Y;
					break;
				}
			}

			if (!result.HasValue)
			{
				return null;
			}

			float value = result.Value;

			for (; it.MoveNext(); )
			{
				if (it.Current.Y.HasValue)
				{
					float y = it.Current.Y.Value;
					if (y > value)
					{
						value = y;
					}
				}
			}

			result = value;
			return result;
		}
		#endregion
		#region drawing methods
		internal void DrawInterpolation(GraphicsEx g, Pen pen)
		{
			var it = m_points.GetEnumerator();
			it.MoveNext();
			Point2F previous = it.Current;

			for (; it.MoveNext(); )
			{
				Point2F next = it.Current;
				g.DrawLineInterpolation(pen, previous, next);
				previous = next;
			}
		}
		internal void DrawStraight(GraphicsEx g, Pen pen)
		{
			var it = m_points.GetEnumerator();
			it.MoveNext();
			Point2F previous = it.Current;

			for (; it.MoveNext(); )
			{
				Point2F next = it.Current;
				g.DrawLineStraight(pen, previous, next);
				previous = next;
			}
			if (m_points.Count > 0)
			{
				Point2F from = m_points.Last();
				Point2F to = new Point2F(0, from.Y);
				g.DrawLineStraight(pen, from, to);
			}
		}
		internal void DrawStepped(GraphicsEx g, Pen pen)
		{
			var it = m_points.GetEnumerator();
			it.MoveNext();
			Point2F previous = it.Current;

			for (; it.MoveNext(); )
			{
				Point2F next = it.Current;
				g.DrawLineStepped(pen, previous, next);
				previous = next;
			}
			if (m_points.Count > 0)
			{
				Point2F from = m_points.Last();
				Point2F to = new Point2F(0, from.Y);
				g.DrawLineStepped(pen, from, to);
			}
		}
		#endregion
		#region members
		private readonly List<Point2F> m_points = new List<Point2F>();
		#endregion
	}
}

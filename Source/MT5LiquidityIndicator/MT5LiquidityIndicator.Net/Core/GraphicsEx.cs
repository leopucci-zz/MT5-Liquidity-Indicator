using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class GraphicsEx
	{
		#region construction
		internal GraphicsEx(Graphics graphics = null)
		{
			m_graphics = graphics;
			Construct();
		}
		private void Construct()
		{
			m_kx = m_logical.Width / m_physical.Width;
			m_ky = -m_logical.Height / m_physical.Height;

			m_ax = m_logical.MinX - m_kx * m_physical.MinX;
			m_ay = m_logical.MaxY - m_ky * m_physical.MinY;
		}
		#endregion
		#region properties
		internal RectF Physical
		{
			get
			{
				return m_physical;
			}
			set
			{
				m_physical = value;
				Construct();
			}
		}
		internal RectF Logical
		{
			get
			{
				return m_logical;
			}
			set
			{
				m_logical = value;
				Construct();
			}
		}
		internal Graphics Graphics
		{
			get
			{
				return m_graphics;
			}
		}
		#endregion
		#region options methods
		internal void SetRenderingMode(RenderingMode mode)
		{
			if (RenderingMode.Speed == mode)
			{
				this.m_graphics.SmoothingMode = SmoothingMode.HighSpeed;
			}
			else if (RenderingMode.Quality == mode)
			{
				this.m_graphics.SmoothingMode = SmoothingMode.HighQuality;
			}
			else
			{
				string message = string.Format("Invalid rendering mode = {0}", mode);
				throw new ArgumentException(message, "mode");
			}
		}

		#endregion
		#region transform methods
		internal double TransformX(double x)
		{
			double result = m_kx * x + m_ax;
			return result;
		}
		internal double TransformY(double y)
		{
			double result = m_ky * y + m_ay;
			return result;
		}
		internal float TransformX(float x)
		{
			float result = (float)(m_kx * x + m_ax);
			return result;
		}
		internal float TransformY(float y)
		{
			float result = (float)(m_ky * y + m_ay);
			return result;
		}
		#endregion
		#region drawing methods
		internal void FillRectangle(Color color, float x, float y, float width, float height)
		{
			using(Brush brush = new SolidBrush(color))
			{
				m_graphics.FillRectangle(brush, x, y, width, height);
			}
		}
		internal void DrawString(string text, Font font, Color color, float x, float y)
		{
			using (Brush brush = new SolidBrush(color))
			{
				m_graphics.DrawString(text, font, brush, x, y);
			}
		}
		internal void DrawString(string text, Font font, Brush brush, float x, float y)
		{
			m_graphics.DrawString(text, font, brush, x, y);
		}
		internal SizeF MeasureString(string text, Font font)
		{
			return m_graphics.MeasureString(text, font);
		}
		internal void DrawLineInterpolation(Pen pen, Point2F from, Point2F to)
		{
			if (from.Y.HasValue && to.Y.HasValue)
			{
				float xf = TransformX(from.X);
				float yf = TransformY(from.Y.Value);
				float xt = TransformX(to.X);
				float yt = TransformY(to.Y.Value);

				m_graphics.DrawLine(pen, xf, yf, xt, yt);
			}
		}
		internal void DrawLineStepped(Pen pen, Point2F from, Point2F to)
		{
			if (from.Y.HasValue)
			{
				float xf = TransformX(from.X);
				float yf = TransformY(from.Y.Value);
				float xt = TransformX(to.X);

				m_graphics.DrawLine(pen, xf, yf, xt, yf);

				if (to.Y.HasValue)
				{
					float yt = TransformY(to.Y.Value);
					m_graphics.DrawLine(pen, xt, yf, xt, yt);
				}
			}
		}
		internal void DrawLineStraight(Pen pen, Point2F from, Point2F to)
		{
			if (from.Y.HasValue)
			{
				float xf = TransformX(from.X);
				float yf = TransformY(from.Y.Value);
				float xt = TransformX(to.X);

				m_graphics.DrawLine(pen, xf, yf, xt, yf);
			}
		}
		internal void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			m_graphics.DrawLine(pen, x1, y1, x2, y2);
		}
		#endregion
		#region members
		private double m_kx;
		private double m_ax;
		private double m_ky;
		private double m_ay;
		private RectF m_logical = new RectF(0, 0, 1, 1);
		private RectF m_physical = new RectF(0, 0, 1, 1);
		private readonly Graphics m_graphics;
		#endregion
	}
}

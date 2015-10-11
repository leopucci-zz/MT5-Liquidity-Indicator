using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MT5LiquidityIndicator.Net.Core
{
	internal class CsvBuilder
	{
		internal static string Format(double lotSize,double roundingStepOfPrice, IEnumerable<double> volumes, Quotes quotes)
		{
			StringBuilder builder = new StringBuilder();
			WriteHeader(volumes, builder);

			foreach (var element in quotes.Items)
			{
				WriteQuote(lotSize, roundingStepOfPrice, volumes, element, quotes.Time, builder);
			}

			string result = builder.ToString();
			return result;
		}

		private static void WriteHeader(IEnumerable<double> volumes, StringBuilder builder)
		{
			builder.Append("LocalTimeOffset,CreationDateTime");
			foreach (var element in volumes)
			{
				builder.AppendFormat(",Bid_{0},Ask_{0}", element);
			}
			builder.AppendLine();
		}

		private static void WriteQuote(double lotSize, double roundingStepOfPrice, IEnumerable<double> volumes, QuoteEx quote, long start, StringBuilder builder)
		{
			double delta = (quote.Time - start) / 1000.0;
			builder.Append(delta);
			builder.AppendFormat(",{0}", quote.Quote.CreatingTime.ToString("yyyy-MM-ddTHH:mm:ss.fff"));

			foreach(var element in volumes)
			{
				float? bid = MathEx.CalculateWAVP(element * lotSize, quote.Quote.Bids);
				float? ask = MathEx.CalculateWAVP(element * lotSize, quote.Quote.Asks);

				if (bid.HasValue)
				{
					bid = (float)MathEx.RoundDown(bid.Value, roundingStepOfPrice);
				}

				if (ask.HasValue)
				{
					ask = (float)MathEx.RoundUp(ask.Value, roundingStepOfPrice);
				}

				builder.AppendFormat(",{0},{1}", bid, ask);
			}

			builder.AppendLine();
		}
	}
}

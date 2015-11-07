#property link "https://github.com/marmysh/MT5-liquidity-indicator"
#property description "MT5 Liquidity Indicator"
#property description "Authors:"
#property description "\tDmitry Kogan email: disayev@hotmail.com"
#property description "\tViktar Marmysh email: marmysh@gmail.com"

#property indicator_chart_window

#import "MT5LiquidityIndicator.dll"
void Debug(string st);
long MT5LIStart(int hwnd, string symbol, int period, int digits, double lotSize);
void MT5LIStop(int hwnd);

void Level2_Begin(long pChart);
void Level2_End(long pChart);
void Level2_AddBid(long pChart, double price, double size);
void Level2_AddAsk(long pChart, double price, double size);

#import

long gHwnd = 0;
long gChart = 0;


long GetCurrentChartId()
{
   ENUM_TIMEFRAMES tf = Period();
   string symbol = Symbol();
   for(long chart = ChartFirst(); chart >= 0; chart = ChartNext(chart))
   {
      string chartSymbol = ChartSymbol(chart);
      ENUM_TIMEFRAMES chartTf = ChartPeriod(chart);
      if((chartSymbol == symbol) && (chartTf == tf))
      {
         return chart;
      }  
   }
   Print("GetCurrentChartId(): couldn't find the current chart ID");
   return (0);
}

long GetCurrentChartHandle()
{
   long handle = 0;
   long id = GetCurrentChartId();
   if(ChartGetInteger(id, CHART_WINDOW_HANDLE, 0, handle))
   {
      return handle;
   }
   else
   {
      Print("GetCurrentChartHandle(): couldn't find the current chart handle");
      return 0;
   }
}

int OnInit()
{
   string symbol = Symbol();
   gHwnd = GetCurrentChartHandle();
   
   Print("OnInit(): symbol = ", symbol);
   int period = Period();
   double lotSize = SymbolInfoDouble(symbol,SYMBOL_TRADE_CONTRACT_SIZE);
   int digits = SymbolInfoInteger(symbol,SYMBOL_DIGITS);
   gChart = MT5LIStart(gHwnd, symbol, period, digits, lotSize);
   if(MarketBookAdd(symbol))
   {
      Print("MarketBookAdd() - 0");
   }  
   else
   {
      Print("MarketBookAdd(): GetLastError() = ", GetLastError());
   }
   return 0;  
}

void OnDeinit(const int reason)
{
   Debug("OnDeinit");
   string symbol = Symbol();
   MarketBookRelease(symbol);
   MT5LIStop(gHwnd);
}

void OnBookEvent(const string& symbol)
{
   Level2_Begin(gChart);
   MqlBookInfo level2[];
   if(MarketBookGet(NULL, level2))
   {
      int size = ArraySize(level2);
      for(int i = 0; i < size; i++)
      {
         MqlBookInfo info = level2[i];
         if(BOOK_TYPE_BUY == info.type)
         {
            Level2_AddBid(gChart, info.price, info.volume);
         }
         else if(BOOK_TYPE_SELL == info.type)
         {
            Level2_AddAsk(gChart, info.price, info.volume);
         }      
      }
   }
   Level2_End(gChart);
}

int OnCalculate(const int rates_total,
                const int prev_calculated,
                const datetime &time[],
                const double &open[],
                const double &high[],
                const double &low[],
                const double &close[],
                const long &tick_volume[],
                const long &volume[],
                const int &spread[])
{

  
   return(0);
}


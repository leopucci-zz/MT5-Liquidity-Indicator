//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================
#include "stdafx.h"
#include "Chart.h"
#include "Stream.h"
#include "Parameters.h"
#include "DotNetBridge.h"
#include "DllMain.h"


namespace
{
	const UINT cServiceMessage = RegisterWindowMessage(TEXT("{C940DF81-863D-4FFB-9504-681684775B47}"));

	const WPARAM cCreate = 0;
	const WPARAM cUpdate = 1;
	const WPARAM cUpdateHeight = 2;
	const WPARAM cDestroy = 3;

	string cTypeName = "MT5LiquidityIndicator.Net.ChartBuilder";
	string cMethodName = "Run";
	const int cDefaultChartHeight = 250;
}
namespace
{
	CComAutoCriticalSection gSynchronizer;
	map<HWND, CChart> gCharts;
}

LRESULT CALLBACK ChartProcHandler(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	auto it = gCharts.find(hwnd);
	if (gCharts.end() != it)
	{
		CChart& chart = it->second;
		return chart.Handle(hwnd, uMsg, wParam, lParam);
	}
	return 0;
}

CChart::CChart() : m_chartHeight(cDefaultChartHeight), m_isSpecial(false), m_wasSent(false), m_width(), m_height(), m_indicator(), m_original()
{
}

CChart::~CChart()
{
	m_queue.Join();
}

void CChart::Construct(HWND hwnd, const string& symbol, int period, int digits, double lotSize)
{
	m_hwnd = hwnd;
	m_symbol = symbol;
	m_period = period;
	m_digits = digits;
	m_lotSize = lotSize;

	CStream() << "CChart::Construct() - 0 = " << GetCurrentThreadId() >> DebugLog;

	m_original = reinterpret_cast<WNDPROC>(SetWindowLongPtr(hwnd, GWLP_WNDPROC, reinterpret_cast<LONG_PTR>(ChartProcHandler)));
	SendMessage(hwnd, cServiceMessage, cCreate, 0);
	CStream() << "CChart::Construct() - 1 = " << GetCurrentThreadId() >> DebugLog;
}

void CChart::DoConstruct()
{
	CStream() << "CChart::DoConstruct() - 0 = " << GetCurrentThreadId() >> DebugLog;

	string argument = FormatParameters(this, m_symbol, m_period, m_digits,m_lotSize, reinterpret_cast<void*>(&CChart::SetHeight));
	CStream() << argument >> DebugLog;
	CDotNetBridge bridge;
	m_indicator = reinterpret_cast<HWND>(bridge.Execute(DotNetDllPath(), cTypeName, cMethodName, argument));
	if (m_indicator > 0)
	{
		HWND parent = GetParent(m_hwnd);

		SetWindowLong(m_indicator, GWL_STYLE, WS_CHILD | WS_VISIBLE);
		SetParent(m_indicator, parent);
		SetWindowLong(parent, GWL_STYLE, GetWindowLong(parent, GWL_STYLE) | WS_CLIPCHILDREN);
		OnUpdate(m_hwnd);
		PostMessage(m_hwnd, cServiceMessage, cUpdateHeight, 0);
	}

	CStream() << "CChart::DoConstruct() - 1 = " << GetCurrentThreadId() >> DebugLog;
}

void CChart::Finalize(HWND hwnd)
{
	if (nullptr != m_indicator)
	{
		CStream() << "CChart::Finalize()" >> DebugLog;
		LRESULT result = SendMessage(m_hwnd, cServiceMessage, cDestroy, 0);

		CStream() << "CChart::Finalize(): result = " << result << "; GetLastError() = " << GetLastError() >> DebugLog;
		m_indicator = nullptr;
	}
	SetWindowLongPtr(hwnd, GWLP_WNDPROC, reinterpret_cast<LONG_PTR>(m_original));
	MoveWindow(hwnd, 0, 0, m_width, m_height, TRUE);
}

LRESULT CChart::Handle(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	WNDPROC original = m_original;

	if (WM_DESTROY == uMsg)
	{
		Finalize(hwnd);
		gCharts.erase(hwnd);
	}
	else if (WM_SIZE == uMsg)
	{
		OnSize(hwnd);
	}
	else if (cServiceMessage == uMsg)
	{
		if (cCreate == wParam)
		{
			this->DoConstruct();
		}
		else if (cUpdate == wParam)
		{
			OnUpdate(hwnd);
		}
		else if (cUpdateHeight == wParam)
		{
			UpdateHeight();
		}
		else if (cDestroy == wParam)
		{
			if (!DestroyWindow(m_indicator))
			{
				CStream() << "Couldn't destroy indicator window; GetLastError() = " << GetLastError() >> DebugLog;
			}
		}
	}
	return original(hwnd, uMsg, wParam, lParam);
}

Level2Queue& CChart::GetLevel2Queue()
{
	return m_queue;
}

void CChart::OnSize(HWND hwnd)
{
	if (!m_isSpecial && !m_wasSent)
	{
		PostMessage(hwnd, cServiceMessage, cUpdate, 0);
		m_wasSent = true;
	}
}

void CChart::OnUpdate(HWND hwnd)
{
	m_wasSent = false;
	m_isSpecial = true;
	RECT rect;
	BOOL status = GetWindowRect(hwnd, &rect);
	if (status)
	{
		m_width = rect.right - rect.left;
		m_height = rect.bottom - rect.top;

		MoveWindow(hwnd, 0, 0, m_width, m_height - m_chartHeight, TRUE);
		m_isSpecial = false;

		MoveWindow(m_indicator, 0, m_height - m_chartHeight, m_width, m_chartHeight, TRUE);
	}
}

void __stdcall CChart::SetHeight(void* ptr, int height)
{
	CChart* pThis = reinterpret_cast<CChart*>(ptr);
	pThis->DoSetHeight(height);
}

void CChart::DoSetHeight(int height)
{
	if (m_chartHeight != height)
	{
		m_chartHeight = height;
		UpdateHeight();
	}
}

void CChart::UpdateHeight()
{
	HWND parent = GetParent(m_indicator);
	HWND mdi = GetParent(parent);
	RECT rect;
	if(GetWindowRect(parent, &rect))
	{
		POINT point = { rect.left, rect.top };
		if (ScreenToClient(mdi, &point))
		{
			MoveWindow(parent, point.x, point.y, rect.right - rect.left + 1, rect.bottom - rect.top, TRUE);
			MoveWindow(parent, point.x, point.y, rect.right - rect.left, rect.bottom - rect.top, TRUE);
		}
	}
}

extern "C"
{
	CChart* MT5LIStart(HWND handle, const wchar_t* pSymbol, int period, int digits, double lotSize)
	{
		CCsLocker lock(gSynchronizer);

		CStream() << "MT5LIStart(handle = 0x" << handle << ", pSymbol = " << pSymbol << ", period = " << period << ", digits = " << digits << ", lotSize = " << lotSize << ")" >> DebugLog;

		if (0 == gCharts.count(handle))
		{
			string symbol = CW2A(pSymbol);
			CChart& chart = gCharts[handle];
			chart.Construct(handle, symbol, period, digits, lotSize);
			return &chart;
		}

		CStream() << "gCharts.count(handle) = " << gCharts.count(handle) >> DebugLog;

		return nullptr;
	}

	void MT5LIStop(HWND handle)
	{
		CCsLocker lock(gSynchronizer);
		CStream() << "MT5LIStop(handle = 0x" << handle << ")" >> DebugLog;
		auto it = gCharts.find(handle);
		if (gCharts.end() != it)
		{
			CStream() << "MT5LIStop() - stopping" >> DebugLog;
			CChart& chart = it->second;
			chart.Finalize(handle);
			gCharts.erase(it);
		}
		CStream() << "MT5LIStop()" >> DebugLog;
	}

	void Level2_Begin(CChart* pChart)
	{
		if (nullptr != pChart)
		{
			Level2Queue& queue = pChart->GetLevel2Queue();
			queue.Begin();
		}
	}

	void Level2_End(CChart* pChart)
	{
		if (nullptr != pChart)
		{
			Level2Queue& queue = pChart->GetLevel2Queue();
			queue.End();
		}
	}

	void Level2_AddBid(CChart* pChart, double price, double size)
	{
		if (nullptr != pChart)
		{
			Level2Queue& queue = pChart->GetLevel2Queue();
			queue.AddBid(price, size);
		}
	}

	void Level2_AddAsk(CChart* pChart, double price, double size)
	{
		if(nullptr != pChart)
		{
			Level2Queue& queue = pChart->GetLevel2Queue();
			queue.AddAsk(price, size);
		}
	}

	const uint8_t* Level2_WaitFor(CChart* pChart, const uint32_t timeoutInMs)
	{
		if(nullptr != pChart)
		{
			Level2Queue& queue = pChart->GetLevel2Queue();
			return queue.WaitFor(timeoutInMs);
		}
		else
		{
			return nullptr;
		}
	}
}

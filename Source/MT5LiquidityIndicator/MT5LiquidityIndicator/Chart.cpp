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
	const UINT cMessageCreate = RegisterWindowMessage(TEXT("{C940DF81-863D-4FFB-9504-681684775B47}"));
	const UINT cMessage = RegisterWindowMessage(TEXT("{9118E7EC-6B89-40A2-9554-3FF7811DF7A4}"));
	const UINT cMessage2 = RegisterWindowMessage(TEXT("{74B9AEB5-B877-4898-84A4-067F6854B810}"));
	string cTypeName = "MT5LiquidityIndicator.Net.ChartBuilder";
	string cMethodName = "Run";
	const int cDefaultChartHeight = 250;
}
namespace
{
	map<HWND, CChart> gCharts;
}

void MT5LIStart(HWND handle, const wchar_t* pSymbol, int period, int digits, double lotSize)
{
	CStream() << "MT5LIStart(handle = 0x" << handle << ", pSymbol = " << pSymbol << ", period = " << period << ", digits = " << digits << ", lotSize = " << lotSize << ")" >> DebugLog;

	if (0 == gCharts.count(handle))
	{
		string symbol = CW2A(pSymbol);
		CChart& chart = gCharts[handle];
		chart.Construct(handle, symbol, period, digits, lotSize);
	}
}

void MT5LIStop(HWND handle)
{
	CStream()<<"MT5LIStop(handle = 0x"<<handle<<")">>DebugLog;
	auto it = gCharts.find(handle);
	if (gCharts.end() != it)
	{
		CChart& chart = it->second;
		chart.Finalize(handle);
		gCharts.erase(it);
	}
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
	SendMessage(hwnd, cMessageCreate, 0, 0);
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
		PostMessage(m_hwnd, cMessage2, 0, 0);
	}

	CStream() << "CChart::DoConstruct() - 1 = " << GetCurrentThreadId() >> DebugLog;

}

void CChart::Finalize(HWND hwnd)
{
	if (nullptr != m_indicator)
	{
		DestroyWindow(m_indicator);
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
		CStream() << "CChart::Handle()" >> DebugLog;
		Finalize(hwnd);
		gCharts.erase(hwnd);
	}
	else if (WM_SIZE == uMsg)
	{
		OnSize(hwnd);
	}
	else if (cMessage == uMsg)
	{
		OnUpdate(hwnd);
	}
	else if (cMessage2 == uMsg)
	{
		UpdateHeight();
	}
	else if (cMessageCreate == uMsg)
	{
		this->DoConstruct();
	}
	return original(hwnd, uMsg, wParam, lParam);
}
void CChart::OnSize(HWND hwnd)
{
	if (!m_isSpecial && !m_wasSent)
	{
		PostMessage(hwnd, cMessage, 0, 0);
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

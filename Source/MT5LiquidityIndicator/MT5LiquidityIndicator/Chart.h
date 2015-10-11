//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

class CChart
{
public:
	CChart();
	~CChart();

public:
	void Construct(HWND hwnd, const string& symbol, int period, int digits, double lotSize);
	void Finalize(HWND hwnd);

public:
	LRESULT Handle(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

private:
	static void __stdcall SetHeight(void* ptr, int height);

private:
	void DoConstruct();
	void OnSize(HWND hwnd);
	void OnUpdate(HWND hwnd);
	void DoSetHeight(int height);
	void UpdateHeight();

private:
	HWND m_hwnd = nullptr;
	string m_symbol;
	int m_period = 0;
	int m_digits = 0;
	double m_lotSize = 0;

private:
	int m_chartHeight;
	bool m_isSpecial;
	bool m_wasSent;
	int m_width;
	int m_height;
	HWND m_indicator;
	WNDPROC m_original;
};
//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

#ifdef MT5LIQUIDITYINDICATOR_EXPORTS
#define MT5LIQUIDITYINDICATOR_API __declspec(dllexport)
#else
#define MT5LIQUIDITYINDICATOR_API __declspec(dllimport)
#endif

class CChart;

extern "C"
{
	CChart* MT5LIStart(HWND handle, const wchar_t* pSymbol, int period, int digits, double lotSize);
	void MT5LIStop(HWND handle);

	void Level2_Begin(uint64_t handle);
	void Level2_End(uint64_t handle);
	void Level2_AddBid(uint64_t handle, double price, double size);
	void Level2_AddAsk(uint64_t handle, double price, double size);
	const uint8_t* Level2_WaitFor(CChart* pChart, const uint32_t timeoutInMs);
}
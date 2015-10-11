//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

#ifdef MT5LIQUIDITYINDICATOR_EXPORTS
#define MT5LIQUIDITYINDICATOR_API __declspec(dllexport)
#else
#define MT5LIQUIDITYINDICATOR_API __declspec(dllimport)
#endif



void MT5LIStart(HWND handle, const wchar_t* pSymbol, int period, int digits, double lotSize);
void MT5LIStop(HWND handle);

//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#include "stdafx.h"
#include "DllMain.h"
#include "Parameters.h"

namespace
{
	const char* cThis = "This";
	const char* cSymbol = "Symbol";
	const char* cPeriod = "Period";
	const char* cDigits = "Digits";
	const char* cLotSize = "LotSize";
	const char* cFunc = "Func";
}

string FormatParameters(void* pThis, const string& symbol, const int period, const int digits, const double lotSize, void* pFunc)
{
	stringstream stream;
	stream<<cThis<<'='<<reinterpret_cast<uint64_t>(pThis)<<'|';
	stream<<cSymbol<<'='<<symbol<<'|';
	stream<<cPeriod<<'='<<period<<'|';
	stream<<cDigits<<'='<<digits<<'|';
	stream<<cLotSize<<'='<<lotSize<<'|';
	stream<<cFunc<<'='<<reinterpret_cast<uint64_t>(pFunc)<<'|';
	string result = stream.str();
	return result;
}

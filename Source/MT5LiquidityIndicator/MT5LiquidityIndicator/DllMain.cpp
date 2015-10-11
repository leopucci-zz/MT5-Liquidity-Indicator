//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#include "stdafx.h"
#include "DllMain.h"

namespace
{
	HMODULE gDllModule;
	string gDllPath;
	string gDotNetDllPath;
}
const string& DllPath()
{
	return gDllPath;
}
const string& DotNetDllPath()
{
	return gDotNetDllPath;
}

HMODULE DllModule()
{
	return gDllModule;
}

namespace
{
	void InitializeDllPaths(HMODULE hModule)
	{
		gDllModule = hModule;
		char buffer[MAX_PATH] = "";
		GetModuleFileNameA(hModule, buffer, _countof(buffer));
		gDllPath = buffer;
		size_t index = gDllPath.find_last_of('\\');
		gDotNetDllPath = gDllPath.substr(0, 1 + index);
		gDotNetDllPath += "MT5LiquidityIndicator.Net.dll";
	}
}


BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID /*lpReserved*/)
{

	if (DLL_PROCESS_ATTACH == ul_reason_for_call)
	{
		InitializeDllPaths(hModule);
		// prevent unload the library, because we set API hooks and it can be cause of terminal crash
		LoadLibraryA(gDllPath.c_str());
		DisableThreadLibraryCalls(hModule);
	}
	return TRUE;
}


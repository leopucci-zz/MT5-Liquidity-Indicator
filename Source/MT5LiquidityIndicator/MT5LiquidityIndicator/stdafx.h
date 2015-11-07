//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>
#include <tchar.h>
#include <stdlib.h>
#include <sstream>
#include <string>
#include <deque>
#include <vector>
#include <set>
#include <map>
#include <atlbase.h>
#include <functional>



using namespace std;

#ifdef max
#undef max
#endif


#ifdef min
#undef min
#endif




typedef unsigned __int64 uint64;
typedef CComCritSecLock<CComAutoCriticalSection> CCsLocker;

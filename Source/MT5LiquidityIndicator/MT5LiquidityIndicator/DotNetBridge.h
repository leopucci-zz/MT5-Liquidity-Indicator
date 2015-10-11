//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

struct ICLRRuntimeHost;
class CDotNetBridge
{
public:
	CDotNetBridge();
public:
	int Execute(const std::string& assemblyPath, const std::string& typeName, const std::string& methodName, const std::string& argument = std::string());
	void Shutdown();
private:
	ICLRRuntimeHost* m_host;
};

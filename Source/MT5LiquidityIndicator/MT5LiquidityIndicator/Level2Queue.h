//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

#include "Level2.h"

class Level2Queue
{
public:
	Level2Queue();
	Level2Queue(const Level2Queue&) = delete;
	Level2Queue& operator = (const Level2Queue&) = delete;
	~Level2Queue();

public:
	void Begin();
	void AddBid(const double price, const double size);
	void AddAsk(const double price, const double size);
	void End();

public:
	const uint8_t* WaitFor(const uint32_t timeoutInMs);
	void Join();

public:
	CComAutoCriticalSection m_synchronizer;

private:
	HANDLE m_semaphore;
	HANDLE m_event;
	Level2 m_incomming;
	std::vector<uint8_t> m_outgoing;
	std::deque<Level2> m_quotes;
};
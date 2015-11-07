//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#include "stdafx.h"
#include "Level2Queue.h"
#include "Stream.h"


Level2Queue::Level2Queue()
{
	m_semaphore = CreateSemaphore(nullptr, 0, INT_MAX, nullptr);
	if (nullptr == m_semaphore)
	{
		throw runtime_error("Couldn't create a new semaphore");
	}
}

Level2Queue::~Level2Queue()
{
	CloseHandle(m_semaphore);
	m_semaphore = nullptr;
}

void Level2Queue::Begin()
{
	m_incomming = Level2();
}

void Level2Queue::AddBid(const double price, const double size)
{
	m_incomming.AddBid(price, size);
}

void Level2Queue::AddAsk(const double price, const double size)
{
	m_incomming.AddAsk(price, size);
}

void Level2Queue::End()
{
	CCsLocker lock(m_synchronizer);
	m_quotes.push_back(m_incomming);
	ReleaseSemaphore(m_semaphore, 1, nullptr);
}


const uint8_t* Level2Queue::WaitFor(const uint32_t timeoutInMs)
{
	WaitForSingleObject(m_semaphore, timeoutInMs);
	CCsLocker lock(m_synchronizer);
	if (m_quotes.empty())
	{
		return nullptr;
	}

	m_outgoing.clear();

	const Level2& level2 = m_quotes.front();
	level2.Write(m_outgoing);
	m_quotes.pop_front();

	return &m_outgoing.front();
}

//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#include "stdafx.h"
#include "Level2.h"

void Level2::AddBid(const double price, const double size)
{
	m_bids.insert(make_pair(price, size));
}

void Level2::AddAsk(const double price, const double size)
{
	m_asks.insert(make_pair(price, size));
}

namespace
{
	template<typename T> void WriteImpl(const T& value, vector<uint8_t>& buffer)
	{
		const uint8_t* pBegin = reinterpret_cast<const uint8_t*>(&value);
		const uint8_t* pEnd = pBegin + sizeof(T);
		buffer.insert(buffer.end(), pBegin, pEnd);
	}
}

void Level2::Write(std::vector<uint8_t>& buffer) const
{
	WriteImpl(static_cast<uint32_t>(m_bids.size()), buffer);
	WriteImpl(static_cast<uint32_t>(m_asks.size()), buffer);

	for (const auto& element : m_bids)
	{
		WriteImpl(element.first, buffer);
		WriteImpl(element.second, buffer);
	}
	for (const auto& element : m_asks)
	{
		WriteImpl(element.first, buffer);
		WriteImpl(element.second, buffer);
	}
}

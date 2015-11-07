//==============================================================
// Copyright (c) 2015 by Viktar Marmysh mailto:marmysh@gmail.com
//==============================================================

#pragma once

class Level2
{
public:
	void AddBid(const double price, const double size);
	void AddAsk(const double price, const double size);

public:
	void Write(std::vector<uint8_t>& buffer) const;

private:
	std::multimap<double, double, std::less<double> > m_asks;
	std::multimap<double, double, std::greater<double> > m_bids;
};



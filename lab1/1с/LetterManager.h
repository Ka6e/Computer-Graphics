#pragma once
#include "Letter.h"
#include <vector>

class LetterManager
{
public:
	void Add(Letter* l)
	{
		letters.push_back(l);
	}

	void Draw(Graphics& g, float t)
	{
		for (auto l : letters)
		{
			l->Draw(g, t);
		}
	}

private:
	std::vector<Letter*> letters;
};

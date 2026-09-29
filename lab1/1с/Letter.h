#pragma once
#include <cmath>
#include <gdiplus.h>

class Letter
{
public:
    Letter(float x, float y, float amp, float speed, float phase)
        : x(x), y(y), amplitude(amp), speed(speed), phase(phase) {
    }

    virtual ~Letter() = default;

    float JumpY(float t) const
    {
        return y - amplitude * std::abs(std::sin(speed * t + phase));
    }

    virtual void Draw(Graphics& g, float t) = 0;
	//Letter();
	//~Letter();

	//virtual void Draw(Graphics& g, float t) = 0;

protected:
    float x, y;
    float amplitude;
    float speed;
    float phase;
};

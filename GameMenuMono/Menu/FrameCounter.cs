using System;
using System.Collections.Generic;
using System.Text;

namespace GameMenuMono.Menu
{
    class FrameCounter
    {
        double currentFrametimes;
        double weight;
        int numerator;

        public double framerate
        {
            get
            {
                return (numerator / currentFrametimes);
            }
        }

        public FrameCounter(int oldFrameWeight)
        {
            numerator = oldFrameWeight;
            weight = (double)oldFrameWeight / ((double)oldFrameWeight - 1d);
        }

        public void Update(double timeSinceLastFrame)
        {
            currentFrametimes = currentFrametimes / weight;
            currentFrametimes += timeSinceLastFrame;
        }
    }
}

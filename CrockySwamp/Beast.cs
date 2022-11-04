﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal abstract class Beast
    {
        public Point Location { get; set; }
        public int StepRange {get; set;}
        
        public Beast(int x, int y)
        {
            Location = new Point(x, y);
        }

        public void Move (int xDirection, int yDirection)
        {
            Location = new Point (Location.X + xDirection * StepRange, 
                                  Location.Y + yDirection * StepRange);
        }

        public abstract void Say();
    }
}

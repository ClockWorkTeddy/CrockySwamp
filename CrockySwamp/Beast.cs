using System;
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
        public abstract int StepRange {get; set;}

        public int Id { get; set; }
        
        public Beast(int x, int y, int id)
        {
            Location = new Point(x, y);
            Id = id;
        }

        public void Move (int xDirection, int yDirection)
        {
            Location = new Point (Location.X + xDirection * StepRange, 
                                  Location.Y + yDirection * StepRange);
        }

        public abstract void Say();
    }
}

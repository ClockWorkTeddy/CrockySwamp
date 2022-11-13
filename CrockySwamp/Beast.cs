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
        protected Swamp SwampObj;

        public int Id { get; set; }
        
        public Beast(int x, int y, int id, Swamp swamp)
        {
            Location = new Point(x, y);
            Id = id;
            SwampObj = swamp;
        }

        protected int GetNewLocation(int oldLocation)
        {
            Random random = new Random();
            int direction = random.Next(-1, 2);

            return oldLocation + direction * StepRange;

        }

        public abstract void Move();

        public abstract void SayHaunt();

        public abstract void SayDefault();
    }
}

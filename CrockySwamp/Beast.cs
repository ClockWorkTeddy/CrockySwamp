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
        public event EventHandler<DrawArgs>? Talk;
        
        public Beast(int x, int y, int id, Swamp swamp)
        {
            Location = new Point(x, y);
            Id = id;
            SwampObj = swamp;
            Talk += Drawer.Talk;
        }

        protected int GetNewLocation(int oldLocation)
        {
            Random random = new Random();
            int direction = random.Next(-1, 2);

            return oldLocation + direction * StepRange;

        }

        protected int GetProbability(int phrasesCount, double chance)
        {
            int cases_def = Convert.ToInt16(1 / chance);
            int cases = Convert.ToInt16(phrasesCount / chance);

            Random random = new Random();
            int rnd = random.Next(cases);

            if (rnd % cases_def == 0)
                return rnd / cases_def;
            else
                return -1;
        }

        protected void OnTalk(object sender, DrawArgs args)
        {
            Talk?.Invoke(sender, args);
        }

        public abstract void Move();

        public abstract void SayHaunt(int id);

        public abstract void SayDefault();
    }
}

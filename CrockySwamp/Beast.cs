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
        protected Swamp Swamp;

        public int Id { get; set; }
        public int Index =>
            Swamp.Size * Location.Y + Location.X;

        public event EventHandler<DrawArgs>? Talk;
        public abstract int StepRange { get; set; }

        public Beast(int index, int id, Swamp swamp)
        {
            Swamp = swamp;
            Location = GetLocFromIndex(index);
            Id = id;
            Talk += Drawer.CollectMessages;
        }

        private Point GetLocFromIndex(int index)
        {
            int x = index % Swamp.Size;
            int y = index / Swamp.Size;

            return new Point(x, y);
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

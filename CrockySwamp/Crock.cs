using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Crock : Beast
    {
        public override int StepRange { get; set; } = 1;
        public delegate void CrockTalker(string message, string color);
        public CrockTalker? Talk { get; set; }
        private List<string> Phrases = new List<string>() 
        {
            "Roar!",
            "Harr!",
            "Where is that frog?!"
        };

        public Crock (int x, int y, int id, Swamp swamp) : base(x, y, id, swamp)
        {
            Talk += Drawer.Talk;
        }

        public override void SayDefault()
        {
            int rnd = GetProbability(Phrases.Count);
            
            if (rnd != -1)
                Talk?.Invoke($"C{Id}: {Phrases[rnd]}", "#008800");
        }

        private int GetProbability(int phrasesCount)
        {
            double chanse = 0.2;
            int cases_def = Convert.ToInt16(1 / chanse);
            int cases = Convert.ToInt16(phrasesCount / chanse);

            Random random = new Random();
            int rnd = random.Next(cases);

            if (rnd % cases_def == 0)
                return rnd / cases_def;
            else
                return -1;
        }

        public override void SayHaunt()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field? field = SwampObj.GetField(newX, newY);

            if (field != null)
                if (field.State != Field.FieldState.Crock)
                {
                    if (field.State == Field.FieldState.Frog)
                        SwampObj.RemoveFrog(newX, newY);

                    SwampObj.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }

            SayDefault();
        }

    }
}

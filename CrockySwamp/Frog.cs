using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Frog : Beast
    {
        public override int StepRange { get; set; } = 2;
        public delegate void FrogTalker(string message, string color);
        public FrogTalker? Talk { get; set; }
        private List<string> Phrases = new List<string>()
        {
            "Ribbit! So good in {0};{1} field!",
            "Ribbit! My field {0};{1} is quite cold..."
        };
        private List<string> HauntPhrases = new List<string>()
        {
            "OMG, FUCKIN' C{0} IS HERE!",
            "OH NO WTF, C{0}!!!"
        };
        public Frog(int x, int y, int id, Swamp swamp) : base(x, y, id, swamp)
        {
            Talk += Drawer.Talk;
        }

        public override void SayDefault()
        {
            double chance = 0.2;
            int rnd = GetProbability(Phrases.Count, chance);

            if (rnd != -1)
            {
                StringBuilder phrase = new StringBuilder().AppendFormat(Phrases[rnd], Location.X, Location.Y);
                Talk?.Invoke($"F{Id}: {phrase}", "#00ff00");
            }
        }

        public override void SayHaunt(int id)
        {
            double chance = 1.0;
            int rnd = GetProbability(HauntPhrases.Count, chance);
            StringBuilder phrase = new StringBuilder().AppendFormat(HauntPhrases[rnd], id);

            Talk?.Invoke($"F{Id}: {phrase}", "#00ff00");
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field? field = SwampObj.GetField(newX, newY);

            if (field != null)
                if (field.State == Field.FieldState.Empty)
                {
                    SwampObj.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }

            SayDefault();
        }
    }
}

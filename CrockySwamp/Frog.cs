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

        private List<string> Phrases = new List<string>()
        {
            "Ribbit! So good in {0};{1} field!",
            "Ribbit! My field {0};{1} is quite cold..."
        };
        private List<string> HauntPhrases = new List<string>()
        {
            "OMG, BIG BAD C{0} IS HERE!",
            "OH NO GET OUT, C{0}!!!"
        };
        public Frog(int index, int id, Swamp swamp) 
            : base(index, id, swamp)
        {
        }

        public override void SayDefault()
        {
            double chance = 0.2;
            int rnd = GetProbability(Phrases.Count, chance);

            if (rnd != -1)
            {
                StringBuilder phrase = new StringBuilder().AppendFormat(Phrases[rnd], Location.X, Location.Y);
                OnTalk(this, new DrawArgs($"F{Id}: {phrase}", "#00ff00"));
            }
        }

        public override void SayHaunt(int id)
        {
            double chance = 1.0;
            int rnd = GetProbability(HauntPhrases.Count, chance);
            StringBuilder phrase = new StringBuilder().AppendFormat(HauntPhrases[rnd], id);

            OnTalk(this, new DrawArgs($"F{Id}: {phrase}", "#00ff00"));
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field? field = Swamp.GetField(newX, newY);

            if (field != null)
                if (field.State == Field.FieldState.Empty)
                {
                    Swamp.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }

            SayDefault();
        }
    }
}

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
        private List<string> HauntPhrases = new List<string>()
        {
            "Mmm, there is yammy F{0} here!..",
            "Come here, little F{0}!.."
        };

        public Crock (int x, int y, int id, Swamp swamp) : base(x, y, id, swamp)
        {
            Talk += Drawer.Talk;
        }

        public override void SayDefault()
        {
            double chance = 0.2;
            int rnd = GetProbability(Phrases.Count, chance);
            
            if (rnd != -1)
                Talk?.Invoke($"C{Id}: {Phrases[rnd]}", "#008800");
        }

        public override void SayHaunt(int id)
        {
            double chance = 1.0;
            int rnd = GetProbability(HauntPhrases.Count, chance);
            StringBuilder phrase = new StringBuilder().AppendFormat(HauntPhrases[rnd], id);

            Talk?.Invoke($"C{Id}: {phrase}", "#008800");
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field? field = SwampObj.GetField(newX, newY);

            if (field != null)
                if (field.State != Field.FieldState.Crock)
                {
                    if (field.State == Field.FieldState.Frog && field.Beast != null)
                    {
                        SayHaunt(field.Beast.Id);
                        SwampObj.RemoveFrog(newX, newY, this.Id);
                    }

                    SwampObj.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }

            SayDefault();
        }

    }
}

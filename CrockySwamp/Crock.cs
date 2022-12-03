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
        public event EventHandler<MurderArgs>? Murder;
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

        public Crock (int index, int id, Swamp swamp) 
            : base(index, id, swamp)
        {
        }

        public override void SayDefault()
        {
            double chance = 0.2;
            int rnd = GetProbability(Phrases.Count, chance);
            
            if (rnd != -1)
                OnTalk(this, new DrawArgs($"C{Id}: {Phrases[rnd]}", "#008800"));
        }

        public override void SayHaunt(int id)
        {
            double chance = 1.0;
            int rnd = GetProbability(HauntPhrases.Count, chance);
            StringBuilder phrase = new StringBuilder().AppendFormat(HauntPhrases[rnd], id);

            OnTalk(this, new DrawArgs($"C{Id}: {phrase}", "#008800"));
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field? field = Swamp.GetField(newX, newY);

            if (field != null)
                if (field.State != Field.FieldState.Crock)
                {
                    if (field.State == Field.FieldState.Frog && field.Beast != null)
                    {
                        SayHaunt(field.Beast.Id);
                        Murder?.Invoke(this, new MurderArgs(this, field));
                        Swamp.RemoveFrog(newX, newY, this.Id);
                    }

                    Swamp.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }

            SayDefault();
        }

    }
}

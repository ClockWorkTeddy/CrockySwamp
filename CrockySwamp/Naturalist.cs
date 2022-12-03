using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Naturalist
    {
        private string Name;
        private string? MurderAnnounce;
        private Swamp Swamp;
        private List<string> Phrases = new List<string>()
        {
            "There is cute little {0} on the {1};{2} field!",
            "I see a spooky {0} on a {1};{2} field...",
            "There is nothing on {0};{1} field...",
            "OH NO, {0} JUST'VE KILLED POOR {1} ON THE {2};{3} FIELD!!! Cruel bloody world..."
        };

        public event EventHandler<DrawArgs> Talk;


        public Naturalist(Swamp swamp, string name)
        {
            Name = name;
            Swamp = swamp;
            Talk += Drawer.Talk;
            foreach (var field in Swamp.Fields)
            {
                if (field.Beast is Crock crock)
                    crock.Murder += SayHunt;
            }
        }

        public void Observe()
        {
            Random random = new Random();
            int x = random.Next(Swamp.Size);
            int y = random.Next(Swamp.Size);
            int index = y * Swamp.Size + x;

            Say(GetPhrase(index));
        }

        private string GetPhrase(int fieldIndex)
        {
            if (!String.IsNullOrEmpty(MurderAnnounce))
                return MurderAnnounce;

            Field field = Swamp.Fields[fieldIndex];

            {
                if (field.State == Field.FieldState.Crock)
                    return String.Format(Phrases[1], "C" + field?.Beast?.Id, field?.Location.X, field?.Location.Y);
                else if (field.State == Field.FieldState.Frog)
                    return String.Format(Phrases[0], "F" + field?.Beast?.Id, field?.Location.X, field?.Location.Y);
                else
                    return String.Format(Phrases[2], field?.Location.X, field?.Location.Y);
            }
        }

        public void Say(string message)
        {
            Talk.Invoke(this, new DrawArgs($"{this.Name}: {message}", "#0088ff"));
            MurderAnnounce = "";
        }

        public void SayHunt(object? sender, MurderArgs ma)
        {
            MurderAnnounce = String.Format(Phrases[3], "C" + ma?.Crock?.Id, 
                                                       "F" + ma?.Field?.Beast?.Id, 
                                                             ma?.Field?.Location.X,
                                                             ma?.Field?.Location.Y);
        }
    }
}

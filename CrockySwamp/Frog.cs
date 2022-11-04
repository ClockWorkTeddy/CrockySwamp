using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Frog : Beast
    {
        public override int StepRange { get; set; } = 2;
        public Frog(int x, int y, int id) : base(x, y, id)
        {

        }

        public override void Say()
        {
            string message = "\"Croak.\"";

            if (Id % 2 == 0)
                message = "\"Ribbit.\"";

            Console.WriteLine($"Frog {Id} says {message}!");
        }
    }
}

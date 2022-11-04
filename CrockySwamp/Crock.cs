using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal class Crock : Beast
    {
        public override int StepRange { get; set; } = 1;

        public Crock (int x, int y, int id) : base(x, y, id)
        {

        }

        public override void Say()
        {
            Console.WriteLine($"Crock {Id} says \"Roar!\"");
        }
    }
}

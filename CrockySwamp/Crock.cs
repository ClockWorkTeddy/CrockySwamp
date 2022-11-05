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

        public Crock (int x, int y, int id, Swamp swamp) : base(x, y, id, swamp)
        {

        }

        public override void Say()
        {
            Console.WriteLine($"Crock {Id} says \"Roar!\"");
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field.FieldState? state = SwampObj.GetFieldState(newX, newY);

            if (state != null)
                if (state == Field.FieldState.Crock)
                    Location = new Point(newX, newY);
        }

    }
}

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
        public Frog(int x, int y, int id, Swamp swamp) : base(x, y, id, swamp)
        {

        }

        public override void Say()
        {
            string message = "\"Croak.\"";

            if (Id % 2 == 0)
                message = "\"Ribbit.\"";

            Console.WriteLine($"Frog {Id} says {message}!");
        }

        public override void Move()
        {
            int newX = GetNewLocation(Location.X);
            int newY = GetNewLocation(Location.Y);

            Field.FieldState? state = SwampObj.GetFieldState(newX, newY);

            if (state != null)
                if (state == Field.FieldState.Empty)
                    Location = new Point(newX, newY);
        }
    }
}

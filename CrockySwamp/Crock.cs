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

            Field? field = SwampObj.GetField(newX, newY);

            if (field != null)
                if (field.State != Field.FieldState.Crock)
                {
                    if (field.State == Field.FieldState.Frog)
                        SwampObj.RemoveFrog(newX, newY);

                    SwampObj.RefreshFields(Location.X, Location.Y, newX, newY, this);
                    Location = new Point(newX, newY);
                }
        }

    }
}

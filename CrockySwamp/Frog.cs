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

        public override void SayDefault()
        {
            string message = "\"Croak.\"";

            if (Id % 2 == 0)
                message = "\"Ribbit.\"";

            Console.WriteLine($"Frog {Id} says {message}!");
        }

        public override void SayHaunt()
        {
            throw new NotImplementedException();
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
        }
    }
}

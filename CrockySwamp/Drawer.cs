using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal static class Drawer
    {
        internal static void Draw(Swamp swamp)
        {
            Console.WriteLine();
            for (int y = 0; y < swamp.Size; y++)
            {
                for (int x = 0; x < swamp.Size; x++)
                {
                    int index = y * swamp.Size + x;
                    var beast = swamp.Fields[index].Beast;
                    Console.Write($"{(beast == null ? 0 : beast.Id)}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int GetDigitFromState(Field.FieldState state)
        {
            return ((int)state);
        }
    }
}

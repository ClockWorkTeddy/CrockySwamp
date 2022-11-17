using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrockySwamp
{
    internal static class Drawer
    {
        internal static void Draw(object sender, EventArgs e)
        {
            Swamp swamp = (Swamp)sender;

            for (int y = 0; y < swamp.Size; y++)
            {
                for (int x = 0; x < swamp.Size; x++)
                {
                    int index = y * swamp.Size + x;
                    var field = swamp.Fields[index];
                    Console.Write($"{GetSymbol(field)}  ");
                }
                Console.WriteLine();
            }
        }
        private static string GetSymbol(Field field)
        {
            string color = GetColorByState(field.State);

            return field.Beast == null ? "0".Pastel("#442200") 
                                       : field.Beast.Id.ToString().Pastel(color);
        }

        static string GetColorByState(Field.FieldState state)
        {
            if (state == Field.FieldState.Frog)
               return "#00ff00";
            else
                return "#008800";
        }

        public static void Talk(object? sender, DrawArgs args) =>
            Console.WriteLine($"{args.Message.Pastel(args.Color)}");
    }

    internal class DrawArgs : EventArgs
    {
        public string? Message;
        public string? Color;

        public DrawArgs(string? message, string? color)
        {
            Message = message;
            Color = color;
        }
    }
}

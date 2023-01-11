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
        static Dictionary<string, string> PhrasesToSay = new Dictionary<string, string>();
        static string NaturalistPhrase = "";
        internal static void Draw(object? sender, EventArgs e)
        {
            Console.Clear();

            if (sender != null)
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

        public static void CollectMessages(object? sender, DrawArgs args)
        {
            if (args.Message != null && args.Color != null)
                if (args.Color == "#0088ff")
                    NaturalistPhrase = args.Message;
                else
                    PhrasesToSay[args.Message] = args.Color;
        }

        public static void Print()
        {
            Console.WriteLine($"{NaturalistPhrase.Pastel("#0088ff")}");

            foreach (var valuePair in PhrasesToSay)
                Console.WriteLine($"{valuePair.Key.Pastel(valuePair.Value)}");

            PhrasesToSay.Clear();
        }
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

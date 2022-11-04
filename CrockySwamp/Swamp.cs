using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace CrockySwamp
{
    internal class Swamp
    {
        public int Size { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
        int FrogsCount = 10;
        public List<Beast> Beasts { get; set; } = new List<Beast>();

        public Swamp(int size)
        {
            Size = size;
            for (int i = 0; i < Size * Size; i++)
            {
                int x = i / Size;
                int y = i % Size;
                Fields.Add(new Field(new Point(x, y)));
            }
        }

        public void InitFrogs()
        {
            for (int i = 0; i < FrogsCount; i++)
                AddFrog(i);
        }

        void AddFrog(int id)
        {
            Random rnd = new Random();
            int x, y, index = 0;
            do
            {
                x = rnd.Next(Size);
                y = rnd.Next(Size);
                index = x * Size + y;
            } 
            while (Fields[index].State != Field.FieldState.Empty);

            Beasts.Add(new Frog(x, y, id));
            Fields[index].State = Field.FieldState.Frog;
        }
    }
}

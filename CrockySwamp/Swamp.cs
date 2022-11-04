using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CrockySwamp
{
    internal class Swamp
    {
        public int Size { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
        public int CrocksCount { get; set; }
        public int FrogsCount { get; set; }

        public Swamp(int size)
        {
            Size = size;
            for (int i = 0; i < Size; i++)
            {
                int x = i / Size;
                int y = i % Size;
                Fields.Add(new Field(new Point(x, y)));
            }
        }
    }
}

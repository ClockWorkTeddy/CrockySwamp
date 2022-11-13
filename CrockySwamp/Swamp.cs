using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace CrockySwamp
{
    internal class Swamp
    {
        int FrogsCount = 0;
        int CrocksCount = 0;
        public int Size { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<Beast> Beasts { get; set; } = new List<Beast>();
        public List<Beast> DeadBeasts { get; set; } = new List<Beast>();
        public delegate void SwampDrawer(Swamp swamp);
        public SwampDrawer? Draw { get; set; }

        public Swamp(int size)
        {
            Size = size;
            for (int i = 0; i < Size * Size; i++)
            {
                int x = i % Size;
                int y = i / Size;
                Fields.Add(new Field(new Point(x, y)));
            }
            FrogsCount = Size * Size / 3;
            CrocksCount = FrogsCount / 3;
        }

        public void InitFrogs()
        {
            for (int i = 0; i < FrogsCount; i++)
                AddFrog(i + 1);

            Draw?.Invoke(this);
        }

        void AddFrog(int id)
        {
            Random rnd = new Random();
            int x, y, index = 0;
            do
            {
                x = rnd.Next(Size);
                y = rnd.Next(Size);
                index = GetIndex(x, y);
            } 
            while (Fields[index].State != Field.FieldState.Empty);

            Frog newFrog = new Frog(x, y, id, this);
            Beasts.Add(newFrog);
            Fields[index].Beast = newFrog;
            Fields[index].State = Field.FieldState.Frog;
        }

        public void InitCrocks()
        {
            for (int i = 0; i < CrocksCount; i++)
                AddCrock(i + 1);

            Draw?.Invoke(this);
        }

        private void AddCrock(int id)
        {
            Random rnd = new Random();
            int x, y, index = 0;
            do
            {
                x = rnd.Next(Size);
                y = rnd.Next(Size);
                index = GetIndex(x, y);
            }
            while (Fields[index].State != Field.FieldState.Empty);

            Crock newCrock = new(x, y, id, this);
            Beasts.Add(newCrock);
            Fields[index].Beast = newCrock;
            Fields[index].State = Field.FieldState.Crock;
        }

        public Field? GetField(int x, int y)
        {
            if (InRange(x) && InRange(y))
                return Fields[GetIndex(x, y)];
            else
                return null;
        }

        private bool InRange(int input) =>
            input >= 0 && input < Size;

        public void RefreshFields(int oldX, int oldY, int newX, int newY, Beast beast)
        {
            int oldIndex = GetIndex(oldX, oldY);
            int newIndex = GetIndex(newX, newY);

            Fields[oldIndex].State = Field.FieldState.Empty;
            Fields[oldIndex].Beast = null;
            Fields[newIndex].Beast = beast;

            if (beast is Frog)
                Fields[newIndex].State = Field.FieldState.Frog;
            else
                Fields[newIndex].State = Field.FieldState.Crock;

        }

        public void Move()
        {
            foreach (var beast in Beasts)
                beast.Move();

            foreach (var deadBeast in DeadBeasts)
                Beasts.Remove(deadBeast);

            Draw?.Invoke(this);
        }

        int GetIndex(int x, int y)
        {
            if (x >= 0 && y >= 0)
                return y * Size + x;
            else
                return -1;
        }

        public void RemoveFrog(int x, int y)
        {
            int index = GetIndex(x, y);

            var beast = Fields[index].Beast;

            if (beast != null)
                DeadBeasts.Add(beast);
        }
    }
}

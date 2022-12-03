using System.Drawing;

namespace CrockySwamp
{
    internal class Swamp
    {
        private List<Beast> DeadBeasts = new List<Beast>();

        public int Size { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<Beast> Beasts { get; set; } = new List<Beast>();
        public EventHandler? Draw { get; set; }

        public Swamp(int size)
        {
            Size = size;
            Draw += Drawer.Draw;
            InitFields();
            InitFrogs();
            InitCrocks();
        }

        private void InitFields()
        {
            for (int i = 0; i < Size * Size; i++)
            {
                int x = i % Size;
                int y = i / Size;
                Fields.Add(new Field(new Point(x, y)));
            }
        }

        public void InitFrogs()
        {
            int frogsCount = Size * Size / 3;

            for (int i = 0; i < frogsCount; i++)
                AddFrog(i + 1);
        }

        void AddFrog(int id)
        {
            int index = GetIndex();
            Frog newFrog = new Frog(index, id, this);
            Beasts.Add(newFrog);
            Fields[index].Beast = newFrog;
            Fields[index].State = Field.FieldState.Frog;
        }

        public void InitCrocks()
        {
            int crocksCount = Size * Size / 9;

            for (int i = 0; i < crocksCount; i++)
                AddCrock(i + 1);
        }

        private void AddCrock(int id)
        {
            int index = GetIndex();
            Crock newCrock = new(index, id, this);
            Beasts.Add(newCrock);
            Fields[index].Beast = newCrock;
            Fields[index].State = Field.FieldState.Crock;
        }

        private int GetIndex()
        {
            Random rnd = new Random();
            int index = 0;
            
            while (Fields[index].State != Field.FieldState.Empty)
                index = rnd.Next(Size * Size);

            return index;
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

            Draw?.Invoke(this, new EventArgs());
        }

        int GetIndex(int x, int y)
        {
            if (x >= 0 && y >= 0)
                return y * Size + x;
            else
                return -1;
        }

        public void RemoveFrog(int x, int y, int crockId)
        {
            int index = GetIndex(x, y);

            var beast = Fields[index].Beast;

            if (beast != null)
            {
                beast.SayHaunt(crockId);
                DeadBeasts.Add(beast);
            }
        }
    }

    internal class MurderArgs : EventArgs
    {
        public Crock? Crock;
        public Field? Field;

        public MurderArgs(Crock crock, Field field)
        {
            Crock = crock;
            Field = field;
        }
    }
}

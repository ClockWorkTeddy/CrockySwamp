using CrockySwamp;

Console.WriteLine("Hello, Swamp!");
int size = 5;
Swamp swamp = new(size);
swamp.Draw += Drawer.Draw;

Naturalist alex = new(swamp, "Alex");
swamp.Murder += alex.SayHunt;
swamp.InitFrogs();
swamp.InitCrocks();

while (true)
{
    Console.Clear();
    swamp.Move();
    alex.Observe();
    char inputKey = Console.ReadKey().KeyChar;

    if (inputKey == 'q')
        break;
}


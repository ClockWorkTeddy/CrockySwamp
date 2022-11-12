using CrockySwamp;

Console.WriteLine("Hello, Swamp!");
int size = 5;
Swamp swamp = new(size);
swamp.Draw += Drawer.Draw;

swamp.InitFrogs();
swamp.InitCrocks();

while (true)
{
    swamp.Move();
    char inputKey = Console.ReadKey().KeyChar;

    if (inputKey == 'q')
        break;
}


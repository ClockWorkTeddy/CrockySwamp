using CrockySwamp;

Console.WriteLine("Hello, Swamp!");
int size = 5;
Swamp swamp = new(size);
swamp.InitFrogs();
Drawer.Draw(swamp);

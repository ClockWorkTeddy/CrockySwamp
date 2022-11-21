using CrockySwamp;

bool Ongoing = true;
int size = 5;

Swamp swamp = new(size);
Naturalist alex = new(swamp, "Alex");

while (Ongoing)
    NextStep();

void NextStep()
{
    Console.Clear();
    swamp.Move();
    alex.Observe();
    char inputKey = Console.ReadKey().KeyChar;

    if (inputKey == 'q')
        Ongoing= false;
}


<Query Kind="Program" />

void Main()
{
    Dump(1, 2, 3);
    Dump(1, 2);
    Dump(1);
}

static void Dump(int x, int y = 20, int z = 30)
{
    Console.WriteLine("x={0} y={1} z={2}", x, y, z);
}


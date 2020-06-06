<Query Kind="Program" />

void Main()
{
	Type t = typeof (Walnut);
	Walnut w = new Walnut();
	w.Crack();
	FieldInfo f = t.GetField ("cracked", BindingFlags.NonPublic |
                                         BindingFlags.Instance);
	f.SetValue (w, false);
	Console.WriteLine (w);         // False
}

class Walnut
{
	private bool cracked;
	public void Crack() { cracked = true; }

	public override string ToString() { return cracked.ToString(); }
}
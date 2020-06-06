<Query Kind="Program" />

void Main()
{
	AddWidgets(); // In another method so they go out of scope
	
	Console.WriteLine("Before GC:");
	Widget.ListAllWidgets();
	
	GC.Collect();

	Console.WriteLine ("After GC:");
	Widget.ListAllWidgets();
}

void AddWidgets()
{
	new Widget ("foo");
	new Widget ("bar");
}

class Widget
{
	static List<WeakReference> _allWidgets = new List<WeakReference>();

	public readonly string Name;

	public Widget (string name)
	{
		Name = name;
		_allWidgets.Add (new WeakReference (this));
	}

	public static void ListAllWidgets()
	{
		foreach (WeakReference weak in _allWidgets)
		{
			Widget w = (Widget)weak.Target;
			if (w != null) Console.WriteLine (w.Name);
		}
	}
}
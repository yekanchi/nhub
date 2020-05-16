<Query Kind="Program">
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var foo = new Foo();
	foo.PropertyChanged += (sender, args) => args.Dump ("Property changed!");
	foo.CustomerName = "asdf";
}

public class Foo : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged = delegate { };

	void RaisePropertyChanged ([CallerMemberName] string propertyName = null)
	{
		PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
	}

	string customerName;
	public string CustomerName
	{
		get { return customerName; }
		set
		{
			if (value == customerName) return;
			customerName = value;
			RaisePropertyChanged();
			// The compiler converts the above line to:
			// RaisePropertyChanged ("CustomerName");
		}
	}
}
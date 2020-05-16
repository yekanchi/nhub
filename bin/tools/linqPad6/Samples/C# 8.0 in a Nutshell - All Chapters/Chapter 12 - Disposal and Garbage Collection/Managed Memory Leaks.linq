<Query Kind="Program" />

void Main()
{
	CreateClients();
}

static Host _host = new Host();

public static void CreateClients()
{
	Client[] clients = Enumerable.Range (0, 1000)
	 .Select (i => new Client (_host))
	 .ToArray();

	// Do something with clients ... 
}

class Host
{
	public event EventHandler Click;
}

class Client
{
	Host _host;
	public Client (Host host)
	{
		_host = host;
		_host.Click += HostClicked;
	}

	void HostClicked (object sender, EventArgs e) {  }
}

class Test
{
	
}
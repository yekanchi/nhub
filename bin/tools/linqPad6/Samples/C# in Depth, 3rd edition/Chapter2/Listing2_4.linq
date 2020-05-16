<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    EventHandler handler;
    
    handler = new EventHandler(HandleDemoEvent);
    handler(null, EventArgs.Empty);
    
    handler = HandleDemoEvent;
    handler(null, EventArgs.Empty);
    
    handler = delegate(object sender, EventArgs e)
        { Console.WriteLine("Handled anonymously"); };
    handler(null, EventArgs.Empty);
    
    handler = delegate
        { Console.WriteLine("Handled anonymously"); };
    handler(null, EventArgs.Empty);
    
    MouseEventHandler mouseHandler = HandleDemoEvent;
    mouseHandler(null, new MouseEventArgs(MouseButtons.None,
                                          0, 0, 0, 0));
}

void HandleDemoEvent(object sender, EventArgs e)
{
    Console.WriteLine("Handled by HandleDemoEvent");
}

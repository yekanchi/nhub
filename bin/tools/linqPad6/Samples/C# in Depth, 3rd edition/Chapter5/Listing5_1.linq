<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    Button button = new Button();
    button.Text = "Click me";
    button.Click += new EventHandler(LogPlainEvent);
    button.KeyPress += new KeyPressEventHandler(LogKeyEvent);
    button.MouseClick += new MouseEventHandler(LogMouseEvent);
    
    Form form = new Form();
    form.AutoSize = true;
    form.Controls.Add(button);
    Application.Run(form);
}

static void LogPlainEvent(object sender, EventArgs e)
{
    Console.WriteLine ("LogPlain");
}

static void LogKeyEvent(object sender, KeyPressEventArgs e)
{
    Console.WriteLine ("LogKey");
}

static void LogMouseEvent(object sender, MouseEventArgs e)
{
    Console.WriteLine ("LogMouse");
}

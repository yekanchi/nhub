<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <NuGetReference>Simplify.Windows.Forms</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

Form sigCapture = new Form();

List<System.Drawing.Point> points = new List<System.Drawing.Point>();
bool draw = false;
sigCapture.Show();
var moves = Observable.FromEventPattern<MouseEventArgs>(sigCapture,"MouseMove")
                      .Select(x => x.EventArgs);
var mouseDowns = Observable.FromEventPattern<MouseEventArgs>(sigCapture,"MouseDown")
                           .Select(x => x.EventArgs);
var mouseUps = Observable.FromEventPattern<MouseEventArgs>(sigCapture,"MouseUp")
                         .Select(x => x.EventArgs);
mouseDowns.Subscribe( x => { draw = true; });
mouseUps.Subscribe( x => { draw = false; });

moves.Subscribe(p => 
{
	points.Add(p.Location);
	if(points.Count >= 2 && draw)
	{
		sigCapture.CreateGraphics()
	             .DrawLine(new System.Drawing.Pen(System.Drawing.Color.Purple,5.7f),	points[points.Count - 2],points[points.Count - 1]);
	}
});
<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace> 
</Query>

Form myForm = new Form();

myForm.Show();

var moves = Observable. FromEventPattern <MouseEventArgs>(myForm,"MouseMove")
					  .Select( p => p.EventArgs.Location);

var bisector = moves.Where(p => p.X == p.Y);

bisector.Dump("You are on the bisector at");
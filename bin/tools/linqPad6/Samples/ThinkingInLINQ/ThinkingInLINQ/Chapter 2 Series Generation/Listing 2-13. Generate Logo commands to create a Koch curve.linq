<Query Kind="Statements">

</Query>

string koch = "F";
Func<string,string> transform = x => x.Replace("F","F+F-F-F+F");
int length = 3;
//Initialize the location and direction of the turtle
string command = @"home
setxy 10 340
right 90
";

//Finish it in the next line so a new line appears in the command
command += Enumerable.Range(1,length)
.Select (k => koch = transform(koch))
.Last()
.Replace("F","forward 15")
.Replace("+",Environment.NewLine + "Left 90" + Environment.NewLine)
.Replace("-",Environment.NewLine + "Right 90" + Environment.NewLine);
command.Dump();
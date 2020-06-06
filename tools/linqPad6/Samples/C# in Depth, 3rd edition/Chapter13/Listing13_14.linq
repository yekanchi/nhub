<Query Kind="Statements">
  <Reference Relative="Chapter13.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter13\Chapter13.exe</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>Chapter13</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

Func<Square> squareFactory = () => new Square(new Point(5, 5), 10);
Func<IShape> shapeFactory = squareFactory;

Action<IShape> shapePrinter = shape => Console.WriteLine(shape.Area);
Action<Square> squarePrinter = shapePrinter;

squarePrinter(squareFactory());
shapePrinter(shapeFactory());

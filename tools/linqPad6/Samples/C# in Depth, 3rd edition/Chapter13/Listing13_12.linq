<Query Kind="Statements">
  <Reference Relative="Chapter13.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter13\Chapter13.exe</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>Chapter13</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

List<Circle> circles = new List<Circle> {
    new Circle(new Point(0, 0), 15),
    new Circle(new Point(10, 5), 20),
};
List<Square> squares = new List<Square> {
    new Square(new Point(5, 10), 5),
    new Square(new Point(-10, 0), 2)
};

List<IShape> shapesByAdding = new List<IShape>();
shapesByAdding.AddRange(Shapes.Circles);
shapesByAdding.AddRange(Shapes.Squares);

List<IShape> shapesByConcat = circles.Concat<IShape>(squares).ToList();

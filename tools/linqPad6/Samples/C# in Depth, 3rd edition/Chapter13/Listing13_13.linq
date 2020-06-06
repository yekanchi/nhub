<Query Kind="Program">
  <Reference Relative="Chapter13.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter13\Chapter13.exe</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>Chapter13</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

void Main()
{
    List<Circle> circles = new List<Circle> {
        new Circle(new Point(0, 0), 15),
        new Circle(new Point(10, 5), 20),
    };
    IComparer<IShape> areaComparer = new AreaComparer();
    circles.Sort(areaComparer);    
}

class AreaComparer : IComparer<IShape>
{
    public int Compare(IShape x, IShape y)
    {
        return x.Area.CompareTo(y.Area);
    }
}

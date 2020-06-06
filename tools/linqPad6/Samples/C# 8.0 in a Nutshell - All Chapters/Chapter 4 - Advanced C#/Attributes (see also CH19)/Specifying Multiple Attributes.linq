<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

[assembly:CLSCompliant(false)]

[Serializable, Obsolete, CLSCompliant (false)]
public class Bar1 {}

[Serializable]
[Obsolete]
[CLSCompliant (false)]
public class Bar2 {}

[Serializable, Obsolete]
[CLSCompliant (false)]
public class Bar3 {}

void Main()
{
}


<Query Kind="Statements" />

Type type = typeof (string);
Type[] parameterTypes = { typeof (int) };
MethodInfo method = type.GetMethod ("Substring", parameterTypes);

object[] arguments = { 2 };
object returnValue = method.Invoke ("stamp", arguments);
Console.WriteLine (returnValue);                           // "amp"

ParameterInfo[] paramList = method.GetParameters();
foreach (ParameterInfo x in paramList)
{
	Console.WriteLine (x.Name);                 // startIndex
	Console.WriteLine (x.ParameterType);        // System.Int32
}
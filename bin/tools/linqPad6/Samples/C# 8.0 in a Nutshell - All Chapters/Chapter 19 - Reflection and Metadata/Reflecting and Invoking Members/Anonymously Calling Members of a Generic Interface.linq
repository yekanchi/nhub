<Query Kind="Program" />

void Main()
{
	Console.WriteLine (ToStringEx (new List<int> { 5, 6, 7 }));
	Console.WriteLine (ToStringEx ("xyyzzz".GroupBy (c => c)));
}

public static string ToStringEx (object value)
{
	if (value == null) return "<null>";
	if (value.GetType().IsPrimitive) return value.ToString();

	StringBuilder sb = new StringBuilder();

	if (value is IList)
		sb.Append ("List of " + ((IList)value).Count + " items: ");

	Type closedIGrouping = value.GetType().GetInterfaces()
		.Where (t => t.IsGenericType &&
                     t.GetGenericTypeDefinition() == typeof (IGrouping<,>))
		.FirstOrDefault();

	if (closedIGrouping != null)   // Call the Key property on IGrouping<,>
	{
		PropertyInfo pi = closedIGrouping.GetProperty ("Key");
		object key = pi.GetValue (value, null);
		sb.Append ("Group with key=" + key + ": ");
	}

	if (value is IEnumerable)
		foreach (object element in ((IEnumerable)value))
			sb.Append (ToStringEx (element) + " ");

	if (sb.Length == 0) sb.Append (value.ToString());

	return "\r\n" + sb.ToString();
}
<Query Kind="Program" />

void Main()
{
	var somestate = State.none;
	var desc = somestate.GetDescription();
	Console.WriteLine(desc);
	Console.Write("hello");
}

public class DescriptionAttribute : Attribute
{
	public string Description { get; set; }

	public DescriptionAttribute(string description)
	{
		Description = description;
	}
}


public enum State
{
	[Description("تست اتربویت")]
	none,
	accpepted,
	rejected
}

public static class EnumExtensions
{
	public static string GetDescription(this State state)
	{
		var enumType = typeof(State);
		var memberInfos = enumType.GetMember(state.ToString());
		var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
		var valueAttributes =
			  enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
		var description = ((DescriptionAttribute)valueAttributes[0]).Description;
		return description;
	}
}
<Query Kind="Statements" />

BindingFlags publicStatic = BindingFlags.Public | BindingFlags.Static;
MemberInfo[] members = typeof (object).GetMembers (publicStatic);
members.Dump ("Public members", 1);

// The following example retrieves all the nonpublic members of type object, both static and instance:
BindingFlags nonPublicBinding =
	BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

MemberInfo[] nonPublic = typeof (object).GetMembers (nonPublicBinding);
nonPublic.Dump ("Non-public members", 1);
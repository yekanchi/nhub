<Query Kind="Statements" />

int i = (int)Activator.CreateInstance (typeof (int)).Dump ("i");

DateTime dt = (DateTime)Activator.CreateInstance (typeof (DateTime), 2000, 1, 1);
dt.Dump ("dt");


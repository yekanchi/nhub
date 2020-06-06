<Query Kind="Statements" />

ValueTuple<string,int> bob1 = ValueTuple.Create ("Bob", 23);
(string, int) bob2 = ValueTuple.Create ("Bob", 23);

bob1.Dump();
bob2.Dump();
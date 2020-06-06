<Query Kind="Statements" />

Object[] array = { "String", 12, true, 'a' };

var types =
  array
	.Select(item => item.GetType().Name)
	.OrderBy(type => type);

types.Dump();
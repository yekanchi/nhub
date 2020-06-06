<Query Kind="Statements" />

Type stringType = typeof (string);

string name = stringType.Name.Dump ("name");                // String
Type baseType = stringType.BaseType.Dump ("baseType", 1);   // typeof(Object)
Assembly assem = stringType.Assembly.Dump ("Assembly");     // System.Private.CoreLib
bool isPublic = stringType.IsPublic.Dump ("IsPublic");      // true

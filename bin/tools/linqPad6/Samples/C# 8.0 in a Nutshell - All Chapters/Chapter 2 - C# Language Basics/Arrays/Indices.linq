<Query Kind="Statements" />

char[] vowels = new char[] {'a','e','i','o','u'};
char lastElement  = vowels [^1].Dump();   // 'u'
char secondToLast = vowels [^2].Dump();   // 'o'

Index first = 0;
Index last = ^1;
char firstElement = vowels [first].Dump();   // 'a'
char lastElement2 = vowels [last].Dump();    // 'u'

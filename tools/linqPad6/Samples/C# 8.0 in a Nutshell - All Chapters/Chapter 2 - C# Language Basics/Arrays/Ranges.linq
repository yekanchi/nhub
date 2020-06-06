<Query Kind="Statements" />

char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

char[] firstTwo = vowels [..2].Dump();     // 'a', 'e'
char[] lastThree = vowels [2..].Dump();    // 'i', 'o', 'u'
char[] middleOne = vowels [2..3].Dump();   // 'i'

char[] lastTwo = vowels [^2..].Dump();     // 'o', 'u'

Range firstTwoRange = 0..2;
char[] firstTwo2 = vowels [firstTwoRange].Dump();   // 'a', 'e'

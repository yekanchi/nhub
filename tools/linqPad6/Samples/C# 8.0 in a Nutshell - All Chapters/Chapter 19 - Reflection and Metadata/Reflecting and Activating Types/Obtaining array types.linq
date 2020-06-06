<Query Kind="Statements" />

Type simpleArrayType = typeof (int).MakeArrayType();
Console.WriteLine (simpleArrayType == typeof (int[]));     // True

//MakeArrayType can be passed an integer argument to make multidimensional rectangular arrays:
Type cubeType = typeof (int).MakeArrayType (3);        // cube shaped
Console.WriteLine (cubeType == typeof (int [,,]));     // True

//GetElementType does the reverse: it retrieves an array type’s element type:
Type e = typeof (int[]).GetElementType().Dump(1);      // e == typeof (int)

//GetArrayRank returns the number of dimensions of a rectangular array:
int rank = typeof (int [,,]).GetArrayRank().Dump ("rank");   // 3

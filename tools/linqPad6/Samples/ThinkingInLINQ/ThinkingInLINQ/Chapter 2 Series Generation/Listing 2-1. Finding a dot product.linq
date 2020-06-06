<Query Kind="Statements">
</Query>

int[] v1 = {1,2,3}; //First vector
int[] v2 = {3,2,1}; //Second vector
//dot product of vector
v1.Zip(v2, (a,b) => a * b).Dump("Dot Product");
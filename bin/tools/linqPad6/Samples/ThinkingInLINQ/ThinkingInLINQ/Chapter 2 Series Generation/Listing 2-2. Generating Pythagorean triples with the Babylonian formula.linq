<Query Kind="Statements">
 
</Query>

Enumerable.Range(2,10)
			.Select (c => new {Length = 2*c, Height = c * c - 1, Hypotenuse = c * c + 1})
			.Dump("Pythagorean Triples");
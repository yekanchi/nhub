<Query Kind="Statements">
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>();
builder.Add(1);
builder.Add(2);
builder.Add(3);
builder.RemoveAt(0);
ImmutableArray<int> myImmutable = builder.ToImmutable();

myImmutable.Dump();

var builder2 = myImmutable.ToBuilder();
builder2.Add (4);      // Efficient
builder2.Remove (2);   // Efficient
// ...                  // More changes to builder...
// Return a new immutable collection with all the changes applied:
ImmutableArray<int> myImmutable2 = builder2.ToImmutable().Dump();


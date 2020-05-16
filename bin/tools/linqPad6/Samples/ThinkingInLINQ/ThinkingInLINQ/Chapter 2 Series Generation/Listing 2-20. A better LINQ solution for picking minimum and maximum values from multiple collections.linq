<Query Kind="Statements">
  
</Query>

List<List<int>> allValues = new List<List<int>>();
List<int> bidValues1 = new List<int>(){1,2,3,4,5};
List<int> bidValues2 = new List<int>(){2,1,4,5,6};
List<int> bidValues3 = new List<int>(){4,0,6,8,1};
List<int> bidValues4 = new List<int>(){9,2,4,1,6};
//Add all collections in this list of collections.
allValues.Add(bidValues1);
allValues.Add(bidValues2);
allValues.Add(bidValues3);
allValues.Add(bidValues4);
//Showing the maximum values compared at each location for 4 collections
allValues
.Aggregate((z1,z2) => z1.Zip(z2,(x,y) => Math.Max(x,y)).ToList())
.Dump("Maximum values : Generalized Approach");
//Showing the minimum values compared at each location for 4 collections
allValues
.Aggregate((z1,z2) => z1.Zip(z2,(x,y) => Math.Min(x,y)).ToList())
.Dump("Minimum values : Generalized Approach");

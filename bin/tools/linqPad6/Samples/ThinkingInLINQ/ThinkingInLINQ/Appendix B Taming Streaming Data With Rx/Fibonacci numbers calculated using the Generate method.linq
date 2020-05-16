<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

KeyValuePair<int,int> seed = new KeyValuePair<int,int>(0,1);
Observable.Generate
           (
				//Start with this seed value
				seed
				// Run it eternally
				,x=>true
				//Here is how to step through to go to the next one
				,x => new KeyValuePair<int,int>(x.Value,x.Key+x.Value)
				//Return the “Key” of the key value pair.
				,x => x.Key
			)
			.Take(10)
.Dump("First 10 Fibonacci numbers");
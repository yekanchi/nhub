<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
  
</Query>

var ebookPricesOne = Observable.Interval(TimeSpan.FromSeconds(1))
								.Select( x => new Random().NextDouble()*10)
								.Take(4);
var ebookPricesTwo = Observable.Interval(TimeSpan.FromSeconds(.5))
								.Select( x => new Random().NextDouble()*10)
								.Take(4);
ebookPricesOne.Dump("First Service");
ebookPricesTwo.Dump("Second Service");
Observable.Zip(ebookPricesOne, ebookPricesTwo)
          .Select(x => x.First() <= x.Last ( )? x.First ():x.Last ( ))
          .Dump("Cheapest e-book prices");
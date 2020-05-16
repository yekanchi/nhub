<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

var range1 = Observable.Range(11,5).Select(x => (long)x);
var inte1 = Observable.Interval(TimeSpan.FromSeconds(.5)).Take(5);
range1.Concat(inte1).Dump();
<Query Kind="Statements">
  
</Query>

List<int> bidValues1 = new List<int>(){1,2,3,4,5};
List<int> bidValues2 = new List<int>(){2,1,4,5,6};
bidValues1.Zip(bidValues2, (firstBid,secondBid) => Math.Max(firstBid,secondBid))
          .Dump("Maximum bids");
bidValues1.Zip(bidValues2, (firstBid,secondBid) => Math.Min(firstBid,secondBid))
          .Dump("Minimum bids");

<Query Kind="Statements">
  
</Query>

int boardSize = 3;
var range = Enumerable.Range(1,boardSize*boardSize);
List<List<int>> winningPaths = new List<List<int>>();
//Horizontal Paths
Enumerable.Range(0,boardSize)
		  .ToList()
	      .ForEach(k => winningPaths.Add(range.Skip(k*boardSize)
          .Take(boardSize).ToList()));
//Vertical Paths
Enumerable.Range(0,boardSize)
	      .ToList()
          .ForEach(k => winningPaths.Add(winningPaths.Take(boardSize)
	      .Select(p => p[k]).ToList()));
//Diagonal Paths
//Main diagonal
winningPaths.Add(range.Where((r,i) => i % (boardSize + 1) == 0).ToList());
//reverse diagonal
winningPaths.Add(range.Where ((r,i) => i % (boardSize - 1) == 0).Skip(1).Take(boardSize).ToList());
//printing all the paths; one path on each line.
winningPaths.Select(x => x.Select (z => z.ToString()).Aggregate((a,b)=> a.ToString () + " " + b.ToString() ))
            .Dump("All winning paths for a Tic-Tac-Toe board of size 3");

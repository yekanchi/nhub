<Query Kind="Statements">
  
</Query>

Stopwatch w = new Stopwatch();
w.Start();
List<int> Qs = new List<int>();
List<int> Qsp = new List<int>();
for (int i = 0; i < 2; i++)
	Qs = Enumerable.Range(1, 10000).Where(d => Enumerable.Range(2, d / 2)
				   .All(e => d % e != 0))
				   .ToList();
w.Stop();
double timeWithoutParallelization = w.Elapsed.TotalMilliseconds;
Stopwatch w2 = new Stopwatch();
w2.Start();
for (int i = 0; i < 2; i++)
	Qsp = Enumerable.Range(1, 10000).AsParallel().Where(d => Enumerable.Range(2, d / 2)
                                                                       .All(e => d % e != 0)).ToList();
w2.Stop();

double timeWithParallelization = w2.Elapsed.TotalMilliseconds;
double percentageGainInPerformance = (timeWithoutParallelization - timeWithParallelization) /timeWithoutParallelization;
bool isSame = Qs.SequenceEqual(Qsp);
isSame.Dump();

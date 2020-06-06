<Query Kind="Program">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

./*
		This code requires MoreLINQ. 
		Because it uses some of the functions from that framework 
		that have been covered in chapter 5
*/

void Main()
{
	int x = 10;
	List<Func<int,int>> steps = new List<Func<int,int>>();
	steps.Add( a => a + 1);
	steps.Add( a => a + 3);
	steps.Add( a => a - 4);
	steps.Add( a => 2*a - 1);

	x.Scan(steps).Dump("Scan");
	x.ScanBack(steps).Dump("Scanned Back");
	FSharpEx.Zip3(x.Scan(steps),x.ScanBack(steps),x.Scan(steps.Concat(steps.Skip(1))))
			.Dump("Zipped");

	x.Scan(steps).Iterate(a => Console.WriteLine("Score is " + a));

	int[] series = {1,2,3,4,5,6,7,8,9,10};

	//Check whether the given series is in AP
	bool isAPSeries = series.Pairwise()
							.Select (s => new {First = s.Key, Second = s.Value, Difference = s.Value - s.Key })
                            .All (s => s.Difference == series[1]-series[0]);

	isAPSeries.Dump("isAP using Pairwise");

	series.ForAll2((a,b) => b - a == series[1] - series[0])
		  .Dump("isAP using ForAll2");

	series.Exists2((a,b) => a + b >= 100 && a + b <= 200)
		  .Dump("Is there any such couple of elements");

	series.Pairwise().Dump("Items picked Pairwise");
	series.Partition(a => a % 2 == 0).Dump("Partitioned");
	int[] theseOnes = {1,3,52,2,1};
	int[] thatOnes = {4,5,2,1,3,4};
	int[] otherOnes = {2,3,1,1,3,14};
	FSharpEx.IntersectMany((new List<int[]>(){theseOnes, thatOnes, otherOnes }))
			.Dump("Intersect Many");
	FSharpEx.UnionMany((new List<int[]>(){theseOnes, thatOnes, otherOnes }))
			.Dump("Union Many");
}
 	public static class FSharpEx
    {
        /// <summary>
        /// This method generates a list of numbers from a given seed number
        /// and a list of functions that are used to generate the next number
        /// one step at a time.
        /// </summary>
        /// <typeparam name="T">The type of the seed and the function arguments</typeparam>
        /// <param name="x0">The seed value</param>
        /// <param name="projectors">The step descriptions in terms of Functions</param>
        /// <returns>A list of generated elements</returns>
        public static IEnumerable<T> Scan<T>(this T x0, IEnumerable<Func<T, T>> projectors)
        where T : IEquatable<T>
        {
            List<T> values = new List<T>();
            values.Add(x0);
            foreach (var f in projectors)
            {
                values.Add(f.Invoke(values.Last()));
            }
            return values.AsEnumerable();
        }
        /// <summary>
        /// This is same as Scan just that the functions provided are used in reverse order
        /// while generating the elements
        /// unlike Scan where the sequence of the functions are used as is.
        /// </summary>
        /// <typeparam name="T">The type of the collection and the seed value</typeparam>
        /// <param name="x0">The seed value</param>
        /// <param name="projectors">The step descriptions</param>
        /// <returns>A list of generated elements</returns>
        public static IEnumerable<T> ScanBack<T>(this T x0, IEnumerable<Func<T, T>> projectors)
        where T : IEquatable<T>
        {
            List<T> values = new List<T>();
            values.Add(x0);
            foreach (var f in projectors.Reverse())
            {
                values.Add(f.Invoke(values.Last()));
            }
            return values.AsEnumerable();
        }

        /// <summary>
        /// This method partitions the given collection into two parts.
        /// The first part contains elements for which the predicate returns true.
        /// The other part contains elements for which the predicate returns false.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A tuple with two ranges. The first range has the elements for
        /// which the predicate returns true and the second part returns elements
        /// for which the predicate returns false.</returns>
        public static Tuple<IEnumerable<T>, IEnumerable<T>> Partition<T>(
        this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return new Tuple<IEnumerable<T>, IEnumerable<T>>(
            collection.Where(c => predicate.Invoke(c)),
            collection.Where(c => !predicate.Invoke(c)));
        }
        /// <summary>
        /// Applies the given action for all elements of the given collection
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to be performed</param>
        public static void Iterate<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var v in collection)
                action.Invoke(v);
        }
        /// <summary>
        /// This method wraps three collections into one.
        /// </summary>
        /// <typeparam name="T1">The type of the first collection</typeparam>
        /// <typeparam name="T2">The type of the second collection</typeparam>
        /// <typeparam name="T3">The type of the third collection</typeparam>
        /// <param name="first">The first collection</param>
        /// <param name="second">The second collection</param>
        /// <param name="third">The third/last collection</param>
        /// <returns>A list of tuples where the items of the tuples are picked from the first,
        // second, and third collection,
        /// respectively.</returns>
        public static IEnumerable<Tuple<T1, T2, T3>> Zip3<T1, T2, T3>(IEnumerable<T1> first,
        IEnumerable<T2> second,
        IEnumerable<T3> third)
        {
            int smallest = (new List<int>() { first.Count(), second.Count(),third.Count() }).Min();
            for (int i = 0; i < smallest; i++)
                yield return new Tuple<T1, T2, T3>(first.ElementAt(i), second.ElementAt(i),
                third.ElementAt(i));
        }

        /// <summary>
        /// Returns the index of the given item in the given collection
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="predicate">The predicate to be used to search the given item</param>
        /// <returns>Returns the index of the given element in the collection, else returns -1
        /// if not found.</returns>
        public static int FindIndex<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            try
            {
                return collection.Select((c, i) => new KeyValuePair<int, bool>(i, predicate.Invoke(c)))
                .First(c => c.Value == true).Key;
            }
            catch (InvalidOperationException ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// Returns a list of consecutive items as a list of key/value pairs
        /// </summary>
        /// <typeparam name="T">The type of the input collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <returns>A list of key/alue pairs</returns>
        public static IEnumerable<KeyValuePair<T, T>> Pairwise<T>(
        this IEnumerable<T> collection)
        {
            return collection.Zip(collection.Skip(1), (a, b) => new KeyValuePair<T, T>(a, b));
        }
        /// <summary>
        /// Checks whether there is a pair of consecutive entries that matches
        /// the given condition
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="predicate">The predicate to use</param>
        /// <returns>True if such a pair exists that matches the given predicate pairwise
        /// else returns false</returns>
        public static bool Exists2<T>(this IEnumerable<T> collection,
        Func<T, T, bool> predicate)
        {
            return collection.Zip(collection.Skip(1), (a, b) =>
            predicate.Invoke(a, b)).Any(c => c == true);
        }
        /// <summary>
        /// Checks whether all pairwise items (taken 2 at a time) from the given collection
        /// matches the predicate or not
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="predicate">The predicate to run against all pairwise coupled
        /// items.</param>
        /// <returns></returns>
        public static bool ForAll2<T>(this IEnumerable<T> collection,
        Func<T, T, bool> predicate)
        {
            return collection.Zip(collection.Skip(1), (a, b) => predicate.Invoke(a, b))
            .All(c => c == true);
        }
        /// <summary>
        /// Finds intersection of several collections
        /// </summary>
        /// <typeparam name="T">type of these collections</typeparam>
        /// <param name="sets">all collections</param>
        /// <returns>A list with all the elements that appear in the intersection of
        /// all these collections</returns>
        public static IEnumerable<T> IntersectMany<T>(this IEnumerable<IEnumerable<T>> sets)
        where T : IComparable
        {
            HashSet<T> temp = new HashSet<T>(sets.ElementAt(0));
            sets.ToList().ForEach(z => temp = new HashSet<T>(z.Intersect(temp)));
            return temp;
        }
        /// <summary>
        /// Finds the union of several collections.
        /// </summary>
        /// <typeparam name="T">The type of these collections</typeparam>
        /// <param name="sets">All the collections, not just sets</param>
        /// <returns>A collection of elements with all the elements in the total union</returns>
        public static IEnumerable<T> UnionMany<T>(this IEnumerable<IEnumerable<T>> sets)
        where T : IComparable
        {
            HashSet<T> allValues = new HashSet<T>();
            sets.SelectMany(s => s).ToList().ForEach(z => allValues.Add(z));
            return allValues;
        }
    }

// Define other methods and classes here


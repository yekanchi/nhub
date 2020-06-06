<Query Kind="Program">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  <Reference>C:\MoreLINQ\MoreLinq.dll</Reference>
  <Reference>C:\QuickBencher.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Reference Assemblies\Microsoft\Roslyn\v1.2\Roslyn.Compilers.CSharp.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Reference Assemblies\Microsoft\Roslyn\v1.2\Roslyn.Compilers.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2012\Projects\RxEx\packages\Rx-Core.2.2.5\lib\net45\System.Reactive.Core.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2012\Projects\RxEx\packages\Rx-Interfaces.2.2.5\lib\net45\System.Reactive.Interfaces.dll</Reference>
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2012\Projects\RxEx\packages\Rx-Linq.2.2.5\lib\net45\System.Reactive.Linq.dll</Reference>
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

/*
	This script requires Evaluant LINQ Compiler to run.
	You can get Evaluant LINQ Compiler at 

	https://linqcompiler.codeplex.com/
	
	Once you get the API
	add a reference to LINQPad and add the namespace "Evaluant.Linq.Compiler" 
	For your convenience the dll is also copied in the Chapter 6 folder.
	
*/

private static string SanitizeBraces(string generatedStatement)
{
	int gap = generatedStatement.ToCharArray().Count(c => c == '(') - generatedStatement.ToCharArray().Count(c => c == ')');
	if (gap == 0)
		return generatedStatement;
	else
		return generatedStatement + new string(')', gap);
}
 private static List<string> GetTokens(string phrase)
 {
            string[] specialOnes = {"are-same","digits-at"};
            if(specialOnes.Any (o => phrase.EndsWith(o)))
			{
				 return phrase.Split(new string[] { "of", "the", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
			}
			else 
			{
	            Stack<string> tokens = new Stack<string>();
	
    	        phrase.Split(new string[] { "of", "the", " " }, StringSplitOptions.RemoveEmptyEntries)
        	          .ToList()
            	      .ForEach(t =>  tokens.Push(t));
		    	tokens.Dump();
            	return tokens.ToList();
			}
}
private static string GenerateArmStrongStatement(List<string> tokens)
{
	Dictionary<string, string> mapping = new Dictionary<string, string>();
	mapping.Add("*", "*");
	mapping.Add("times", "*");
	mapping.Add("(", ")");
	mapping.Add(")", "(");
	mapping.Add("are-same", ".IsSame()");
	mapping.Add("==", "==");
	mapping.Add("proper-divisors", ".ProperDivisors()");
	mapping.Add("even-digits", ".EvenDigits()");
	mapping.Add("odd-digits", ".OddDigits()");
	mapping.Add("number", "n");
	mapping.Add("square", ".Square()");
	mapping.Add("product", ".Product()");
	mapping.Add("is", "==");
	mapping.Add("!=", "!=");
	mapping.Add("+", "+");
	mapping.Add("-", "-");
	mapping.Add("and", "&&");
	mapping.Add("or", "||");
	mapping.Add("/", "/");
	mapping.Add(">", "<");
	mapping.Add("<", ">");
	mapping.Add("<=", ">=");
	mapping.Add(">=", "<=");
	mapping.Add("divided-by", "/");
	mapping.Add("are", ".Are(");
	mapping.Add("digits", ".Digits()");
	mapping.Add("reverse-digits",".ReverseDigits()");
	mapping.Add("cube", ".Cube()");
	mapping.Add("factorial", ".Factorial()");
	mapping.Add("sum", ".Sum()");
	//Add all normal LINQ operators
	mapping.Add("average", ".Average()");
	mapping.Add("maximum", ".Max()");
	mapping.Add("minimum", ".Min()");
	mapping.Add("digits-at", ".DigitsAt(");
	StringBuilder armstrongBuilder = new StringBuilder();
	foreach (string to in tokens)
	{
		if (mapping.ContainsKey(to))
			armstrongBuilder.Append(mapping[to]);
		if (to.ToCharArray().All(t => Char.IsNumber(t) || t == '.'))
			armstrongBuilder.Append(to);
	}
	return SanitizeBraces("input.Where ( n => " + armstrongBuilder.ToString() + ")");
}
public static class IntEx
	{
		public static int Cube(this int number)
        {
            return number * number * number;
        }
		public static IEnumerable<int> Cube(this IEnumerable<int> digits)
		{
			return digits.Select (d =>d.Cube() );
		}
        public static int Square(this int number)
        {
            return number * number;
        }

        public static IEnumerable<int> Digits(this int number)
        {
            return number.ToString().ToCharArray()
            .Select(n => Convert.ToInt32(n.ToString()));
        }
        public static IEnumerable<int> ReverseDigits(this int number)
        {
            return number.Digits().Reverse();
        }
        public static IEnumerable<int> EvenDigits(this int number)
        {
            return number.ToString().ToCharArray()
            .Where((m, i) => i % 2 == 0).Select(n => Convert.ToInt32(n.ToString()));
        }
        public static IEnumerable<int> OddDigits(this int number)
        {
            return number.ToString().ToCharArray()
            .Where((m, i) => i % 2 != 0).Select(n => Convert.ToInt32(n.ToString()));
        }
        public static bool Are(this IEnumerable<int> actualDigits, params int[] digits)
        {
            return actualDigits.SequenceEqual(digits);
        }
        public static IEnumerable<int> DigitsAt(this int number, params int[] indices)
        {
            var asString = number.ToString();
            return indices.Select(i => Convert.ToInt32(asString[i].ToString()));
        }
        public static bool AreZero(this IEnumerable<int> digits)
        {
            return digits.All(d => d == 0);
        }
        public static int FormNumber(this IEnumerable<int> digits)
        {
            return digits.Select((d, i) => d * (int)Math.Pow(10, digits.Count() - (i + 1)))
            .Aggregate((a, b) => a + b);
        }
        public static IEnumerable<int> Factorial(this IEnumerable<int> digits)
        {
            foreach (var d in digits)
                if (d == 0)
                    yield return 1;
                else
                    yield return Enumerable.Range(1, d).Aggregate((a, b) => a * b);
        }
        public static int Product(this IEnumerable<int> digits)
        {
            return digits.Aggregate((f, s) => f * s);
        }
	}
void Main()
{
do
{
		var inputs = Enumerable.Range(1, 10000);
		Console.WriteLine("Armstrong >>");
		string line = Console.ReadLine()
							.Replace("(", "( ")
							.Replace(")", " )");
		string statement = GenerateArmStrongStatement(GetTokens(line));
		LinqCompiler lc = new LinqCompiler(statement);
		lc. ExternalAssemblies .Add(typeof(IntEx).Assembly);
		///lc.ExternalAssemblies.Add(typeof(MathEx).Assembly);
		lc. AddSource ("input", inputs);
		line.Dump("Armstrong Query Expressed in Plain English");
		statement.Dump("Generated LINQ Query");
		lc.Evaluate().Dump("Answers");
	} while (true);
}
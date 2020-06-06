<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

//Parsing Survey Monkey Results
string result = @"Will you come by bus
					No
					Name
					Sam
					Phone number
					1234
					Will you come by bus
					Yes
					Name
					Ram
					Phone number
					3213
					Will you come by bus
					Yes
					Name
					Raul
					Phone number
					4245";
string[] questions = {"Will you come by bus","Name","Phone number"};
var allResponses = result.Split(questions,StringSplitOptions.RemoveEmptyEntries)
						 .Select (r => r.Trim());
int numberOfResponses = allResponses.Count ()/questions.Length;

string csv =
	//Headers
	questions
	.Select (q => "\"" + q + "\"" )
	.Aggregate ((h1,h2) => h1 + "," + h2 ) +
	//Insert Newline
	Environment.NewLine +
	//Rows
	Enumerable.Range(0,numberOfResponses)
			  .Select (e => allResponses.Skip(e*questions.Length).Take(questions.Length))
	          .Select (e => Enumerable.Range(0,questions.Length)
	          .Select (en => e.ElementAt(en) ))
	          .Select (e => e.Select (x => "\"" + x + "\"")
	          .Aggregate ((m,n) => m + "," + n ))
	          .Aggregate ((a,b) => a + Environment.NewLine + b);
csv.Dump("CSV representation");

<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Func<IEnumerable<string>,int,IEnumerable<string>> TakeLast = (list, count) => list.Skip(list.Count()-count);
//Cloning head
Func<string,int,IEnumerable<string>> Head = (fileName, lineCount)=> File.ReadAllLines(fileName).Take(lineCount);
//Cloning tail
Func<string,int,IEnumerable<string>> Tail = (fileName, lineCount)=> TakeLast(File.ReadAllLines(fileName),lineCount);
Head("C:\\conf.txt",4).Dump();
Tail("C:\\conf.txt",4).Dump();

<Query Kind="Statements">
  
</Query>

//Assume the answer we want to reach is "10"
int answer = 10 ;
//And we want to create 10 using 1, 2, 4 and 9
List<int> set = new List<int>() {1,2,4,9};
List<KeyValuePair<int,string>> query = set.SelectMany ((s,i) => set.Where (se => se!=s).Select (se => new KeyValuePair<int,string>(se+s,se.ToString()+"+"+s.ToString())))
                                          .ToList();
query.AddRange(set.SelectMany ((s,i) => set.Where (se => se!=s).Select (se => new KeyValuePair<int,string>(se*s,se.ToString()+"*"+s.ToString()))));
query.AddRange(set.SelectMany ((s,i) => set.Where (se => se!=s).Select (se => new KeyValuePair<int,string>(se-s,se.ToString()+"-"+s.ToString()))));

List<string> expressions = new List<string>();
for(int i=0;i<query.Count();i++)
{
	for(int j=0;j<query.Count ();j++)
	{
		if(i!=j)
		{
			if(!Regex.Matches(query[i].Value,"[0-9]")
					 .Cast<Match>()
                     .Select (m =>Convert.ToInt16(m.Value))
                     .OrderBy (m => m)
                     .Any(z => Regex.Matches(query[j].Value,"[0-9]")
                     .Cast<Match>()
                     .Select (m =>Convert.ToInt16(m.Value))
                     .OrderBy (m => m)
                    .Contains(z)))
			{
					if(query[i].Key + query[j].Key == answer)
				    {
                           expressions.Add("(" + query[i].Value + ") + (" + query[j].Value +")");
                           break;
                    }
                    if(query[i].Key - query[j].Key == answer)
                    {
                           expressions.Add("(" + query[i].Value + ") - (" + query[j].Value +")");
                           break;
                    }
                    if(query[i].Key * query[j].Key == answer)
                    {
                           expressions.Add("(" + query[i].Value + ")* (" + query[j].Value +")");
                           break;
                    }
            }
       }
   }
}
expressions.Dump("Expressions");

<Query Kind="Statements" />

string keyPad = @"2 = ABC2
					3 = DEF3
					4 = GHI4
					5 = JKL5
					6 = MNO6
					7 = PQRS7
					8 = TUV8
					9 = WXYZ9";
Dictionary<char,char> keyMap = new Dictionary<char,char>();
//4663 can lead to "good","home" etc
string key = "4663";
//"735328";//select/reject
//"select" and "reject" can be typed using the key combination "735328"
List<KeyValuePair<string,string>> keyAndLetters =
keyPad.ToLower()
	  .Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries)
      .Select
      (
			p =>
			 new KeyValuePair<string,string>(p.Split('=')[0].Trim(),
			                                p.Split('=')[1].Trim()))
			.ToList();
			foreach (var keyL in keyAndLetters)
			{
				foreach (char c in keyL.Value.ToCharArray())
				keyMap.Add(c,Convert.ToChar(keyL.Key));
			}
		//Environment.UserName.Dump();
		StreamReader sr = new StreamReader ( String.Format(@"C:\users\{0}\Documents\LINQPad Queries\Thinking in LINQ\Chapter 3\T9.txt",Environment.UserName));
		string total = sr.ReadToEnd();
		sr.Close();
		var query = total
                        .Split(new char[]{'\r','\n',' '},StringSplitOptions.RemoveEmptyEntries)
                        .Where (t => t.Length==key.Length)
                        .Select (t => t.Trim());
query
   .ToList()
   .Select(w=> new KeyValuePair<string,string>(w,new string(w.ToCharArray().Select (x => keyMap[x]).ToArray())))
   .Where (w => w.Value==key)
   .Select (w => w.Key)
   .Dump("Word suggestions");
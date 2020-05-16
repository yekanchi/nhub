<Query Kind="Program">
  
</Query>

        
		//Peter Norvig's spell checker in C#
		static Dictionary<string, int> NWords = new Dictionary<string, int>();
        public static IEnumerable<string> Edits1(string word)
        {
            char[] alphabet = {'a','b','c','d','e','f','g','h','j','k','l','m','n','o',
                               'p','q','r','s','t','u','v','w','x','y','z'};
            var splits = Enumerable.Range(1, word.Length-1)
            .Select(i =>
            new
            {
                First = word.Substring(0, i),
                Second = word.Substring(i + 1)
            });
            var deletes = splits.Where(split => !string.IsNullOrEmpty(split.Second))
            .Select(split => split.First + split.Second.Substring(1));
            var transposes = splits
            .Where(split => split.Second.Length > 1)
            .Select(split => split.First + split.Second[1] + split.Second[0]
            + split.Second.Substring(2));
            var replaces = splits
            .Where(split => !string.IsNullOrEmpty(split.Second))
            .SelectMany(split => alphabet
            .Select(c => split.First + c + split.Second.Substring(1)));
            var inserts = splits
            .Where(split => !string.IsNullOrEmpty(split.Second))
            .SelectMany(split => alphabet
            .Select(c => split.First + c + split.Second));
            return deletes
            .Union(transposes)
            .Union(replaces)
            .Union(inserts);
        }
        public static Dictionary<string, int> Train(IEnumerable<string> features)
        {
            Dictionary<string, int> model = new Dictionary<string, int>();
            features
            .ToList()
            .ForEach(f => { if (model.ContainsKey(f)) model[f] += 1; else model.Add(f, 1); });
            return model;
        }
        public static IEnumerable<string> KnownEdits2(string word)
        {
            List<string> knownEdits2 = new List<string>();
            return Edits1(word)
            .SelectMany(e1 => Edits1(e1)
            .Where(x => NWords.ContainsKey(x)));
        }
        public static IEnumerable<string> Known(IEnumerable<string> words)
        {
            return words.Intersect(NWords.Select(v => v.Key));
        }
        public static IEnumerable<string> Correct(string word)
        {
            List<string> candidates = new List<string>();
            candidates.AddRange(Known(new List<string>() { word }));
            candidates.AddRange(Known(Edits1(word)));
            candidates.AddRange(Known(Edits1(word)));
            candidates.AddRange(KnownEdits2(word));
            candidates.Add(word);
            return candidates
            .Where(c => NWords.ContainsKey(c)).OrderByDescending(c => NWords[c]);
        }

        public static void Main()
        {
            StreamReader sr = new StreamReader("C:\\T9.txt");
            string total = sr.ReadToEnd();
            sr.Close();
            NWords = Train(Regex.Matches(total, "[a-z]+")
            			.Cast<Match>()
            			.Select(m => m.Value.ToLower()));
            string word = "mysgtry"; //should return "mystery"
            Correct(word)
            	.Distinct()
            	.OrderByDescending(x => x.Length)
            	.Dump("Did you mean?");
        }


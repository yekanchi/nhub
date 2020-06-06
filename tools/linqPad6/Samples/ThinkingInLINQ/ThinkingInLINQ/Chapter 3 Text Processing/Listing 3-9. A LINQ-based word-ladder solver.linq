<Query Kind="Program">
  
</Query>

  public static int HammingDistance(string first, string second)
        {
            return first.ToCharArray().Where((f, i) => second[i] != f).Count();
        }
        void Main()
        {
            List<string> transitions = new List<string>();
            List<string> allWords = new List<string>();
            StreamReader t9Reader = new StreamReader(@"C:\T9.txt");
            string total = t9Reader.ReadToEnd();
            t9Reader.Close();
            //Start and End words
            string start = "myth";
            string end = "fact";
            string startCopy = start;
            transitions.Add(start);
            allWords.AddRange(total.Split(new char[] { ' ', '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries));

            allWords = allWords.Where(word => word.Length == start.Length)
            .ToList();
            allWords.Add(end);
            Dictionary<string, List<string>> wordEditDistanceMap =
            allWords.ToLookup(w => w)
            .ToDictionary
            (
                //key selector
            w => w.Key,
                //value selector
            w => allWords.Where(a =>
            HammingDistance(a, w.Key) == 1).ToList()
            );
            //At this point we have the dictionary separated by edit distance 1
            bool noPath = false;
            List<string> currentList = new List<string>();
            do
            {
                string[] currents = wordEditDistanceMap[start]
                .Where(word => HammingDistance(word, end) ==
                wordEditDistanceMap[start].Min(c => HammingDistance(end, c))).ToArray();
                do
                {
                    foreach (string c in currents)
                    {
                        if (!currentList.Contains(c))
                        {
                            currentList.Add(c);
                            break;
                        }
                        if ((currents.Length == 1 && currentList.Contains(c)))
                        {
                            Console.WriteLine("There is no such path !");
                            noPath = true;
                            break;
                        }
                    }
                    if (noPath)
                        break;
                } while (currentList.Count == 0);
                if (noPath)
                    break;
                transitions.Add(currentList[currentList.Count - 1]);

                if (transitions.Count >= 2 && transitions[transitions.Count - 2] == transitions.Last())
                {
                    Console.WriteLine("There is no such path");
                    noPath = true;
                    break;
                }
                start = currentList[currentList.Count - 1];
            } while (!start.Equals(end) || noPath == true);
            if (!noPath)
                transitions.Dump("Transition");// from \"" + startCopy + "\" to \"" + end +"\"");
        }

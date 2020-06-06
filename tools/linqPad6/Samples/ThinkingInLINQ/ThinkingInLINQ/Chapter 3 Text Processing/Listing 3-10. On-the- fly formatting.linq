<Query Kind="Program">
  
</Query>

 		public Func<string, string> FormatLikeThis(string transformation)
        {
            string[] tokens = transformation.Split(new string[] { "=>" }, StringSplitOptions.
            RemoveEmptyEntries);
            string start = tokens[0];
            string end = tokens[1];
            Dictionary<int, char> insertCharMap = new Dictionary<int, char>();
            Enumerable.Range(0, end.Length).Where(k => !start.Contains(end[k]))
            .ToList()
            .ForEach(k => insertCharMap.Add(k, end[k]));
            Func<string, string> transformer = x =>
            {
                insertCharMap.ToList().ForEach(z => x = x.Insert(z.Key, z.Value.ToString()));
                return x;
            };
            return transformer;
        }
        void Main()
        {
            string[] someVals = { "234567890", "345678901", "456789012" };
            List<string> modifiedVals = new List<string>();
            var transformer = FormatLikeThis("123456789=>123-456-789");
            someVals.ToList().ForEach(k => modifiedVals.Add(transformer.Invoke(k)));
            someVals.Dump("Before");
            modifiedVals.Dump("After");
        }

<Query Kind="Program">
  
</Query>


#region Longest Common Subsequence
public static class StringEx
{
		/// <summary>
        /// Creates a score matrix that will be used in LongestCommonSubsequnce method
        /// </summary>
        /// <param name="x">First String</param>
        /// <param name="y">Second String</param>
        /// <returns></returns>
        private static int[,] CreateLCSMatrix(string x, string y)
        {


            int m = x.Length;
            int n = y.Length;
            int[,] T = new int[m, n];
            for (int i = 0; i < m; i++)
                T[i, 0] = 0;
            for (int i = 0; i < n; i++)
                T[0, i] = 0;
            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (x[i] == y[j])
                        T[i, j] = T[i - 1, j - 1] + 1;
                    else
                        T[i, j] = Math.Max(T[i, j - 1], T[i - 1, j]);
                }
            }
            return T;

        }
        /// <summary>
        /// Finds the longest commong sub-sequence of two Strings
        /// </summary>
        /// <param name="x">The first string</param>
        /// <param name="y">The second string</param>
        /// <returns></returns>
        /// <remarks>If you want to know about the logic,I recommend the following link
        /// http://www-igm.univ-mlv.fr/~lecroq/seqcomp/node4.html
        /// Prof. Lecroq has several other string processing document 
        /// those are quite interesting and amazing read. 
        /// </remarks>
        public static string LongestCommonSubsequence(this string x, string y)
        {

            List<char> lcs = new List<char>();
            StringBuilder sb = new StringBuilder();

            x = " " + x;
            y = " " + y;
            int[,] T = CreateLCSMatrix(x, y);

            int m = x.Length;
            int n = y.Length;
            int i = m - 1;
            int j = n - 1;
            //int k = T[m - 1, n - 1] - 1;
            while (i > 0 && j > 0)
            {
                if (T[i, j] == T[i - 1, j - 1] + 1 && x[i] == y[j])
                {
                    lcs.Add(x[i]);

                    i--;
                    j--;
                    //      k--;
                }
                else if (T[i - 1, j] > T[i, j - 1])
                {
                    i--;
                }
                else
                    j--;
            }

            for (int p = lcs.Count - 1; p >= 0; p--)
            {
                sb.Append(lcs[p].ToString());
            }

            return sb.ToString();
        }
}
#endregion
void Main()
{
		// Define other methods and classes here
		StreamReader sr = new StreamReader (String.Format(@"C:\users\{0}\Documents\LINQPad Queries\Thinking in LINQ\Chapter 3\T9.txt",Environment.UserName));
		string total = sr.ReadToEnd();
		sr.Close();
		List<string> suggestions = new List<string>();
		var query = total.Split(new char[]{'\r','\n',' '},StringSplitOptions.RemoveEmptyEntries)
		.Select (t => t.Trim());
		//should show understand. See the bold characters.
		string path = "ujnbvcderesdftrewazxcvbnhgfds";
		query.Where(word => StringEx.LongestCommonSubsequence(path,word).Equals(word))
			.OrderByDescending (word => word.Length)
			.ThenByDescending (word => word )
			.Take(4)//Show first 4 suggestions.
			.Dump("Suggestions");
}

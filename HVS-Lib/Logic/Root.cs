namespace Logic
{
    public static class Root
    {
        private static List<BorrowableItem> items = new List<BorrowableItem>();
        private static int levenshteinDistanceRange = 3;
        public static List<BorrowableItem> GetItems(Type type)
        {
            return items;
        }
        public static List<BorrowableItem> SearchByType(Type type)
        {
            return items.FindAll(_item => _item.GetType() == type);
        }

        public static async IAsyncEnumerable<BorrowableItem> SearchByTitle(string title)
        {
            foreach (var _item in items)
            {
                int distance = await Task.Run(() => LevenshteinDistance(_item.title, title));

                if (distance <= levenshteinDistanceRange)
                {
                    yield return _item;
                }
            }
        }


        //Die "LevenshteinDistance" Funktion ist KI-generiert
        private static int LevenshteinDistance(string toCheckWord, string goalWord)
        {
            int len1 = toCheckWord.Length;
            int len2 = goalWord.Length;

            int[,] dp = new int[len1 + 1, len2 + 1];

            for (int i = 0; i <= len1; i++)
                dp[i, 0] = i;
            for (int j = 0; j <= len2; j++)
                dp[0, j] = j;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int cost = (toCheckWord[i - 1] == goalWord[j - 1]) ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), 
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[len1, len2];
        }
    }
}

namespace Logic
{
    public static class UserRoot
    {
        public static List<User> users = new List<User>();

        public static List<UserBorrowItem> GetUserHistoryItem(Guid userId)
        {
            List<UserBorrowItem> returnHistory = new List<UserBorrowItem>();
            foreach(var item in Root.GetItems())
            {
                foreach(var history in item.history)
                {
                    if(history.userIdNameOfBorrower == userId)
                    {
                        returnHistory.Add(new UserBorrowItem()
                        {
                            id = Guid.NewGuid(),
                            itemHistory = history,
                            itemId = item.id,
                            itemName = item.title
                        });
                    }
                }
            }

            return returnHistory;
        }
    }

    public static class Root
    {
        private static List<BorrowableItem> items = new List<BorrowableItem>();
        private static int levenshteinDistanceRange = 3;
       
        public static List<BorrowableItem> GetItems()
        {
            return items;
        }
        public static List<BorrowableItem> SearchByType(Type type)
        {
            return items.FindAll(_item => _item.GetType() == type);
        }

        public static List<BorrowableItem> SearchByTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                return items.FindAll(_item => LevenshteinDistance(_item.title, title) <= levenshteinDistanceRange);
            }
            else return items;
        }

        public static List<BorrowableItem> SearchByTypeAndDescription(Type type, string title)
        {
            return SearchByTitle(title).FindAll(_item => _item.GetType() == type);
        }


        //Die "LevenshteinDistance" Funktion ist KI-generiert
        private static int LevenshteinDistance(string toCheckWord, string goalWord)
        {
            if (goalWord.ToLower().Contains(toCheckWord.ToLower())) return 0;
            if (toCheckWord.ToLower().Contains(goalWord.ToLower())) return 0;

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



        public static async Task InsertSampleData()
        {
            //Die Items wurden KI generiert
            items = new List<BorrowableItem>()
                {

                    new Book() { id = Guid.NewGuid(), author = "Samuel Nittbaur", publicationDate = DateTime.Now.AddYears(-10), title = "Die Geschichte des Programmierens" },
                    new Book() { id = Guid.NewGuid(), author = "Robert C. Martin", publicationDate = DateTime.Now.AddYears(-15), title = "Clean Code" },
                    new Book() { id = Guid.NewGuid(), author = "Martin Fowler", publicationDate = DateTime.Now.AddYears(-20), title = "Refactoring" },
                    new Book() { id = Guid.NewGuid(), author = "Eric Evans", publicationDate = DateTime.Now.AddYears(-18), title = "Domain-Driven Design" },
                    new Book() { id = Guid.NewGuid(), author = "Don Norman", publicationDate = DateTime.Now.AddYears(-25), title = "The Design of Everyday Things" },
                    new Book() { id = Guid.NewGuid(), author = "Kent Beck", publicationDate = DateTime.Now.AddYears(-22), title = "Test-Driven Development by Example" },
                    new Book() { id = Guid.NewGuid(), author = "Andrew Hunt, David Thomas", publicationDate = DateTime.Now.AddYears(-24), title = "The Pragmatic Programmer" },
                    new Book() { id = Guid.NewGuid(), author = "Steve McConnell", publicationDate = DateTime.Now.AddYears(-23), title = "Code Complete" },
                    new Book() { id = Guid.NewGuid(), author = "Fred Brooks", publicationDate = DateTime.Now.AddYears(-30), title = "The Mythical Man-Month" },
                    new Book() { id = Guid.NewGuid(), author = "Douglas Crockford", publicationDate = DateTime.Now.AddYears(-14), title = "JavaScript: The Good Parts" },
                    new Book() { id = Guid.NewGuid(), author = "Bjarne Stroustrup", publicationDate = DateTime.Now.AddYears(-27), title = "The C++ Programming Language" },
                    new Book() { id = Guid.NewGuid(), author = "Brian Kernighan, Dennis Ritchie", publicationDate = DateTime.Now.AddYears(-35), title = "The C Programming Language" },
                    new Book() { id = Guid.NewGuid(), author = "Scott Meyers", publicationDate = DateTime.Now.AddYears(-21), title = "Effective C++" },
                    new Book() { id = Guid.NewGuid(), author = "Jon Bentley", publicationDate = DateTime.Now.AddYears(-32), title = "Programming Pearls" },
                    new Book() { id = Guid.NewGuid(), author = "Harold Abelson, Gerald Jay Sussman", publicationDate = DateTime.Now.AddYears(-34), title = "Structure and Interpretation of Computer Programs" },
                    new Book() { id = Guid.NewGuid(), author = "Peter Seibel", publicationDate = DateTime.Now.AddYears(-13), title = "Coders at Work" },
                    new Book() { id = Guid.NewGuid(), author = "Gayle Laakmann McDowell", publicationDate = DateTime.Now.AddYears(-12), title = "Cracking the Coding Interview" },
                    new Book() { id = Guid.NewGuid(), author = "David Flanagan", publicationDate = DateTime.Now.AddYears(-16), title = "JavaScript: The Definitive Guide" },
                    new Book() { id = Guid.NewGuid(), author = "Karl Fogel", publicationDate = DateTime.Now.AddYears(-17), title = "Producing Open Source Software" },
                    new Book() { id = Guid.NewGuid(), author = "Richard P. Feynman", publicationDate = DateTime.Now.AddYears(-40), title = "Surely You're Joking, Mr. Feynman!" },


                    new DVD() { id = Guid.NewGuid(), director = "Christopher Nolan", title = "Interstellar", publicationDate = DateTime.Now.AddYears(-8), runtime = new TimeSpan(2, 10, 45) },
                    new DVD() { id = Guid.NewGuid(), director = "Denis Villeneuve", title = "Blade Runner 2049", publicationDate = DateTime.Now.AddYears(-6), runtime = new TimeSpan(2, 44, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Steven Spielberg", title = "Jurassic Park", publicationDate = DateTime.Now.AddYears(-31), runtime = new TimeSpan(2, 7, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "James Cameron", title = "Avatar", publicationDate = DateTime.Now.AddYears(-15), runtime = new TimeSpan(2, 42, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "George Lucas", title = "Star Wars: A New Hope", publicationDate = DateTime.Now.AddYears(-47), runtime = new TimeSpan(2, 1, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Ridley Scott", title = "Alien", publicationDate = DateTime.Now.AddYears(-45), runtime = new TimeSpan(1, 57, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Peter Jackson", title = "The Lord of the Rings: The Fellowship of the Ring", publicationDate = DateTime.Now.AddYears(-23), runtime = new TimeSpan(2, 58, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Francis Ford Coppola", title = "The Godfather", publicationDate = DateTime.Now.AddYears(-52), runtime = new TimeSpan(2, 55, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Quentin Tarantino", title = "Pulp Fiction", publicationDate = DateTime.Now.AddYears(-30), runtime = new TimeSpan(2, 34, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Stanley Kubrick", title = "2001: A Space Odyssey", publicationDate = DateTime.Now.AddYears(-56), runtime = new TimeSpan(2, 29, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "The Wachowskis", title = "The Matrix", publicationDate = DateTime.Now.AddYears(-25), runtime = new TimeSpan(2, 16, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "David Fincher", title = "Fight Club", publicationDate = DateTime.Now.AddYears(-26), runtime = new TimeSpan(2, 19, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Christopher Nolan", title = "Inception", publicationDate = DateTime.Now.AddYears(-14), runtime = new TimeSpan(2, 28, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Martin Scorsese", title = "The Wolf of Wall Street", publicationDate = DateTime.Now.AddYears(-11), runtime = new TimeSpan(3, 0, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Frank Darabont", title = "The Shawshank Redemption", publicationDate = DateTime.Now.AddYears(-30), runtime = new TimeSpan(2, 22, 0) },
                    new DVD() { id = Guid.NewGuid(), director = "Steven Spielberg", title = "Schindler's List", publicationDate = DateTime.Now.AddYears(-31), runtime = new TimeSpan(3, 15, 0) }
                };

        }
    }
}

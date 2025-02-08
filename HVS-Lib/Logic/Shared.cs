namespace Logic
{
    public class BorrowableItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public string title { get; set; } = String.Empty;
        public DateTime publicationDate { get; set; } = DateTime.MinValue;
        public List<WaitLineItem> waitLine { get; set; } = new List<WaitLineItem>();
        public List<BorrowHistoryItem> history { get; set; } = new List<BorrowHistoryItem>();

        public bool CheckIfCurrentlyBorrowed()
        {
            return history.FindAll(_history => !_history.gaveBack)
                            .FindAll(_history => _history.startBorrowTime <= DateTime.Now && _history.startBorrowTime != DateTime.MinValue)
                            .FindAll(_history => _history.endBorrowTime > DateTime.Now && _history.endBorrowTime != DateTime.MinValue)
                            .FindAll(_history => _history.gaveBackTime > DateTime.Now && _history.gaveBackTime != DateTime.MinValue)
                            .Count() != 0;
        }
    }

    public class Book : BorrowableItem
    {
        public string author { get; set; } = String.Empty;
    }

    public class DVD : BorrowableItem
    {
        public TimeSpan runtime { get; set; } = new TimeSpan();
        public string director { get; set; } = String.Empty;
        public DateTime publicationDate { get; set; } = DateTime.MinValue;
    }

    public class WaitLineItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid userIdNameOfBorrower { get; set; } = Guid.Empty;
        public string userNameOfBorrower { get; set; } = String.Empty;
        public DateTime startBorrowTime { get; set; } = DateTime.MinValue;
        public DateTime endBorrowTime { get; set; } = DateTime.MinValue;
    }

    public class BorrowHistoryItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid userIdNameOfBorrower { get; set; } = Guid.Empty;
        public string userNameOfBorrower { get; set; } = String.Empty;
        public DateTime startBorrowTime { get; set; } = DateTime.MinValue;
        public DateTime endBorrowTime { get; set; } = DateTime.MinValue;
        public DateTime gaveBackTime { get; set; } = DateTime.MinValue;
        public bool gaveBack { get; set; } = false;
    }
}

using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using System;

namespace Logic
{
    public interface IObserver
    {
        void Update(BorrowableItem item);
    }
    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
    public class BorrowableItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public string title { get; set; } = String.Empty;
        public DateTime publicationDate { get; set; } = DateTime.MinValue;
        public List<WaitLineItem> waitLine { get; set; } = new List<WaitLineItem>();
        public List<BorrowHistoryItem> history { get; set; } = new List<BorrowHistoryItem>();
        private List<IObserver> observers { get; set; } = new List<IObserver>();
        public void AddObserver(IObserver observer) => observers.Add(observer);
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }
        public bool CheckIfCurrentlyBorrowed()
        {
            return waitLine.Count() != 0;
        }

        public bool CheckIfNextWaiterPickedUp()
        {
            if (waitLine.Count() == 0) return false;
            else
            {
                return !waitLine.First().pickedUp;
            }
        }

        public bool IsUserInWaitingList(Guid userId)
        {
            return waitLine.Exists(_user => _user.userIdNameOfBorrower == userId);
        }

        public bool GaveItemBack()
        {
            try
            {
                if (waitLine.Count() == 0)
                {
                    return false;
                }
                else
                {
                    WaitLineItem waitLineItem = waitLine.First();
                    history.Add(
                        new BorrowHistoryItem()
                        {
                            id = waitLineItem.id,
                            startBorrowTime = waitLineItem.startBorrowTime,
                            gaveBack = true,
                            gaveBackTime = DateTime.Now,
                            userIdNameOfBorrower = waitLineItem.userIdNameOfBorrower,
                            userNameOfBorrower = waitLineItem.userNameOfBorrower

                        });
                    waitLine.Remove(waitLineItem);

                    NotifyObservers();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PickUpItem()
        {
            try
            {
                if (waitLine.Count() != 0)
                {
                    WaitLineItem item = waitLine.First();
                    item.pickedUp = true;
                    item.startBorrowTime = DateTime.Now;
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool BookItem(Guid userId, string userName)
        {
            try
            {
                waitLine.Add(new WaitLineItem()
                {
                    id = Guid.NewGuid(),
                    userIdNameOfBorrower = userId,
                    userNameOfBorrower = userName
                });
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UnBookItem(Guid userId)
        {
            try
            {
                if (IsUserInWaitingList(userId))
                {
                    waitLine.RemoveAll(_user => _user.userIdNameOfBorrower == userId);
                    NotifyObservers();
                    return true;
                }
                else return false;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
    public class NotificationService : IObserver
    {
        public void Update(BorrowableItem item)
        {
            //Send E-Mail
        }
    }

    [Description("Buch")]
    public class Book : BorrowableItem
    {
        public string author { get; set; } = String.Empty;
    }

    [Description("DVD")]
    public class DVD : BorrowableItem
    {
        public TimeSpan runtime { get; set; } = new TimeSpan();
        public string director { get; set; } = String.Empty;
    }

    [Description("Brett-Spiel")]
    public class BoardGame : BorrowableItem
    {
        public bool isComplete { get; set; } = false;
    }

    public class WaitLineItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid userIdNameOfBorrower { get; set; } = Guid.Empty;
        public string userNameOfBorrower { get; set; } = String.Empty;
        public DateTime startBorrowTime { get; set; } = DateTime.MinValue;
        public bool pickedUp { get; set; } = false;
    }

    public class BorrowHistoryItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid userIdNameOfBorrower { get; set; } = Guid.Empty;
        public string userNameOfBorrower { get; set; } = String.Empty;
        public DateTime startBorrowTime { get; set; } = DateTime.MinValue;
        public DateTime gaveBackTime { get; set; } = DateTime.MinValue;
        public bool gaveBack { get; set; } = false;
    }

    public class User
    {
        public Guid id { get; set; } = Guid.Empty;
        public string name { get; set; } = String.Empty;
        public Dictionary<Guid, Guid> bookHistory { get; set; } = new Dictionary<Guid, Guid>(); //BookId and HistoryID
    }

    public class UserBorrowItem
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid itemId { get; set; } = Guid.Empty;
        public string itemName { get; set; } = String.Empty;
        public BorrowHistoryItem itemHistory { get; set; } = new BorrowHistoryItem();
    }
}
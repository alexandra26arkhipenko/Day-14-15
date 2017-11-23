using System;

namespace BusinessLogic.Interfaces.Entities
{
    public abstract class Account
    {
        private string _firstName;
        private string _id;
        private string _lastName;

        #region Constructor

        protected Account(string id, string firstName, string lastName, decimal amount, int points)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Amount = amount;
            Points = points;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Account №{0}\n Owner: {1} {2} \n Amount: {3}$  points:{4} ",
                Id, FirstName, LastName, Amount, Points);
        }

        public abstract int CalculatePointsForAddAmount(int bonusValue);
        public abstract int CalculatePointsForDivAmount(int bonusValue);

        public void AddMoney(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException(nameof(money));
            }

            Amount += money;
            Points += CalculatePointsForAddAmount(BonusValue);
        }

        public void DivMoney(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException(nameof(money));
            }

            if (Amount <= money)
            {
                throw new ArgumentException(nameof(money));
            }

            Amount -= money;
            Points -= CalculatePointsForAddAmount(BonusValue);
        }

        #region properties

        public string Id
        {
            get => _id;

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();
                _lastName = value;
            }
        }

        public decimal Amount { get; set; }
        public int Points { get; set; }
        public int BonusValue { get; set; }

        #endregion

    }
}
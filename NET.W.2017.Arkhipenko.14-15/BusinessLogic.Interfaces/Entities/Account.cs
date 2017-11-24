using System;

namespace BusinessLogic.Interfaces.Entities
{
    public abstract class Account
    {
        #region private fields
        private string _firstName;
        private string _id;
        private string _lastName;
        #endregion

        #region ctor

        protected Account(string id, string firstName, string lastName, decimal amount, int points)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Amount = amount;
            Points = points;
        }

        #endregion

        #region public
        /// <summary>
        /// Override ToString from Object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Account №{0}\n Owner: {1} {2} \n Amount: {3}$  points:{4} ",
                Id, FirstName, LastName, Amount, Points);
        }

        /// <summary>
        /// abstract method that calculate bonus points for account
        /// </summary>
        /// <param name="bonusValue"></param>
        /// <returns></returns>
        public abstract int CalculatePointsForAddAmount(int bonusValue);
        public abstract int CalculatePointsForDivAmount(int bonusValue);

        /// <summary>
        ///  Add money to accout's amount and add bonus points
        /// </summary>
        /// <param name="money">money that we add</param>
        public void AddMoney(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException(nameof(money));
            }

            Amount += money;
            Points += CalculatePointsForAddAmount(BonusValue);
        }

        /// <summary>
        /// Withdraw money from accout's amount and add bonus points
        /// </summary>
        /// <param name="money">money that we withdraw</param>
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

#endregion

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
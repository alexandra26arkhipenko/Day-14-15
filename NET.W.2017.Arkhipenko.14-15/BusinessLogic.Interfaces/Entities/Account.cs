using System;

namespace BusinessLogic.Interfaces
{
    public abstract class Account
    {
        private string id;
        private string firstName;
        private string lastName;

        #region properties
        public string Id
        {
            get { return id; }

            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }
                id = value;
            }
        }
        public string FirstName
        {
            get { return firstName; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }
                id = value;
            }
        }
        public decimal Amount { get; set; }
        public int Points { get; set; }
        public int BonusValue { get; set; }
        #endregion

        #region Constructor
        public Account(string id, string firstName, string lastName, decimal amount, int points)
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
            return String.Format("Account №{0}\n Owner: {1} {2} \n Amount: {3}$  points:{4} ",
                Id, FirstName, LastName, Amount, Points); 
        }

        public abstract int CalculatePointsForAddAmount(decimal amount, int bonusValue);
        public abstract int CalculatePointsForDivAmount(decimal amount, int bonusValue);

            
    }
}

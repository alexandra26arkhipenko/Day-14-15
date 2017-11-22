using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces.Entities
{
    class BaseAccount : Account
    {
        public BaseAccount(string id, string firstName, string lastName, decimal amount, int points)
        :base(id, firstName, lastName, amount, points)
        {
            this.BonusValue = 1;
        }

        public override string ToString() => "BaseAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(decimal amount, int bonusValue) => 10 * bonusValue;
        public override int CalculatePointsForDivAmount(decimal amount, int bonusValue) => 10 * bonusValue;
        
    }
}

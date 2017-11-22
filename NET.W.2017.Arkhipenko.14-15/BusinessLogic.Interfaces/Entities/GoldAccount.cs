using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces.Entities
{
    class GoldAccount :Account 
    {
        public GoldAccount(string id, string firstName, string lastName, decimal amount, int points)
        :base(id, firstName, lastName, amount, points)
        {
            this.BonusValue = 2;
        }

        public override string ToString() => "BaseAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(decimal amount, int bonusValue) => 20 * bonusValue + 5;
        public override int CalculatePointsForDivAmount(decimal amount, int bonusValue) => 20 * bonusValue + 10;
    }
}

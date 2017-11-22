using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces.Entities
{
    class PlatinumAccount : Account
    {
        public PlatinumAccount(string id, string firstName, string lastName, decimal amount, int points)
        :base(id, firstName, lastName, amount, points)
        {
            this.BonusValue = 3;
        }

        public override string ToString() => "BaseAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(decimal amount, int bonusValue) => 30 * bonusValue + 20;
        public override int CalculatePointsForDivAmount(decimal amount, int bonusValue) => 30 * bonusValue + 20;
    }
}

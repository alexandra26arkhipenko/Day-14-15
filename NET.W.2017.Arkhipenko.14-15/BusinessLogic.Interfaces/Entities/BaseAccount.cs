

namespace BusinessLogic.Interfaces.Entities
{
    public class BaseAccount : Account
    {
        public BaseAccount(string id, string firstName, string lastName, decimal amount, int points, string email)
        :base(id, firstName, lastName, amount, points, email)
        {
            BonusValue = 1;
        }

        public override string ToString() => "BaseAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(int bonusValue) => 10 * bonusValue;
        public override int CalculatePointsForDivAmount(int bonusValue) => 10 * bonusValue;
        
    }
}

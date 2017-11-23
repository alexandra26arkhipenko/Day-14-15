namespace BusinessLogic.Interfaces.Entities
{
    public class GoldAccount :Account 
    {
        public GoldAccount(string id, string firstName, string lastName, decimal amount, int points)
        :base(id, firstName, lastName, amount, points)
        {
            BonusValue = 2;
        }

        public override string ToString() => "GoldAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(int bonusValue) => 20 * bonusValue + 5;
        public override int CalculatePointsForDivAmount(int bonusValue) => 20 * bonusValue + 10;
    }
}

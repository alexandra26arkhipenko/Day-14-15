namespace BusinessLogic.Interfaces.Entities
{
    public class GoldAccount :Account 
    {
        public GoldAccount(string id, string firstName, string lastName, decimal amount, int points, string email)
        :base(id, firstName, lastName, amount, points, email)
        {
            BonusValue = 2;
        }

        public override string ToString() => "GoldAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(int bonusValue) => 20 * bonusValue + 5;
        public override int CalculatePointsForDivAmount(int bonusValue) => 20 * bonusValue + 10;
        protected override bool IsValidBalance(decimal value)
            => value >= -1000;
    }
}

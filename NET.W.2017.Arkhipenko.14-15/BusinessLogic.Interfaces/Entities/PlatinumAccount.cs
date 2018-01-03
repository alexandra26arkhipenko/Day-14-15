namespace BusinessLogic.Interfaces.Entities
{
    public class PlatinumAccount : Account
    {
        public PlatinumAccount(string id, string firstName, string lastName, decimal amount, int points, string email)
        :base(id, firstName, lastName, amount, points, email)
        {
            BonusValue = 3;
        }

        public override string ToString() => "PlarimunAccount" + base.ToString();
        public override int CalculatePointsForAddAmount(int bonusValue) => 30 * bonusValue + 20;
        public override int CalculatePointsForDivAmount(int bonusValue) => 30 * bonusValue + 20;
        protected override bool IsValidBalance(decimal value)
            => value >= -5000;
    }
}

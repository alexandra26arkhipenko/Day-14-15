using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AccountStatusModel
    {
        [Display(Name = "Account type")]
        public string Type { get; set; }

        [Display(Name = "Account id")]
        public string AccountId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public string Amount { get; set; }

        [Display(Name = "Bonus points")]
        public string BonusPoints { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
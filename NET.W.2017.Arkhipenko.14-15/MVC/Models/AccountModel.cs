using System.ComponentModel.DataAnnotations;
using BusinessLogic.Interfaces.Interfaces;

namespace MVC.Models
{
    public class ViewAccountModel
    {
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string SecondName { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public decimal Amount { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
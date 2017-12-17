using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AccountOperationModel
    {
        [Display(Name = "Account Id")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public decimal Amount { get; set; }
    }
}
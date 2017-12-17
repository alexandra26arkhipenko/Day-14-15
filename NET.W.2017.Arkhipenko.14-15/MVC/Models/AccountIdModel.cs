using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AccountIdModel
    {
        [Display(Name = "Account Id")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountId { get; set; }
    }
}
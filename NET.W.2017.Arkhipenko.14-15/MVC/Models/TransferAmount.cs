using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class TransferAmount
    {
        [Display(Name = "Source account Id")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string FromAccountId { get; set; }

        [Display(Name = "Destination account Id")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string ToAccountId { get; set; }

        [Display(Name = "Transfer sum")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 500,000")]
        public decimal Amount { get; set; }
    }
}
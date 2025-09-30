using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.UserDTOs
{
    public class ChangeEmailDTO
    {
        [Required]
        [EmailAddress]
        public string OldEmail { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

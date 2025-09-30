using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.UserDTOs
{
    public class ChangePasswordDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
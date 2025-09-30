using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.UserDTOs
{
    public class SignUpDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }

    }
}

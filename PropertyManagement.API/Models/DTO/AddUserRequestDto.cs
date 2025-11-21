using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.API.Models.DTO
{
    public class AddUserRequestDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [MinLength(3, ErrorMessage = "Full name must have at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
        public string Email { get; set; }
    }
}
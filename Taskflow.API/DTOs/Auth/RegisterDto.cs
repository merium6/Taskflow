using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6–20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Department { get; set; }

        public string? Role { get; set; } = "User"; // Default role
    }
}

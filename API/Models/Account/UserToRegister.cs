using System.ComponentModel.DataAnnotations;

namespace API.Models.Account
{
  public class UserToRegister
  {
    [Required(ErrorMessage = "Name is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [Compare("Email", ErrorMessage = "Emails must match.")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("Password", ErrorMessage = "The confirm password must match with the password.")]
    public string ConfirmPassword { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
  }
}
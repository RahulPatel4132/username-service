namespace UsernameService.Api.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key] public Guid AccountId { get; set; }

    [Required, StringLength(30, MinimumLength = 6)]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Alphanumeric only.")]
    public string Username { get; set; } = string.Empty;
}

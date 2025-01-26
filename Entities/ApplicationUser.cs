using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BiteBox.Backend.Entities;

public class ApplicationUser : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [MaxLength(128)]
    public override string Id { get; set; }

    [Required]
    [EmailAddress]
    public override string Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }

    // Metodo per verificare se una password è valida
    public bool VerifyPassword(string password, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        return passwordHasher.VerifyHashedPassword(this, PasswordHash, password) == PasswordVerificationResult.Success;
    }
}

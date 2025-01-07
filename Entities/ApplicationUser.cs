using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BiteBox.Backend.Entities;

public class ApplicationUser : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [MaxLength(128)]
    public string Id { get; set; }
    
    [MaxLength(256)]
    public String DisplayName { get; set; }
}
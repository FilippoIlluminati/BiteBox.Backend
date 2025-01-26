using System.ComponentModel.DataAnnotations;

namespace BiteBox.Backend.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Inserisci un'email valida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        [MinLength(6, ErrorMessage = "La password deve essere lunga almeno 6 caratteri.")]
        public string Password { get; set; }
    }
}

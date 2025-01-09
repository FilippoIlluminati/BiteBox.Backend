using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBox.Backend.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; } // ID univoco del carrello

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } // ID dell'utente associato al carrello

        public ApplicationUser User { get; set; } // Navigazione verso ApplicationUser

        public List<CartItem> CartItems { get; set; } // Lista delle righe del carrello
    }
}

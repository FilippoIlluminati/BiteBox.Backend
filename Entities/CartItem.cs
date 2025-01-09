using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBox.Backend.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; } // ID univoco della riga del carrello

        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; } // Riferimento al carrello

        public Cart Cart { get; set; } // Navigazione verso il carrello

        [Required]
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; } // Riferimento al piatto

        public MenuItem MenuItem { get; set; } // Navigazione verso il piatto

        [Required]
        public int Quantity { get; set; } // Quantità del piatto nel carrello

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Prezzo unitario del piatto
    }
}

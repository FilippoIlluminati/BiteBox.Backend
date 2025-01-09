using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBox.Backend.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; } // ID univoco della riga dell'ordine

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; } // Riferimento all'ordine

        public Order Order { get; set; } // Navigazione verso l'ordine

        [Required]
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; } // Riferimento al piatto

        public MenuItem MenuItem { get; set; } // Navigazione verso il piatto

        [Required]
        [MaxLength(100)]
        public string MenuItemName { get; set; } // Nome del piatto (storicizzato)

        [Required]
        public int Quantity { get; set; } // Quantità del piatto ordinato

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Prezzo del piatto al momento dell'ordine
    }
}

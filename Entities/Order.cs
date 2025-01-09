using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteBox.Backend.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // ID univoco dell'ordine

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } // ID dell'utente che ha effettuato l'ordine

        public ApplicationUser User { get; set; } // Navigazione verso ApplicationUser

        [Required]
        public DateTime OrderDate { get; set; } // Data e ora dell'ordine

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } // Prezzo totale dell'ordine

        public List<OrderItem> OrderItems { get; set; } // Lista dei piatti ordinati
    }
}

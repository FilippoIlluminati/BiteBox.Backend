using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiteBox.Backend.Entities
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; } // ID univoco del piatto

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Nome del piatto

        [MaxLength(255)]
        public string ShortDescription { get; set; } // Descrizione breve del piatto

        public string Info { get; set; } // Informazioni dettagliate sul piatto

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // Tipologia del piatto

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Prezzo del piatto

        [MaxLength(255)]
        public string ImageUrl { get; set; } // URL dell'immagine del piatto
    }
}

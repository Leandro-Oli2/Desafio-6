using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacySystem.model
{
    [Table("sales")]
    public class Sale
    {
        [Column("id")]
        [Key]
        [Required]
        public int Id { get; set; }

        [Column("customer")]
        [Required]
        public string Customer { get; set; } = string.Empty;

        [Column("sale_date")]
        [Required]
        public DateTime SaleDate { get; set; }

        [Column("total_value")]
        [Required]
        public decimal TotalValue { get; set; }

        [Column("user_id")]
        [Required]
        [ForeignKey(nameof(User))]
        public int SalesmanId { get; set; }
        public User? User { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacySystem.model
{
    [Table("sales_medicines")]
    public class SaleMedicine
    {
        [Column("id")]
        [Key]
        [Required]
        public int Id { get; set; }

        [Column("quantity")]
        [Required]
        public int Quantity { get; set; }

        [Column("controlled")]
        [Required]
        public bool Controlled { get; set; }

        [Column("stock_id")]
        [Required]
        [ForeignKey(nameof(Stock))]
        public int StockId { get; set; }

        [Column("sale_id")]
        [Required]
        [ForeignKey(nameof(Sale))]
        public int SaleId { get; set; }


        public Stock? Stock { get; set; }
        public Sale? Sale { get; set; }
    }
}
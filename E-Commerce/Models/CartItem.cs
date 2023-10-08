using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVC_Project.Models
{
    public class CartItem
    {

        [Key]
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        [Key]
        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public virtual Cart? Cart { get; set; }

        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")] 
        public decimal Price { get; set; }
    }
}

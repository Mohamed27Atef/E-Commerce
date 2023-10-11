using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using E_Commerce.Models;

namespace MVC_Project.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public virtual Order? Order { get; set; }

        [Required]
        [ForeignKey("User")]
        public int user_id { get; set; }

        public virtual User? User { get; set; }

        [Required]
        [Column(TypeName = "money")] 
        public decimal TotalPrice { get; set; }

        public virtual List<CartItem>? CartItems { get; set; }
    }
}

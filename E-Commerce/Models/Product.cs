using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.Models
{


public class Product
    {
        public Product()
        {

            Images = new List<ProductImage>();
            Reviews = new List<Review>();
            Favorites = new List<Favorite>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Brand { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
        public string rate { get; set; } = "3.5";

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public int BuyCounter { get; set; } = 0;

        public int CategoryId { get; set; }

        public string image { get; set; }



        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImage>? Images { get; set; }
        public virtual List<Favorite>? Favorites { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual  CartItem? CartItem { get; set; }


    }



}

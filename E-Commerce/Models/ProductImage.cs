using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class ProductImage
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Key]
        [MaxLength(255)] 
        public string Image { get; set; }

        public virtual Product? Product { get; set; }

    }
}

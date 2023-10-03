using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.Models
{
    public class Favorite
    {
        public Favorite()
        {

            Products = new List<Product>();
        }
        [Key]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Key]
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}

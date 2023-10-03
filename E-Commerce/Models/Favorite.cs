using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.Models
{
    public class Favorite
    {
        public Favorite()
        {

        }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}

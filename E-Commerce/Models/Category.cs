using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Category
    {
        public Category()
        {
            Products=new List<Product>();
        }

        [Key]
        public int CategoryId { get; set; }


        [Required]
        [Display(Name ="category name")]
        [MaxLength(255)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

    }
}

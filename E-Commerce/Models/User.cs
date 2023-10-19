using MVC_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class User
    {
        public User()
        {

            Reviews = new List<Review>();
            Orders = new List<Order>();
            Favorites = new List<Favorite>();
        }
        public int user_id { get; set; }

        [ForeignKey("ApplicationIdentityUser")]
        public string ApplicationIdentityUser_id { get; set; }

        public virtual ApplicationIdentityUser? ApplicationIdentityUser { get; set; }

        //[Required]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? BirthDate { get; set; } = default;


        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }


        public virtual List<Favorite>? Favorites { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

      
        public virtual Cart? Cart { get; set; }
    }
}

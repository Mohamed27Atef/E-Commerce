using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public enum GenderEnum
    {
        Male,
        Female
    }

    public class User
    {
        public User() {

            Reviews = new List<Review>();
            Orders = new List<Order>();   
            Favorites = new List<Favorite>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@_]).{8,}$", ErrorMessage = "Invalid password format.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [EnumDataType(typeof(GenderEnum))]
        public GenderEnum Gender { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; }

        public virtual List<Favorite>? Favorites { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual Cart? Cart { get; set; }



    }
}

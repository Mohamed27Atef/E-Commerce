using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public enum GenderEnum
    {
        Male = 0,
        Female = 1
    }

    public class ApplicationIdentityUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public GenderEnum Gender { get; set; }

        public virtual User? user{ get; set; }

    }
}

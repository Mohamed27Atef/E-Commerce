using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class AuthenticationBaseClass
    {
        
        [Required(ErrorMessage = "email requried")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        [Remote("checkEmail", "Account", ErrorMessage ="email is aleardy exist")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "max greater than 6 litter")]
        [Remote("IsPasswordValid", "Account", ErrorMessage = "must contain upper and lower case")]
        [Required(ErrorMessage = "password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

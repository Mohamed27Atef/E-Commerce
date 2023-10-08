using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class LoginViewModel : AuthenticationBaseClass
    {
        [Required(ErrorMessage = "email requried")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        [Remote("checkEmailLogin", "Account", ErrorMessage = "email is not exist")]
        public new string Email{get; set;}
        public bool rememberMe { get; set; }
    }
}

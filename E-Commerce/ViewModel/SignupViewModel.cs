using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class SignupViewModel: AuthenticationBaseClass
    {

        [Required(ErrorMessage = "username requried")]
        [MaxLength(30, ErrorMessage = "username must be max 30 characters")]
        [Remote("chechUser", "Account", ErrorMessage = "this username is exist")]
        public string username { get; set; }

        [Required(ErrorMessage = "gender required")]
        public int gender { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password not matched")]
        public string ConfirmePassword { get; set; }



    }
}

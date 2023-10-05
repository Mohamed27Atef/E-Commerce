using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class auth
    {

        public LoginViewModel login { get; set; } = new LoginViewModel();
        public SignupViewModel signup { get; set; } = new SignupViewModel();

    }
}

using E_Commerce.Models;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MVC_Project.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly SignInManager<ApplicationIdentityUser> signInManager;
        private readonly ECommerceContext context;

        public AccountController(
            UserManager<ApplicationIdentityUser> usermanager,
            SignInManager<ApplicationIdentityUser> signInManager,
            ECommerceContext context

            )
        {
            this.userManager = usermanager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IActionResult SignUp()
        {
            return View("authentication");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SignUp(SignupViewModel SignupModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationIdentityUser userModel = new ApplicationIdentityUser();
                userModel.UserName = SignupModel.username;
                userModel.PasswordHash = SignupModel.Password;
                userModel.Email = SignupModel.Email;
                userModel.Gender = (GenderEnum)SignupModel.gender;
                userModel.user = new User();
                IdentityResult res = await userManager.CreateAsync(userModel, SignupModel.Password);


                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, false);

                    return RedirectToAction("index", "Product");
                }
                foreach (var item in res.Errors)
                    ModelState.AddModelError("", item.Description);

            }
            ViewData["islogin"] = "s";
            auth auth = new auth() { signup = SignupModel };
            return View("authentication", auth);

        }



        public IActionResult Login()
        {
            return View("authentication");
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string ReturnUrl = "/product/index")
        {
            if (ModelState.IsValid)
            {
                ApplicationIdentityUser user = await userManager.FindByEmailAsync(loginModel.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.rememberMe, false);
                    if (result.Succeeded)
                    {
                        signInManager.SignInAsync(user, loginModel.rememberMe);
                        return LocalRedirect(ReturnUrl);
                    }
                    ModelState.AddModelError("", "Username or password is invalid");
                }
                else
                    ModelState.AddModelError("", "Username or password is invalid");
            }
            ViewData["islogin"] = "s";
            auth auth = new auth() { login = loginModel };
            return View("authentication", auth);
        }

        public async Task<bool> chechUser(string username)
        {
            ApplicationIdentityUser user = await userManager.FindByNameAsync(username);
            if (user != null)
                return false;
            return true;
        }

        public async Task<bool> checkEmail(string email)
        {
            ApplicationIdentityUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return false;
            return true;
        }

        public async Task<bool> checkEmailLogin(string email)
        {
            ApplicationIdentityUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            return true;
        }

        public bool IsPasswordValid(string password)
        {
            Regex regexPattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])");

            return regexPattern.IsMatch(password);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}

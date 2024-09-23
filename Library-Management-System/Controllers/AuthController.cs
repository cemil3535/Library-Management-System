using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    public class AuthController : Controller
    {
        public static List<UserEntity> _user = new List<UserEntity>()
        {
            new UserEntity{ Id = 1, FullName = "Cemil", Email = "cemil@gmail.com", Password = "1223", PhoneNumber = "124353636", JoinDate= new DateTime(2024, 10, 8, 0, 0, 0)}
        };


        private readonly IDataProtector _dataProtector;

        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");  
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(SignUpViewModel formdata)
        {
            if(!ModelState.IsValid)
            {
                return View(formdata);
            }

            var user = _user.FirstOrDefault(x => x.Email.ToLower() == formdata.Email.ToLower());
            if (user is not null)
            {
                ViewBag.Error = "Kullanici Mevcut";
                return View(formdata);
            }

            var newUser = new UserEntity()
            {
                Id = _user.Max(x => x.Id) + 1,
                Email = formdata.Email.ToLower(),
                Password = _dataProtector.Protect(formdata.Password)
            };

            _user.Add(newUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            var user = _user.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            if (user is null)
            {
                ViewBag.Error = "Kullanici Adi veya Sifre Hatali";
                return View(formData);
            }


            var rawPassword = _dataProtector.Unprotect(user.Password);

            if (rawPassword == formData.Password) 
            {
                var claims = new List<Claim>();

                claims.Add(new Claim("email", user.Email));
                claims.Add(new Claim("id", user.Id.ToString()));

                var claimIdentity = new ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

                var autProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity), autProperties);


            }
            else
            {
                ViewBag.Error = "Kullanici Adi veya Sifre Hatali";
                return View(formData);
            }



            return RedirectToAction("Index", "Home");
        }
    }
}

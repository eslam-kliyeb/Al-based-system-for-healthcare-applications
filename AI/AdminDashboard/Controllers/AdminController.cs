using AI.Core.DTOs;
using AI.Core.Entities.Identity;
using AI.Core.Interfaces.Service;
using AI.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace AdminDashboard.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IDoctorService _idctorService;
        public AdminController(UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                                  ITokenService tokenService,
                                  IDoctorService doctorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _idctorService = doctorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "You are not Authorithezed");
                return RedirectToAction(nameof(login));
            }
            else
                return RedirectToAction("Index", "Service", login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser()
                {
                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    PhoneNumber = model.PhoneNumber,

                };
                var Doctor = new DoctorDto()
                {
                    Name = model.DisplayName,
                    Country = model.Country,
                    City = model.City,
                    Phone = model.PhoneNumber,
                    Age = model.Age,
                    Qualification = model.Qualification,
                    Email = model.Email,
                };

                var Result = await _userManager.CreateAsync(User, model.Password);
                if (!Result.Succeeded) RedirectToAction(nameof(Signup));
                int resultDoctor = await _idctorService.AddDoctorAsync(Doctor);
                return RedirectToAction("Login", "Admin");

            }
            else return RedirectToAction(nameof(Signup));

        }
    }
}

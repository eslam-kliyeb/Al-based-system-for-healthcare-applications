using AI.Core.DTOs;
using AI.Core.Entities.Identity;
using AI.Core.Interfaces.Service;
using AI.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IDoctorService _idctorService;
        public AccountsController(UserManager<AppUser> userManager, 
                                  SignInManager<AppUser> signInManager ,
                                  ITokenService tokenService,
                                  IDoctorService doctorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _idctorService = doctorService;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
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
                Name =model.DisplayName,
                Country=model.Country, 
                City =model.City,
                Phone =model.PhoneNumber,
                Age =model.Age,
                Qualification =model.Qualification,
                Email =model.Email,
            };
            
            var Result = await _userManager.CreateAsync(User,model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            int resultDoctor =await _idctorService.AddDoctorAsync(Doctor);
            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturnedUser);

        }

        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if(!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager),
            };
            return Ok(ReturnedUser);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_job_portal.Data;
using Simple_job_portal.Data.Static;
using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;
using System.Data;

namespace Simple_job_portal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;      
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult EmployerRegister() => View(new EmployerRegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> EmployerRegister(EmployerRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress,
                Gender = model.Gender,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser,model.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Employer);
            }

            return View("RegisterCompleted");
        }

        public IActionResult EmployeeRegister() => View(new EmployeeRegisterViewModel());
        
        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress,
                Gender = model.Gender,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Employee);
            }
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(model);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(model);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] ApplicationUser model)
        {
            var user = await _userManager.GetUserAsync(User);

            user.FullName = model.FullName;
            user.Gender = model.Gender;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToActionPermanent("EditProfile", "Account");
        }

    }
}

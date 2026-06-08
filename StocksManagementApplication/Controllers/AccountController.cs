using LiveUpdates.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksManagementApplication.Core.Domain.IdentityEntities;
using StocksManagementApplication.Core.DTOs;

namespace StocksManagementApplication.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Authorize("AccessForNotAuthorizedAllowed")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("AccessForNotAuthorizedAllowed")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser() { Email = registerDTO.Email, PhoneNumber = registerDTO.Phone, UserName = registerDTO.Email, PersonName = registerDTO.Name };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction(nameof(TradeController.Index), "Trade");
            }
            else 
            {
                foreach (IdentityError e in result.Errors) 
                {
                    ModelState.AddModelError("Register", e.Description);
                }
                ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);
            }
        }

        [HttpGet]
        [Authorize("AccessForNotAuthorizedAllowed")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [Authorize("AccessForNotAuthorizedAllowed")]
        public async Task<IActionResult> LogIn(LoginDTO loginDTO, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return View(loginDTO);
            }


            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    return LocalRedirect(ReturnUrl);
                return RedirectToAction(nameof(TradeController.Index), "Trade");
            }
            else 
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(loginDTO);
            }
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.LogIn), "Account");
        }

        [HttpGet]
        [Authorize("AccessForNotAuthorizedAllowed")]
        public async Task<IActionResult> IsMailExistent(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            return (user == null) ? Json(true) : Json(false);
        }
    }
}

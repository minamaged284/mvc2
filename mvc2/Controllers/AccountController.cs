using dal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc2.Helpers;
using mvc2.ViewModels;
using System;
using System.Threading.Tasks;

namespace mvc2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly EmailSettings _emailSettings;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,EmailSettings emailSettings)
        {
            _userManager = userManager;
			_signInManager = signInManager;
			_emailSettings = emailSettings;
		}
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                var mappedUser = new ApplicationUser()
                {
                    UserName = userVm.Email.Split('@')[0],
                    Email = userVm.Email,
                    FName = userVm.FName,
                    LName = userVm.LName,
                    IsAgree = userVm.IsAgree,
                };
                var result = await _userManager.CreateAsync(mappedUser,userVm.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(userVm);
        }

        public IActionResult SignIn() {
            return View();
        }



		[HttpPost]

		public async Task<IActionResult> SignIn(SignInViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userVm.Email);
                if(user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, userVm.Password as string);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, userVm.Password, userVm.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}

					}
				}
               

                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(userVm);

        }

        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));

        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordURL = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email,token=token },Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "reset password",
                        Body = resetPasswordURL,
                        Recipients = forgetPasswordViewModel.Email

                    };
                    _emailSettings.SendEmail(email);

                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty,"invalid email");
            }
            return View(forgetPasswordViewModel);
        }

        public IActionResult CheckYourInbox() { return View(); }

        public IActionResult ResetPassword(string email,string token) {
            TempData["email"]=email;
            TempData["token"]=token;
            return View(); }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token,resetPasswordViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);


				}
			}
			return View(resetPasswordViewModel);


		}

	}
}

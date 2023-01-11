using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Controllers
{
	[Authorize]
	//[Route("account")]
	public class AccountController : Controller
	{
		private readonly IApplicationUserRepository _applicationUserRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			IApplicationUserRepository applicationUserRepository,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_applicationUserRepository = applicationUserRepository;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		//[Route("login")]
		public async Task<IActionResult> Login(string returnUrl = null)
		{
			await _signInManager.SignOutAsync();
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//[Route("login")]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			if (ModelState.IsValid)
			{
				var email = model.Email.ToLowerInvariant();

				var userFromDb = _applicationUserRepository.GetByLoginAndPassword(email, model.Password);
				if (userFromDb != null)
				{
					var user = new ApplicationUser
					{
						Email = email,
						Id = userFromDb.ID,
						UserName = userFromDb.FirstName + " " + userFromDb.LastName,
					};
					await _signInManager.SignInAsync(user, isPersistent: true);
					return RedirectToLocal(returnUrl);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Неудачная попытка входа.");
					return View(model);
				}

			}
			return View(model);
		}

		//
		// POST: /Account/Logout
		[HttpGet]
		//[ValidateAntiForgeryToken]
		//[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync().ConfigureAwait(false);
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		[HttpGet]
		[AllowAnonymous]
		//[Route("access-denied")]
		public IActionResult AccessDenied()
		{
			return View();
		}

		#region Helpers

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}

		#endregion

	}
}

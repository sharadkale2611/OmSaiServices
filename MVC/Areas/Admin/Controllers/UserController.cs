using GeneralTemplate.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeneralTemplate.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}


		// List Users
		public async Task<IActionResult> Index()
		{
			var users = _userManager.Users.ToList();
			return View(users);
		}

		// Create User (GET)
		public IActionResult Create()
		{
			return View();
		}

		// Create User (POST)
		[HttpPost]
		public async Task<IActionResult> Create(string email, string password)
		{
			var user = new AppUser { UserName = email, Email = email };
			var result = await _userManager.CreateAsync(user, password);

			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}

			return View();
		}

		// Delete User
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				await _userManager.DeleteAsync(user);
			}

			return RedirectToAction("Index");
		}


		// Assign Role (GET)
		public async Task<IActionResult> AssignRole(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var roles = _roleManager.Roles.ToList();
			var userRoles = await _userManager.GetRolesAsync(user);

			ViewBag.User = user;
			ViewBag.Roles = roles;
			ViewBag.UserRoles = userRoles;

			return View();
		}

		// Assign Role (POST)
		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (!await _userManager.IsInRoleAsync(user, role))
			{
				await _userManager.AddToRoleAsync(user, role);
			}

			return RedirectToAction("Index");
		}




	}
}

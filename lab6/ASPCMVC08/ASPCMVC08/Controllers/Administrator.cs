using ASPCMVC08.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCMVC08.Controllers
{
    public class Administrator : Controller
    {
        private readonly ILogger<Administrator> _logger;


        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Administrator(RoleManager<IdentityRole> roleManager, ILogger<Administrator> logger, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;

        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register(string returnController = "Home", string returnAction = "Index")
        {
            return View(new RegisterViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction(model.returnAction, model.returnController);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn(string returnController = "Home", string returnAction = "Index")
        {
            return View(new SignInViewModel { returnAction = returnAction, returnController = returnController });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(model.returnAction, model.returnController);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignOut(string returnController = "Home", string returnAction = "Index")
        {
            return View(new SignOutViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> SignOut(SignOutViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(model.returnAction, model.returnController);
        }
        [HttpGet]
        public IActionResult ChangePassword(string returnController = "Home", string returnAction = "Index")
        {
            return View(new ChangePasswordViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction(model.returnAction, model.returnController);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Unsubscribe()
        {
            return View("DeleteOwnAccount");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOwnAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Error", new { message = "Произошла ошибка при удалении аккаунта" });
            }
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", new { message = "Произошла ошибка при удалении аккаунта" });
            }
        }
        [HttpGet]
        public IActionResult CreateUser(string returnController = "Home", string returnAction = "Index")
        {
            return View(new CreateUserViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction(model.returnAction, model.returnController);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id, string returnController = "Home", string returnAction = "Index")
        {
            var user = await _userManager.FindByIdAsync(id);
            _logger.LogInformation("DeleteUser");
            if (user == null)
            {
                return RedirectToAction("Error", new { message = "Произошла ошибка при удалении аккаунта" });
            }

            return View(new DeleteUserViewModel { Id = id, Email = user.Email, returnAction = returnAction, returnController = returnController });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(DeleteUserViewModel model)
        {
            User? user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            _logger.LogInformation(model.returnAction + " " + model.returnController);
            return RedirectToAction(model.returnAction, model.returnController);
        }
        [HttpGet]
        public IActionResult CreateRole(string returnController = "Home", string returnAction = "Index")
        {
            return View(new CreateRoleViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.name));
                if (result.Succeeded)
                {
                    return RedirectToAction(model.returnAction, model.returnController);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteRole(string returnController = "Home", string returnAction = "Index")
        {
            return View(new DeleteRoleViewModel { returnAction = returnAction, returnController = returnController });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(model.id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction(model.returnAction, model.returnController);
        }
        [HttpGet]
        public async Task<IActionResult> Assign(string id, string returnController = "Home", string returnAction = "Index")
        {
            User? user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                    returnAction = returnAction,
                    returnController = returnController
                };
                return View(model);
            }

            return RedirectToAction("Error", new { message = "Произошла ошибка при изменении ролей" });
        }
        [HttpPost]
        public async Task<IActionResult> Assign(ChangeRoleViewModel model)
        {
            User? user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = model.ChosenRoles.Except(userRoles);
                var removedRoles = userRoles.Except(model.ChosenRoles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction(model.returnAction, model.returnController);
            }
            return RedirectToAction("Error", new { message = "Произошла ошибка при изменении ролей" });
        }
        [HttpGet]
        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            ViewBag.PreviousUrl = Request.Headers["Referer"].ToString();
            return View();
        }

    }
}

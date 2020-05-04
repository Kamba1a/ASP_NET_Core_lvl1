using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; //позволяет аутентифицировать пользователя и устанавливать или удалять его куки
        }

        public IActionResult Login()
        {
            return View(new LoginUserViewModel());
        }

        public IActionResult Registration()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //т.к. в представлении есть аналогичный атрибут asp-antiforgery="true"
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("","Ошибка входа");
                return View(model);
            }

            if (Url.IsLocalUrl(model.ReturnUrl)) return Redirect(model.ReturnUrl); //ReturnUrl всегда null, т.к. в представлении нигде не прописано присваивание

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model); //валидация

            User user = new User { UserName = model.UserName, Email = model.Email };
            IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password); //создать пользователя

            if (!identityResult.Succeeded) //если не удалось создать пользователя
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(model); //вывести ошибки
            }

            await _signInManager.SignInAsync(user, false); //логин пользователя после регистрации (без пароля)
            await _userManager.AddToRoleAsync(user, "Users"); //добавляем зарегистрировавшегося к общей группе пользователей

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken] //метод post т.к. ValidateAntiForgeryToken не работает с get ?
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
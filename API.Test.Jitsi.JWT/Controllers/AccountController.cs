using API.Test.Jitsi.JWT.Models;
using API.Test.Jitsi.JWT.VievModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Test.Jitsi.JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            // Логика регистрации пользователя с использованием Identity и сохранения данных в базе данных.
            // Возможные дополнительные поля для пользователя, кроме стандартных, таких как UserName и Email.
            // Здесь используется модель UserModel.
            var user = new UserModel { UserName = model.UserName, isModerator=model.isModerator /*Email = model.Email*/ };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }

        // GET: /Account/Login
        /*[HttpGet]
        public IActionResult Login()
        {
            return View();
        }*/

        // POST: /Account/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok("Succes login"); // Перенаправление на главную страницу после успешного входа
                }
                /*else
                {

                }*/
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return BadRequest("Oups!");
        }
    }
}

using dotnet60_example.Filters;
using dotnet60_example.Helpers;
using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet60_example.Controllers
{
    [TypeFilter(typeof(ExceptionLoggingFilterAttribute))]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly SessionHelper _sessionHelper;

        public LoginController(ILoginService loginService,
                               SessionHelper sessionHelper)
        {
            _loginService = loginService;
            _sessionHelper = sessionHelper;
        }

        /// <summary>
        /// 登入頁面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (IsAlreadyLogin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Title"] = Resources.Language.Login;

            return View();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginVM"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginVM loginVM, string returnUrl)
        {
            try
            {
                if (loginVM is null)
                {
                    return BadRequest();
                }

                if (string.IsNullOrEmpty(loginVM.UserId) ||
                    string.IsNullOrEmpty(loginVM.Password))
                {
                    ModelState.AddModelError(string.Empty, Resources.Language.LoginFailWrongFormat);
                    return View(loginVM);
                }

                if (!_loginService.CheckAccount(loginVM.UserId, loginVM.Password))
                {
                    ModelState.AddModelError(string.Empty, Resources.Language.LoginFailWrongAccountOrPassword);
                    return View(loginVM);
                }

                //登入成功
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"系統錯誤: {ex.Message}");
                return View(loginVM);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            _sessionHelper.RemoveUserSession();
            return RedirectToAction("Login", "Login");
        }

        private bool IsAlreadyLogin()
        {
            return _sessionHelper.GetUserSession() != null;
        }
    }
}

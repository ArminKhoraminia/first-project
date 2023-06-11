using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;
using System.Security.Claims;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Senders;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Creat Context

        private IUserService _userService;
        private IViewRenderService _viewRender;
        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;

        }
        #endregion

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری در سیستم وجود دارد میتوانید با این نام کاربری وارد شوید");
                return View(register);
            }
            string Tmpstr = FixedText.TrimeAndLowerText(register.Email);
            if (_userService.IsExistEmail(Tmpstr))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده تکراری می باشد ");
                return View(register);
            }


            User Tmpuser = new User()
            {
                UserName = register.UserName,
                Email = FixedText.TrimeAndLowerText(register.Email),
                //UserRole = "", // مقدار خالی برابر با این است که این نام کاربری در سیستم نقشی ندارد
                ActiveCode = CodeGenerator.GenerateUniqueCode(),
                UserAvatar = "DefultAvatar.jpg",
                IsActive = true,
                Password = HashPassword.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now
            };

            _userService.AddUser(Tmpuser);

            #region Sens Activation Email
            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", Tmpuser);
            //SendEmail.Send(Tmpuser.Email, "فعالسازی", body);
            #endregion

            return View("SuccesRegister", Tmpuser);
        }
        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;  
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var tmpuser = _userService.Login(login);
            if (tmpuser != null)
            {
                if (tmpuser.IsActive) // کاربر فعال باشد
                {
                    #region SetCookies 
                    var claims = new List<Claim>()
                     {
                         new Claim(ClaimTypes.NameIdentifier,tmpuser.UserId.ToString()),
                         new Claim(ClaimTypes.Name,tmpuser.UserName)
                     };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    #endregion

                    if (ReturnUrl == "/")
                    {
                        ViewBag.IsLogedIn = true;
                        return View(login);
                    }
                    else
                        return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    return View(login);
                }
            }
            ModelState.AddModelError("Email", "کاربری یا مشخصات فوق یافت نشد ");
            return View(login);
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion

        #region Activation User
        public IActionResult ActivationUser(string id)
        {
            ViewBag.SuccesActivation = _userService.ActivationUser(id);
            return View();
        }
        #endregion
    }
}

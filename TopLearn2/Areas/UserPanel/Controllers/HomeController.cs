using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {

        #region Create Context
        private IUserPanelService _dbcontext;
        private IUserService _usercontext;
        public HomeController(IUserPanelService dbcontext, IUserService usercontext)
        {
            _dbcontext = dbcontext;
            _usercontext = usercontext;
        }
        #endregion

        #region SideBar
        public IActionResult Index()
        {
            return View(_dbcontext.GetDetailUserPanell(User.Identity.Name));
        }
        #endregion

        #region Edite User Panel

        [Route("UserPanel/EditeUserPanel")]
        public IActionResult EditeUserPanel()
        {
            return View(_dbcontext.GetEditeUserPanel(User.Identity.Name));
        }

        [Route("UserPanel/EditeUserPanel")]
        [HttpPost]
        public IActionResult EditeUserPanel(EditeUserPanelViewModel UserPanel)
        {
            if (!ModelState.IsValid)
                return View(UserPanel);

            bool isSuccess = _dbcontext.EditeUserPanel(User.Identity.Name, UserPanel);

            if (isSuccess)
            {
                ViewBag.EditeIsSucess = isSuccess;
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Redirect("/Login?EditeUser=true");
            }

            else return View(UserPanel);
        }
        #endregion

        #region Change Password
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel Change = new ChangePasswordViewModel
            {
                Email = _usercontext.GetEmailByUserName(User.Identity.Name)
            };
            return View(Change);
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel Change)
        {
            string tmpEmail = _usercontext.GetEmailByUserName(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                Change.Email = tmpEmail;
                return View(Change);
            }

            if (!_usercontext.CompareOldPassword(Change.OldPassword, User.Identity.Name))
            {
                Change.Email = tmpEmail;
                ModelState.AddModelError("OldPassword", "کلمه ی عبور فعلی اشتباه است");
                return View(Change);
            }

            if (_usercontext.UpdatePassword(Change.Password, User.Identity.Name))
            {
                return Redirect("/Login?ChangePassword=true");
            }
            else
            {
                Change.Email = tmpEmail;
                return View(Change);
            }
        }
        #endregion
    }
}

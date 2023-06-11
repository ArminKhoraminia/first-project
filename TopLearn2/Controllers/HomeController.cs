using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Greate Context

        private ICourseService _courseService;
        public HomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSubGroups(int id)
        {
            var SubGroups = _courseService.GetSubGroupsByGroupId(id);
            return Json(new SelectList(SubGroups, "Value", "Text"));
        }
        public IActionResult Error404()
        {
            return View("Error404");
        }
    }
}

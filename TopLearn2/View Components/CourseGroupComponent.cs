using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.View_Components
{
    public class CourseGroupComponent : ViewComponent
    {
        #region Create Context

        private ICourseService _courseService;
        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _courseService.GetAllGroups();
            return View("CourseGroup", items);
        }
    }
}

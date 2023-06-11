using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.View_Components
{
    public class LastCoursesComponent : ViewComponent
    {
        #region Create Context

        private ICourseService _courseService;
        public LastCoursesComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _courseService.GetCourseListForShow();
            return View("LastCourses", items);
        }
    }
}

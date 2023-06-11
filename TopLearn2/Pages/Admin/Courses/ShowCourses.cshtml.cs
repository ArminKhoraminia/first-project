using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.Course;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class ShowCoursesModel : PageModel
    {
        #region Create Context
        private ICourseService _courseService;
        public ShowCoursesModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        #endregion
        public List<ShowAllCourseForAdmin> Courses { get; set; }
        public void OnGet()
        {
            Courses = _courseService.ShowAllCourse();
        }
    }
}

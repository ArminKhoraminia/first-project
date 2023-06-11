using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        #region Create Context 

        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int courseId)
        {
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = courseId;
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            string sss = fileEpisode.FileName.ToString();
            if (fileEpisode == null) // Check For Duplicate File
            {
                ViewData["FileIsEmpty"] = true;
                return Page();
            }

            if (_courseService.CheckExistFile(fileEpisode.FileName)) // Check For Duplicate File
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }

            int tmpId = _courseService.AddCourseEpisode(CourseEpisode, fileEpisode);

            if (tmpId == 0)
                return Page();
            else
                return Redirect("/Admin/Courses/ShowEpisode/" + CourseEpisode.CourseId);
        }
    }
}

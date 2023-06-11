using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class DeleteEpisodeModel : PageModel
    {
        #region Create Context 

        private ICourseService _courseService;
        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = _courseService.GetEpisodeById(id);
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            bool isSucces = _courseService.DeleteCourseEpisode(CourseEpisode);

            if (isSucces == false)
                return Page();
            else
                return Redirect("/Admin/Courses/ShowEpisode/" + CourseEpisode.CourseId);

        }
    }
}

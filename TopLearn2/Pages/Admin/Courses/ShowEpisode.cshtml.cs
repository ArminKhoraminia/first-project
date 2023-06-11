using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class ShowEpisodeModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public ShowEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public List<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int id)
        {
            CourseEpisodes = _courseService.GetEpisodeByCourseId(id);
            ViewData["CourseId"] = id;
        }
    }
}

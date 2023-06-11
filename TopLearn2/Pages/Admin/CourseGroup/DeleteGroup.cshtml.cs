using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Pages.Admin.CourseGroup
{
    public class DeleteGroupModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public DeleteGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public TopLearn.DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }

        public void OnGet(int id)
        {
            CourseGroup = _courseService.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            bool isSuccess;

            isSuccess = _courseService.DeleteGroup(CourseGroup);
            if (isSuccess)
                return Redirect("/Admin/CourseGroup/ShowGroups");
            else
                return Page();

        }
    }
}

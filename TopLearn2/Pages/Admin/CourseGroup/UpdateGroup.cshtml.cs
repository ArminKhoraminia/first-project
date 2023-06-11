using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.CourseGroup
{
    public class UpdateGroupModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public UpdateGroupModel(ICourseService courseService)
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

            if (string.IsNullOrEmpty(CourseGroup.GroupTitle))
            {
                isSuccess = false;
                return Page();
            }
            else
            {
                isSuccess = _courseService.UpdateGroup(CourseGroup);
                if (isSuccess)
                    return Redirect("/Admin/CourseGroup/ShowGroups");
                else
                    return Page();
            }
        }
    }
}

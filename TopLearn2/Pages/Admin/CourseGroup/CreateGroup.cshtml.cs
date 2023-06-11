using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.CourseGroup
{
    public class CreateGroupModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public CreateGroupModel(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        #endregion

        [BindProperty]
        public TopLearn.DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }
        public void OnGet(int? id)
        {
            CourseGroup = new TopLearn.DataLayer.Entities.Course.CourseGroup()
            {
                ParentId = id
            };
        }

        public IActionResult Onpost(int? id)
        {
            bool isSuccess;

            if (string.IsNullOrEmpty(CourseGroup.GroupTitle))
            {
                isSuccess = false;
                return Page();
            }
            else
            {
                if (id == null)
                    isSuccess = _courseService.AddGroup(CourseGroup);
                else
                    isSuccess = _courseService.UpdateGroup(CourseGroup);

                return Redirect("/Admin/CourseGroup/ShowGroups");
            }

        }
    }
}

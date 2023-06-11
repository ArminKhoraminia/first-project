using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Text;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.CourseGroup
{
    
    public class ShowGroupsModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public ShowGroupsModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public List<TopLearn.DataLayer.Entities.Course.CourseGroup> CourseGroups { get; set; }

        public void OnGet()
        {
            CourseGroups = _courseService.GetAllGroups();   
        }
    }
}

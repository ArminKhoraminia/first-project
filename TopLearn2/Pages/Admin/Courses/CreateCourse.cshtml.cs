using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security;
using System.Text.RegularExpressions;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            var Groups = _courseService.GetAllHeadGroups();
            ViewData["Groups"] = new SelectList(Groups, "Value", "Text","0");

            var SubGroups = _courseService.GetSubGroupsByGroupId(int.Parse(Groups.First().Value));
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text", "0");

            var Teachers = _courseService.GetAllTeacher();
            ViewData["Teachers"] = new SelectList(Teachers, "Value", "Text", "0");

            var Levels = _courseService.GetAllLevel();
            ViewData["Levels"] = new SelectList(Levels, "Value", "Text", "0");

            var Statues = _courseService.GetAllStatus();
            ViewData["Statues"] = new SelectList(Statues, "Value", "Text", "0");
        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            //if (!ModelState.IsValid)
            //{
            //    FixData();
            //    return Page();
            //}
            if (Course.GroupId == 0)
            {
                ModelState.AddModelError("Course.GroupId", "لطفا گروه اصلی را انتخاب کنید");
                FillData();
                return Page();
            }
            if (Course.SubGroupId == 0)
            {
                ModelState.AddModelError("Course.SubGroup", "لطفا گروه فرعی را انتخاب کنید");
                FillData();
                return Page();
            }
            if (Course.TeacherId == 0)
            {
                ModelState.AddModelError("Course.TeacherId", "لطفا استاد مربوطه را انتخاب کنید");
                FillData();
                return Page();
            }
            if (Course.LevelId == 0)
            {
                ModelState.AddModelError("Course.LevelId", "لطفا سطح دوره را انتخاب کنید");
                FillData();
                return Page();
            }
            if (Course.StatusId == 0)
            {
                ModelState.AddModelError("Course.StatusId", "لطفا وضعیت دوره را انتخاب کنید");
                FillData();
                return Page();
            }

            int tmpid = _courseService.AddCourse(Course, imgCourseUp, demoUp);

            if (tmpid == 0)
                return Page();
            else
                return RedirectToPage("/Admin/Courses/ShowCourses");
        }

        private void FillData()
        {
            var Groups = _courseService.GetAllHeadGroups();
            ViewData["Groups"] = new SelectList(Groups, "Value", "Text", Course.GroupId);

            var SubGroups = _courseService.GetSubGroupsByGroupId(Course.GroupId);
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text", Course.SubGroup);

            var Teachers = _courseService.GetAllTeacher();
            ViewData["Teachers"] = new SelectList(Teachers, "Value", "Text", Course.TeacherId);

            var Levels = _courseService.GetAllLevel();
            ViewData["Levels"] = new SelectList(Levels, "Value", "Text", Course.LevelId);

            var Statues = _courseService.GetAllStatus();
            ViewData["Statues"] = new SelectList(Statues, "Value", "Text", Course.StatusId);
        }

    }
}

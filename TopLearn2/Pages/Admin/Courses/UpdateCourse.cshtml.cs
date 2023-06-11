using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Pages.Admin.Courses
{
    public class UpdateCourseModel : PageModel
    {
        #region Create Context

        private ICourseService _courseService;
        public UpdateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);
            FillData();
        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            bool isSucces = false;
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

            isSucces = _courseService.UpdateCourse(Course, imgCourseUp, demoUp);

            if (isSucces)
                return RedirectToPage("/Admin/Courses/ShowCourses");
            else
                return Page();
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

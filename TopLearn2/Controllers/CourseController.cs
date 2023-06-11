using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn2.Web.Controllers
{
    public class CourseController : Controller
    {
        #region Create Context

        private ICourseService _courseService;
        private IOrderService _orderService;
        private IUserService _userService;
        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
        }

        #endregion

        #region Show Course

        public IActionResult Index(int pageid = 1, string filter = "", string getType = "All",
            string orderByType = "CreateDate", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null,
            int take = 5)
        {
            ViewBag.getType = getType;
            ViewBag.pageid = pageid;
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _courseService.GetAllGroups();
            return View(_courseService.GetCourseListForShow(pageid, filter, getType, orderByType, minPrice,
                maxPrice, selectedGroups, take));
        }

        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id)
        {
            var course = _courseService.GetCourseForShowById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        #endregion

        #region BuyCourse

        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, id);
            if (orderId == 0)
                return Redirect("/ShowCourse/" + id);
            else
                return Redirect("/UserPanel/ShowOrder/" + orderId);
        }

        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;

            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-doenload", fileName);
            }
            if (User.Identity.IsAuthenticated)
            {
                if (_courseService.BuyCourseByUser(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-doenload", fileName);
                }

            }

            return Forbid();
        }

        [HttpPost]
        public void CreateComment(string commentDescription,int courseId)
        {
            CourseComment courseComment = new CourseComment();
            courseComment.CourseId = courseId;
            courseComment.UserId = _userService.GetIdByUserName(User.Identity.Name);
            courseComment.CommentDiscription = commentDescription;

            _courseService.AddCommentForCourseId(courseComment);
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_courseService.GetAllCommentForCourseId(id, pageId));
        }

        [Authorize]
        public IActionResult ShowVote(int id)
        {
            return PartialView("ShowVote",_courseService.GetVoteByCourseId(id));
        }

        [Authorize]
        [HttpPost]
        public void AddVote(int id,bool vote)
        {
            int userId = _userService.GetIdByUserName(User.Identity.Name);
            _courseService.AddVoteForCourse(id, userId, vote);
        }

        #endregion

    }
}

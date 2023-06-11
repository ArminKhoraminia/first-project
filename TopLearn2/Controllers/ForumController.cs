using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.Question;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn2.Web.Controllers
{
    public class ForumController : Controller
    {
        #region Create Context 

        private IForumService _forumService;
        private IUserService _userService;
        private ICourseService _courseService;
        public ForumController(IForumService forumService, IUserService userService, ICourseService courseService)
        {
            _forumService = forumService;
            _userService = userService;
            _courseService = courseService;
        }

        #endregion

        #region Show All Questions
        public IActionResult Index(int courseId, string? filter = "")
        {
            List<Question> list = _forumService.ShowQuestionsByCourseId(courseId, filter);
            ViewBag.CourseId = courseId;
            return View(list);
        }

        #endregion

        #region Create Question

        public IActionResult CreateQuestion(int id)
        {
            ViewBag.CourseId = id;
            Question question = new Question();
            question.CourseId = id;
            return View(question);
        }

        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            bool isSuccess;
            question.CreateDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;
            question.UserId = _userService.GetIdByUserName(User.Identity.Name);
            isSuccess = _forumService.AddQuestion(question);

            if (!isSuccess) return BadRequest();

            return Redirect("/Forum?filter=&courseId=" + question.CourseId);
        }

        #endregion

        #region Show Question

        [Authorize]
        public IActionResult ShowQuestion(int id)
        {
            Question question = _forumService.ShowQuestionById(id);
            ViewBag.UserId = _userService.GetIdByUserName(User.Identity.Name);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        public IActionResult SelectIsTrueAnswer(int answerId, int questionId)
        {
            bool isSuccess = _forumService.CheckedTrueAnswer(answerId, questionId, User.Identity.Name);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Redirect("/Forum/ShowQuestion/" + questionId);
        }

        #endregion

        #region Answer

        [Authorize]
        [HttpGet]
        public IActionResult ShowAnswer(int id)
        {
            List<Answer> answers = _forumService.GetAllAnswerForQuestionId(id);
            int questionUserId = _forumService.GetQuestionUserId(id);
            return View(Tuple.Create(answers, questionUserId));
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateAnswer(string answerBody, int questionId)
        {
            if (string.IsNullOrEmpty(answerBody) || questionId == 0)
            {
                return Content("False");
            }
            Answer answers = new Answer();
            answers.SelectedAnswer = false;
            answers.CreateDate = DateTime.Now;
            answers.Body = answerBody;
            answers.UserId = _userService.GetIdByUserName(User.Identity.Name);
            answers.QuestionId = questionId;

            bool isSuccess = _forumService.AddAnswer(answers);

            if (!isSuccess)
                return Content("False");
            else
                return Content("True");
        }

        #endregion

    }
}

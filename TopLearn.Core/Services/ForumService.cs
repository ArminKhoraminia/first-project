using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Question;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Question;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class ForumService : IForumService
    {
        #region Create Context

        private TopLearnContext _context;
        private IUserService _userService;
        private ICourseService _courseService;
        public ForumService(TopLearnContext context, IUserService userService, ICourseService courseService)
        {
            _context = context;
            _userService = userService;
            _courseService = courseService;
        }

        #endregion

        #region Question

        public List<Question> ShowQuestionsByCourseId(int courseId, string filter)
        {
            IQueryable<Question> list;
            if (courseId == 0)
            {
                list = _context.Question
                    .Include(q => q.User)
                    .Include(c=>c.Course);
                
            }
            else
            {
                list = _context.Question
                    .Include(q => q.User)
                    .Include(c => c.Course)
                    .Where(q => q.CourseId == courseId);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(c => c.Title.Contains(filter));
            }
            return list.ToList();

        }

        public Question ShowQuestionById(int id)
        {
            return _context.Question
                .Include(q => q.User)
                .FirstOrDefault(q => q.QuestionId == id);
        }

        public int GetQuestionUserId(int id)
        {
            return _context.Question.Find(id).UserId;
        }

        public bool AddQuestion(Question question) 
        {
            Course course = _courseService.GetCourseById(question.CourseId);
            string userName = _userService.GetUserById(question.UserId).UserName;
            if (course == null) return false;
            if (course.CoursePrice != 0 && !_courseService.BuyCourseByUser(userName, course.CourseId)) return false;

            _context.Question.Add(question);
            _context.SaveChanges();

            return true;
        }

        #endregion

        #region Answer

        public List<Answer> GetAllAnswerForQuestionId(int id)
        {
            return _context.Answer.Include(u => u.User)
                .Where(a => a.QuestionId == id).ToList();
        }

        public bool CheckedTrueAnswer(int answerid, int questionid, string userName)
        {
            bool isSuccess;
            int userId = _userService.GetIdByUserName(userName);
            Answer answer = _context.Answer.Find(answerid);
            if (answer == null) return false;

            int questionUserId = _context.Question.Find(questionid).UserId;
            if (questionUserId != userId) return false;

            foreach (var item in _context.Answer.Where(a => a.QuestionId == answer.QuestionId).ToList())
            {
                if (item.AnswerId == answerid)
                    item.SelectedAnswer = true;
                else
                    item.SelectedAnswer = false;
                _context.Answer.Update(item);
            }
            _context.SaveChanges();
            return true;
        }

        public bool AddAnswer(Answer answer) 
        {
            string name = _userService.GetUserById(answer.UserId).UserName;
            if (name == null) return false;

            Question question = _context.Question.Find(answer.QuestionId);
            if (question == null) return false;

            Course course = _context.Course.Find(question.CourseId);
            if (course == null) return false;

            if (course.CoursePrice != 0 && !_courseService.BuyCourseByUser(name, course.CourseId)) return false;

            _context.Answer.Add(answer);
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}

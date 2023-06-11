using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.DataLayer.Entities.Question
{
    public class Question
    {
        public Question()
        {
            
        }

        public int QuestionId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }

        [Display(Name ="عنوان سوال")]
        public string Title { get; set; }

        [Display(Name = "متن سوال")]
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        #region Relation

        public Course.Course Course { get; set; }
        public User.User User { get; set; }
        public List<Answer> Answers { get; set; }

        #endregion
    }
}

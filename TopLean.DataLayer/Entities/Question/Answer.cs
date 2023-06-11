using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Question
{
    public class Answer
    {
        public Answer()
        {
            
        }

        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool SelectedAnswer { get; set; }


        #region Relations

        public Question Question { get; set; }
        public User.User User { get; set; }

        #endregion
    }
}

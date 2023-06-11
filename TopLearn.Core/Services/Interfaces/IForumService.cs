using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Question;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IForumService
    {
        #region Question

        List<Question> ShowQuestionsByCourseId(int courseId, string filter);
        Question ShowQuestionById(int id);
        int GetQuestionUserId (int id);
        bool AddQuestion(Question question);

        #endregion

        #region Answer

        List<Answer> GetAllAnswerForQuestionId(int id);
        bool CheckedTrueAnswer(int answerid, int questionid, string userName);
        bool AddAnswer(Answer answer);

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class UserCourse
    {
        public UserCourse()
        {
            
        }

        [Key]
        public int UserCourseId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relations
        public virtual Course Course { get; set; }
        public virtual User.User User { get; set; }

        #endregion
    }
}

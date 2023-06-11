using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseComment
    {
        public CourseComment()
        {

        }

        [Key]
        public int CommentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(700)]
        public string CommentDiscription { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public bool IsAdminRead { get; set; }

        #region Relations

        public virtual User.User User { get; set; }
        public virtual Course Course { get; set; }

        #endregion
    }
}

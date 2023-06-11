using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseVote
    {
        public CourseVote()
        {
            
        }

        [Key]
        public int VoteId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool Vote { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Relations

        public virtual Course Course { get; set; }
        public virtual User.User User { get; set; }

        #endregion
    }
}

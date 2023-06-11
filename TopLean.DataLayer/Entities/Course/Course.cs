using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Course
    {
        public Course()
        {
            
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroupId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(450, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int CoursePrice { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [MaxLength(500)]
        public string Tags { get; set; }

        [Display(Name = "نام عکس")]
        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ اصلاح دوره")]
        public DateTime? UpdateDate { get; set; }

        [MaxLength(100)]
        public string? DemoFileName { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey("TeacherId")]
        public User.User Teacher { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup Group { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup SubGroup { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }

        public virtual List<CourseEpisode> CourseEpisodes { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<UserCourse> UserCourses { get; set; }

        public virtual List<CourseComment> CourseComments { get; set; }

        public virtual List<CourseVote> CourseVotes { get; set; }

        public List<Question.Question> Questions { get; set; }


        #endregion

    }
}

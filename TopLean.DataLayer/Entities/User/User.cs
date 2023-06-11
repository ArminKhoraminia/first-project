using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }


        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده درست نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(50)]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200)]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        public bool IsDelete { get; set; }


        #region Relation

        public virtual List<UserRole> UserRole { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<Course.Course> Courses { get; set; }
        public virtual List<Order.Order> Orders { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<UserDiscount> UserDiscounts { get; set; }
        public virtual List<CourseComment> CourseComments { get; set; }
        public virtual List<CourseVote> CourseVotes { get; set; }

        public List<Question.Question> Questions { get; set; }
        public List<Question.Answer> Answers { get; set; }

        #endregion
    }
}

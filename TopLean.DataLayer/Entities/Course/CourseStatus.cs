using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Display(Name = "وضعیت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(150, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string StatusTitle { get; set; }

        #region Relations

        public List<Course> Courses { get; set; }

        #endregion
    }
}

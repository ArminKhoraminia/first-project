using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int LevelId { get; set; }

        [Display(Name = "سطح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(150, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string LevelTitle { get; set; }

        #region Relations

        public List<Course> Courses { get; set; }

        #endregion
    }
}

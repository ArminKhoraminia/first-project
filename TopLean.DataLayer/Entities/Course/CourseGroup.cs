using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseGroup
    {
        public CourseGroup()
        {
            
        }

        [Key]
        public int GroupId { get; set; }

        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string GroupTitle { get; set; }

        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public virtual List<CourseGroup> SubGroups { get; set; }

        [InverseProperty("Group")]
        public virtual List<Course> Courses { get; set; }

        [InverseProperty("SubGroup")]
        public virtual List<Course> Courses_Sub { get; set; }

        #endregion
    }
}

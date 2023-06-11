using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseEpisode
    {

        [Key]
        public int EpisodeId { get; set; }

        //[Required]
        public int CourseId { get; set; }

        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(450, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string EpisodeFileName { get; set; }

        [Display(Name = "رایگان بودن")]
        public bool IsFree { get; set; }

        #region Relations

        //[ForeignKey("CourseId")]
        public Course Course { get; set; }

        #endregion

    }
}

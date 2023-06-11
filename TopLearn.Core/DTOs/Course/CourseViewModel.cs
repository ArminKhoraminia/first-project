using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Course
{
    public class ShowAllCourseForAdmin
    {
        public string ImageName { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CountEpisode { get; set; }
    }
}

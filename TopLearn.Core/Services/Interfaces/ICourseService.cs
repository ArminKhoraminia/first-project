using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group
        List<CourseGroup> GetAllGroups();
        List<SelectListItem> GetAllHeadGroups();
        List<SelectListItem> GetSubGroupsByGroupId(int groupId);
        bool AddGroup(CourseGroup group);
        bool UpdateGroup(CourseGroup group);
        bool DeleteGroup(CourseGroup group);
        CourseGroup GetGroupById(int id);

        #endregion

        #region Get Data For Course

        List<SelectListItem> GetAllTeacher();
        List<SelectListItem> GetAllLevel();
        List<SelectListItem> GetAllStatus();
        List<ShowAllCourseForAdmin> ShowAllCourse();
        Course GetCourseById(int id);
        bool BuyCourseByUser(string userName, int courseId);
        int CountCourseUsers(int courseId);

        #endregion

        #region Course Functions
        int AddCourse(Course course, IFormFile imgname, IFormFile demoname);
        bool UpdateCourse(Course course, IFormFile imgname, IFormFile demoname);

        #endregion

        #region Get Data For Episode

        List<CourseEpisode> GetEpisodeByCourseId(int courseId);
        CourseEpisode GetEpisodeById(int episodeId);

        #endregion

        #region Episode Functions

        bool CheckExistFile(string filename);
        int AddCourseEpisode(CourseEpisode episode, IFormFile filename);
        bool UpdateCourseEpisode(CourseEpisode episode, IFormFile filename);
        bool DeleteCourseEpisode(CourseEpisode episode);

        #endregion

        #region Show Course Episode List

        Tuple<List<ShowCourseListItemViewModel>, int, int, int> GetCourseListForShow(int pageid = 1, string filter = "",
           string getType = "All", string orderByType = "date", int minPrice = 0, int maxPrice = 0,
           List<int> selectedGroups = null, int take = 0);

        Course GetCourseForShowById(int courseId);

        List<ShowCourseListItemViewModel> GetPopularCourse();

        #endregion

        #region SearchBar

        List<string> SearchTitleForSearchBar(string str);

        #endregion

        #region Comment

        Tuple<List<CourseComment>, int> GetAllCommentForCourseId(int courseId, int pageId = 1);
        void AddCommentForCourseId(CourseComment courseComment);

        #endregion

        #region Vote

        void AddVoteForCourse(int courseId, int userId, bool vote);
        Tuple<int,int> GetVoteByCourseId(int courseId);

        #endregion

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Course;
using TopLearn.Core.Generator;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.User;
using TopLearn.Core.Convertors;
using TopLearn.Core.Security;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        #region Create Context

        private TopLearnContext _context;
        private IUserService _userService;
        public CourseService(TopLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #endregion

        #region Group 

        public List<CourseGroup> GetAllGroups()
        {
            return _context.CourseGroup.ToList();
        }

        public List<SelectListItem> GetAllHeadGroups()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem { Value = "0", Text = "لطفا گروه اصلی را انتخاب کنید" });

            listItem.AddRange(_context.CourseGroup.Where(g => g.ParentId == null).Select(s => new SelectListItem
            {
                Value = s.GroupId.ToString(),
                Text = s.GroupTitle
            }).ToList());

            return listItem;
        }
        public List<SelectListItem> GetSubGroupsByGroupId(int groupId)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            listItem.Add(new SelectListItem { Value = "0", Text = "لطفا گروه فرعی را انتخاب کنید" });

            listItem.AddRange(_context.CourseGroup.Where(g => g.ParentId == groupId).Select(s => new SelectListItem
            {
                Value = s.GroupId.ToString(),
                Text = s.GroupTitle
            }).ToList());

            return listItem;
        }

        public bool AddGroup(CourseGroup group)
        {
            _context.CourseGroup.Add(group);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateGroup(CourseGroup group)
        {
            _context.CourseGroup.Update(group);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteGroup(CourseGroup group)
        {
            int countSubGroup;
            countSubGroup = _context.CourseGroup.Where(g => g.ParentId == group.GroupId).Count();
            if (countSubGroup > 0) return false;
            _context.CourseGroup.Remove(group);
            _context.SaveChanges();
            return true;
        }

        public CourseGroup GetGroupById(int id)
        {
            return _context.CourseGroup.Find(id);
        }

        #endregion

        #region Get Data For Course

        public List<SelectListItem> GetAllTeacher()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Value = "0", Text = "لطفا استاد مربوطه را انتخاب کنید" });

            listItem.AddRange(_context.UserRoles.Where(r => r.RoleId == 2).Include(u => u.User).Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = u.User.UserName
            }).ToList());

            return listItem;
        }

        public List<SelectListItem> GetAllLevel()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Value = "0", Text = "لطفا سطح دوره را انتخاب کنید" });

            listItem.AddRange(_context.CourseLevel.Select(l => new SelectListItem
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList());

            return listItem;
        }

        public List<SelectListItem> GetAllStatus()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Value = "0", Text = "لطفا وضعیت دوره را انتخاب کنید" });

            listItem.AddRange(_context.CourseStatus.Select(s => new SelectListItem
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList());

            return listItem;
        }

        public List<ShowAllCourseForAdmin> ShowAllCourse()
        {
            return _context.Course.Select(c => new ShowAllCourseForAdmin
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                ImageName = c.CourseImageName,
                CountEpisode = c.CourseEpisodes.Count
            }).ToList();
        }
        public Course GetCourseById(int id)
        {
            return _context.Course.Find(id);
        }

        public bool BuyCourseByUser(string userName, int courseId)
        {
            int userId = _userService.GetIdByUserName(userName);
            return _context.UserCourse.Any(c => c.UserId == userId && c.CourseId == courseId);
        }

        public int CountCourseUsers(int courseId)
        {
            return _context.UserCourse.Count(c => c.CourseId == courseId);
        }

        #endregion

        #region Course Functions

        public int AddCourse(Course course, IFormFile imgname, IFormFile demoname)
        {
            course.CreateDate = DateTime.Now;
            course.UpdateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";

            if (imgname != null && imgname.IsImage()) // Create Image
            {
                course.CourseImageName = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(imgname.FileName);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Image", course.CourseImageName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    imgname.CopyTo(stream);
                }

                // Create Thumbnail picture
                ImageConvertor imageConverter = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Thumb", course.CourseImageName);
                imageConverter.Image_resize(imgPath, thumbPath, 150);
            }

            if (demoname != null) // Create Movie
            {
                course.DemoFileName = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(demoname.FileName);
                string moviePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Demos", course.DemoFileName);
                using (var stream = new FileStream(moviePath, FileMode.Create))
                {
                    demoname.CopyTo(stream);
                }
            }

            _context.Course.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public bool UpdateCourse(Course course, IFormFile imgname, IFormFile demoname)
        {
            bool isSucces = false;

            course.UpdateDate = DateTime.Now;

            if (imgname != null && imgname.IsImage())
            {

                if (course.CourseImageName != null && course.CourseImageName != "no-photo.jpg")
                {
                    // Delete Image
                    string oldImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Image", course.CourseImageName);
                    if (File.Exists(oldImgPath))
                    {
                        File.Delete(oldImgPath);
                    }

                    // Delete Thumbnail Image
                    string oldThumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Thumb", course.CourseImageName);
                    if (File.Exists(oldThumbnailPath))
                    {
                        File.Delete(oldThumbnailPath);
                    }
                }

                // Create Image
                course.CourseImageName = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(imgname.FileName);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Image", course.CourseImageName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    imgname.CopyTo(stream);
                }

                // Create Thumbnail picture
                ImageConvertor imageConverter = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Thumb", course.CourseImageName);
                imageConverter.Image_resize(imgPath, thumbPath, 150);
            }

            if (demoname != null)
            {
                // Delete Old Movie
                if (course.DemoFileName != null)
                {
                    string oldMoviePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Demos", course.DemoFileName);
                    if (File.Exists(oldMoviePath))
                    {
                        File.Delete(oldMoviePath);
                    }
                }

                // Create Movie
                course.DemoFileName = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(demoname.FileName);
                string moviePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Demos", course.DemoFileName);
                using (var stream = new FileStream(moviePath, FileMode.Create))
                {
                    demoname.CopyTo(stream);
                }
            }

            _context.Course.Update(course);
            _context.SaveChanges();
            isSucces = true;
            return isSucces;
        }

        #endregion

        #region Get Data For Episode

        public List<CourseEpisode> GetEpisodeByCourseId(int courseId)
        {
            return _context.CourseEpisode.Where(e => e.CourseId == courseId).ToList();
        }
        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisode.Find(episodeId);
        }

        #endregion

        #region Episode Functions
        public bool CheckExistFile(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", filename);
            return File.Exists(filePath);

        }

        public int AddCourseEpisode(CourseEpisode episode, IFormFile filename)
        {
            if (filename != null) // Create Movie
            {
                episode.EpisodeFileName = filename.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    filename.CopyTo(stream);
                }
            }
            else
                return 0;

            _context.CourseEpisode.Add(episode);
            _context.SaveChanges();
            return episode.CourseId;
        }

        public bool UpdateCourseEpisode(CourseEpisode episode, IFormFile filename)
        {
            if (filename != null) // Create Movie
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", episode.EpisodeFileName);
                if (File.Exists(deleteFilePath))
                {
                    File.Delete(deleteFilePath);
                }

                episode.EpisodeFileName = filename.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    filename.CopyTo(stream);
                }
            }
            else
                return false;

            _context.CourseEpisode.Update(episode);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCourseEpisode(CourseEpisode episode)
        {
            bool isSucces = false;
            bool fileexist = CheckExistFile(episode.EpisodeFileName);
            if (fileexist == true) // Delete Movie
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFiles", episode.EpisodeFileName);
                if (File.Exists(deleteFilePath))
                {
                    File.Delete(deleteFilePath);
                }
            }

            _context.CourseEpisode.Remove(episode);
            _context.SaveChanges();
            isSucces = true;
            return isSucces;
        }

        #endregion

        #region Show Course Episode List

        public Tuple<List<ShowCourseListItemViewModel>, int, int, int> GetCourseListForShow(int pageid = 1, string filter = "", string getType = "All",
            string orderByType = "CreateDate", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            IQueryable<Course> List = _context.Course;

            if (!string.IsNullOrEmpty(filter))
            {
                List = List.Where(l => l.CourseTitle.Contains(filter) || l.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "All": break;
                case "Buy": List = List.Where(l => l.CoursePrice > 0); break;
                case "Free": List = List.Where(l => l.CoursePrice == 0); break;
            }

            switch (orderByType)
            {
                case "CreateDate": List = List.OrderByDescending(l => l.CreateDate); break;
                case "UpdateDate": List = List.OrderByDescending(l => l.UpdateDate); break;
                case "Price": List = List.OrderByDescending(l => l.CoursePrice); break;
            }

            if (minPrice > 0)
            {
                List = List.Where(l => l.CoursePrice >= minPrice);
            }

            if (maxPrice > 0)
            {
                List = List.Where(l => l.CoursePrice <= maxPrice);
            }

            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (var item in selectedGroups)
                {
                    List = List.Where(l => l.GroupId == item || l.SubGroupId == item);
                }
            }

            if (take == 0) take = 8;

            int skip = (pageid - 1) * take;

            int countItems = List.Count();
            int pageCount = 0;

            if (countItems % take == 0)
                pageCount = countItems / take;
            else
                pageCount = (countItems / take) + 1;

            var result = List.Include(l => l.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TimeEpisode = c.CourseEpisodes.Select(c => c.EpisodeTime).ToList()
            }).Skip(skip).Take(take).ToList();

            int countItemsByFilter = result.Count;

            return Tuple.Create(result, pageCount, countItems, countItemsByFilter);
        }

        public Course GetCourseForShowById(int courseId)
        {
            return _context.Course.Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.Teacher)
                .Include(c => c.CourseComments)
                .SingleOrDefault(c => c.CourseId == courseId);
        }

        public List<ShowCourseListItemViewModel> GetPopularCourse()
        {
            return _context.Course.Include(c => c.OrderDetails)
                .Where(c => c.CourseEpisodes.Any())
                .OrderByDescending(d => d.OrderDetails.Count()).Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitle,
                    TimeEpisode = c.CourseEpisodes.Select(c => c.EpisodeTime).ToList()
                }).ToList();
        }

        #endregion

        #region SearchBar

        public List<string> SearchTitleForSearchBar(string str)
        {
            return _context.Course.Where(c => c.CourseTitle.Contains(str) || c.Tags.Contains(str))
                .Select(c => c.CourseTitle).ToList();
        }

        #endregion

        #region Comment

        public Tuple<List<CourseComment>, int> GetAllCommentForCourseId(int courseId, int pageId)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            int countPage = 0;
            int countComment = _context.CourseComment.Where(c => c.CourseId == courseId && c.IsDelete == false).Count();

            if (countComment != 0)
            {
                if ((countComment % take) == 0)
                    countPage = countComment / take;
                else
                    countPage = (countComment / take) + 1;
            }

            var list = _context.CourseComment.Include(c => c.User)
                .Where(c => c.CourseId == courseId && c.IsDelete == false).Skip(skip).Take(take).ToList();

            return Tuple.Create(list, countPage);
        }

        public void AddCommentForCourseId(CourseComment courseComment)
        {
            _context.CourseComment.Add(new CourseComment()
            {
                CourseId = courseComment.CourseId,
                UserId = courseComment.UserId,
                CommentDiscription = courseComment.CommentDiscription,
                DateCreate = DateTime.Now,
                IsDelete = false,
                IsAdminRead = false
            });
            _context.SaveChanges();
        }

        #endregion

        #region Vote

        public void AddVoteForCourse(int courseId, int userId, bool vote)
        {
            CourseVote tmpVote = _context.CourseVote.SingleOrDefault(v => v.CourseId == courseId && v.UserId == userId);
            if (tmpVote != null)
            {
                tmpVote.Vote = vote;
                _context.CourseVote.Update(tmpVote);
            }
            else
            {
                tmpVote = new CourseVote()
                {
                    Vote = vote,
                    CourseId = courseId,
                    UserId = userId
                };
                _context.CourseVote.Add(tmpVote);
            }
            _context.SaveChanges();
        }

        public Tuple<int, int> GetVoteByCourseId(int courseId)
        {
            var voteList = _context.CourseVote.Where(v => v.CourseId == courseId).Select(v => v.Vote).ToList();
            int like = voteList.Count(v => v == true);
            int dislike = voteList.Count(v => v == false);
            return Tuple.Create(like, dislike);
        }

        #endregion
    }
}

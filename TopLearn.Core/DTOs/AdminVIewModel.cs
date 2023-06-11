using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.DTOs
{
    #region Get Information

    public class GetAllUserForAdminViewModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<User> Users { get; set; }
    }

    //public class GetAllRolesForAdminViewModel
    //{
    //    public int RoleId { get; set; }
    //    public int CurrentPage { get; set; }
    //    public List<User> Users { get; set; }
    //}

    #endregion

    #region Add User

    public class AddUserForAdminViewModel
    {
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

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید ")]
        public IFormFile UserAvatar { get; set; }
    }

    #endregion

    #region Update User

    public class UpdateUserForAdminViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده درست نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile NewUserAvatar { get; set; }

        public string UserAvatar { get; set; }

        public List<int> UserRolesInDataBase { get; set; }
    }

    #endregion

    #region Delete User

    public class DeleteUserForAdminViewModel
    {

        public int UserId { get; set; }

        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }

    }

    #endregion

}

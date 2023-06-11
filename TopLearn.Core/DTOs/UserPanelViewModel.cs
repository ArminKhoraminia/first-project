using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TopLearn.Core.DTOs
{
    public class DetailUsePanelViewModel
    {
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

        [Display(Name = "آواتار")]
        [MaxLength(200)]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        public int PriceOfWallet { get; set; }
    }
    public class SideBarUserPanelViewModel
    {

        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200)]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
    }
    public class EditeUserPanelViewModel
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

        public IFormFile? UserAvatar { get; set; }

        public string OldUserAvatar { get; set; }
    }


    public class ChangePasswordViewModel
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده درست نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "رمز عبور تکرار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نسیت")]
        public string RePassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Common.Model
{
    public class CookieLogin
    {
        public byte UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ForgetPasswordModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Password { get; set; }
        public string UrlBase { get; set; }
        public string RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LoginParameter
    {
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LoginModelWithCapctha : LoginModel
    {
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "{0} ضروری است.")]
        [Display(Name = "کد امنیتی")]
        public string Captcha { get; set; }
    }

    public class SingUpModel
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SiteRules { get; set; }
    }

    public class AppUserChangePass
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class AppUserCreate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalId { get; set; }
        public byte Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

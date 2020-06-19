using System;
using System.ComponentModel.DataAnnotations;

namespace YouGe.Core.Models.DTModel.Manager
{
    public class LoginModel
    {
        public LoginModel()
        {
        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

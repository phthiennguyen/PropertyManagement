using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManagementApp.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required"), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required"), MaxLength(50)]
        public string Password { get; set; }

        public string UserType { get; set; }



    }

    


    
}
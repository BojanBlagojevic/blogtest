using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogZadatak.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(150)]
        [Display(Name ="User Name: ")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength=6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}
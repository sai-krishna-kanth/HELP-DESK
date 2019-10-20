using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk_Ticketing.Models.ViewModels
{
    public class LoginUser
    {   
        /// <summary>
        /// Not sure if the login would be an email yet or not.  For now, seperate.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /*
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
        */
    }
}

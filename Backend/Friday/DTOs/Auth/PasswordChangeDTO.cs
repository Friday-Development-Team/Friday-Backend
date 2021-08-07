using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Auth
{
    public class PasswordChangeDTO
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}

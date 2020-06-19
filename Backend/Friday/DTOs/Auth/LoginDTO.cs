using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs
{
    /// <summary>
    /// DTO used to contain credential information used to log in
    /// </summary>
    public class LoginDTO
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
        public string Password { get; set; }
    }
}

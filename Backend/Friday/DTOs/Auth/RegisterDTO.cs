using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs
{
    /// <summary>
    /// Used to contain credential information used to register a new User
    /// </summary>
    public class RegisterDTO
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
        public string Password { get; set; }
    }
}

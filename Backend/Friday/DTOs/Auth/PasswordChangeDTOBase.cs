using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Auth
{
    /// <summary>
    /// Base for a password change. Used by admins.
    /// </summary>
    public class PasswordChangeDTOBase
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
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Confirmation of the new password (never leave this to frontend)
        /// </summary>
        [Required]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Auth
{
    /// <summary>
    /// DTO for a User changing their own password
    /// </summary>
    public class PasswordChangeDTO : PasswordChangeDTOBase
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}

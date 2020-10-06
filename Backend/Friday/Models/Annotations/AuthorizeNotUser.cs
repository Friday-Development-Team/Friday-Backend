using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Friday.Models.Annotations
{
    /// <summary>
    /// Allows all roles that are not a default user. Use as an annotation.
    /// </summary>
    public class AuthorizeNotUser : AuthorizeAttribute
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AuthorizeNotUser()
        {
            Roles = Role.Admin + "," + Role.Catering + "," + Role.Kitchen;
        }
    }
}

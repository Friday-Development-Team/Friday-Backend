using Microsoft.AspNetCore.Authorization;


namespace Friday.Models.Annotations
{
    /// <summary>
    /// Allows use by Admin or Catering roles. Used as an annotation.
    /// </summary>
    public class AuthorizeAdminOrCatering : AuthorizeAttribute
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AuthorizeAdminOrCatering()
        {
            Roles = Role.Admin + "," + Role.Catering;
        }
    }
}

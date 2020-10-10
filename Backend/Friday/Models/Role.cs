namespace Friday.Models
{
    /// <summary>
    /// Contains the different roles. Couples to String to a fault-free way to reference these.
    /// </summary>
    public static class Role
    {
        /// <summary>
        /// Admin role with administrative and monetary permissions.
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// Catering role that handles the bar and possibly the kitchen
        /// </summary>
        public const string Catering = "Catering";
        /// <summary>
        /// Kitchen role that handles the kitchen (food items)
        /// </summary>
        public const string Kitchen = "Kitchen";
        /// <summary>
        /// Regular user
        /// </summary>
        public const string User = "User";
    }
}

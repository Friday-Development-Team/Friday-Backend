using System;

namespace Friday.Models.Out
{
    /// <summary>
    /// Links a User's name to their balance. Used for showing and changing their balance without needing other information. Used to make a Dictionary easier to send via JSON.
    /// </summary>
    public class ShopUserDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Username of the User
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Balance of said User, currency is arbitrary
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// Where the User is seated
        /// </summary>
        public string Seat { get; set; }
    }
}

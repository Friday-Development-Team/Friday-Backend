namespace Friday.Models
{
    /// <summary>
    /// Contains configuration information
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// ID of this object. Should always be 1, as there can only be a single instance of this.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Whether or not the catering (bar/beverages) and kitchen (food) are combined
        /// </summary>
        public bool CombinedCateringKitchen { get; set; }
        //#TODO Re-implement somewhere in the future
        /// <summary>
        /// Whether or not people has a set seating spot
        /// </summary>
        public bool UsersSetSpot { get; set; }
        /// <summary>
        /// Whether or not Accepted Orders can be cancelled
        /// </summary>
        public bool CancelOnAccepted { get; set; }
        /// <summary>
        /// Copies the information of a Configuration object to this, as to preserve this single instance.
        /// </summary>
        /// <param name="con">Configuration object to copy from</param>
        public void Copy(Configuration con)
        {
            CancelOnAccepted = con.CancelOnAccepted;
            CombinedCateringKitchen = con.CombinedCateringKitchen;
            UsersSetSpot = con.UsersSetSpot;
        }

    }
}

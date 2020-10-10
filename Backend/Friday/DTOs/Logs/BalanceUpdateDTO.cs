namespace Friday.DTOs
{
    /// <summary>
    /// DTO containing data about a change in balance of a User
    /// </summary>
    public class BalanceUpdateDTO
    {
        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Amount to add. Negative to subtract
        /// </summary>
        public double Amount { get; set; }
    }
}

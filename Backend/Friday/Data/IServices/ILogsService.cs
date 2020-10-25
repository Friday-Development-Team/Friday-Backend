using Friday.DTOs;
using Friday.Models.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Friday.Data.IServices
{
    /// <summary>
    /// Service that handles both currency logs and item logs, and information derived from these.
    /// </summary>
    public interface ILogsService
    {
        /// <summary>
        /// Gets the transactions made by a single User
        /// </summary>
        /// <param name="id">ID of User</param>
        /// <returns>All logs by the User</returns>
        Task<IList<LogDTO>> GetByUser(int id);
        /// <summary>
        /// Gets all transactions made involving a specific Item
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>All logs for the Item</returns>
        Task<IList<LogDTO>> GetPerItem(int id);
        /// <summary>
        /// Gets all logs made involving currency
        /// </summary>
        /// <returns>All currency logs</returns>
        Task<IList<LogDTO>> GetAllCurrencyLogs();
        /// <summary>
        /// Gets all logs involving Items
        /// </summary>
        /// <returns>All item logs</returns>
        Task<IList<LogDTO>> GetAllItemLogs();
        /// <summary>
        /// Gets the total amount made (currency transferred)
        /// </summary>
        /// <returns>All income</returns>
        Task<double> GetTotalIncome();
        /// <summary>
        /// Shows how much of each Item was sold
        /// </summary>
        /// <returns>How much of each item was sold</returns>
        Task<IList<ItemAmountDTO>> GetTotalStockSold();


        /// <summary>
        /// Shows how much stock is left
        /// </summary>
        /// <returns>How much of each item remains</returns>
        Task<IList<ItemAmountDTO>> GetRemainingStock();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;

namespace Friday.Data.Unit {
    /// <summary>
    /// Used to maintain state consistency
    /// </summary>
    public interface IUnitOfWork : IDisposable {
        IItemRepository items { get; }
        IOrderRepository orders { get; }
        /// <summary>
        /// Saves all the changes to the database.
        /// </summary>
        /// <returns></returns>
        int Complete();
    }
}

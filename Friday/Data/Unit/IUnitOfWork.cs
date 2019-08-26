using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;

namespace Friday.Data.Unit {
    public interface IUnitOfWork : IDisposable {
        IItemRepository items { get; }

        int Complete();
    }
}

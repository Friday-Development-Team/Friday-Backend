using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models.Out;

namespace Friday.Data.IRepositories {
    public interface IOrderRepository {
        OrderHistory GetHistory(string username);
        Task<bool> SetAccept(int id, bool value);

    }
}

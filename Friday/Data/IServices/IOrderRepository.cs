using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models.Out;

namespace Friday.Data.IRepositories {
    public interface IOrderService {
        OrderHistory GetHistory(string username);
        bool SetAccept(int id, bool value);

    }
}

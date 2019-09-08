using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data.IRepositories {
    public interface IUserService {
        bool ChangeBalance(int id, double amount);
    }
}

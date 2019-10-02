using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances
{
    public class UserService : IUserService
    {

        private readonly Context context;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<CurrencyLog> logs;

        public UserService(Context context)
        {
            this.context = context;
            users = this.context.ShopUsers;
            logs = this.context.CurrencyLogs;
        }

        public bool AddUser(ShopUser user)
        {
            if (user == null)
                return false;
            users.Add(user);
            context.SaveChanges();
            return true;
        }

        public bool ChangeBalance(int id, double amount)
        {
            var user = users.SingleOrDefault(s => s.Id == id);
            if (!user.UpdateBalance(amount))
                return false;
            users.Update(user);
            context.SaveChanges();
            LogMoney(user, amount);
            return true;
        }

        public ICollection<Order> GetOrderHistory(int id, DateTime orderTime)
        {
            var user = users.Single(s => s.Id == id);
            return user.Order.Where(t => t.OrderTime == orderTime).ToList();
        }

        public ShopUser GetUser(string username)
        {
            return users.FirstOrDefault(t => t.Name == username);
        }

        private void LogMoney(ShopUser user, double count)
        {
            var log = new CurrencyLog { User = user, Count = count, Time = DateTime.Now };
            logs.Add(log);
            context.SaveChanges();
        }
    }
}

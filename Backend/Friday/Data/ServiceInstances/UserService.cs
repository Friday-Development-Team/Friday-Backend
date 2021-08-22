using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Out;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data.ServiceInstances
{
    /// <inheritdoc cref="IUserService" />
    public class UserService : ServiceBase, IUserService
    {

        private readonly DbSet<ShopUser> users;
        private readonly DbSet<CurrencyLog> logs;
        /// <summary>
        /// Service for Users.
        /// </summary>
        /// <param name="context">Link to DB.</param>
        public UserService(Context context) : base(context)
        {
            users = this.context.ShopUsers;
            logs = this.context.CurrencyLogs;
        }
        /// <inheritdoc/>
        public async Task<bool> AddUser(ShopUser user)
        {
            if (user == null)
                return false;
            await users.AddAsync(user);
            await context.SaveChangesAsync();
            return true;
        }
        /// <inheritdoc/>
        public async Task<bool> ChangeBalance(int id, double amount, bool log)
        {
            var user = await users.SingleAsync(s => s.Id == id);
            if (!user.UpdateBalance(amount))
                return false;
            users.Update(user);
            await context.SaveChangesAsync();
            if (log)
                await LogMoney(user, amount);
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> ChangeBalance(string name, double amount, bool log)
        {
            var user = await users.SingleAsync(s => s.Name.Equals(name));
            return await ChangeBalance(user.Id, amount, log);
        }

        /// <inheritdoc/>
        public async Task<IList<ShopUserDTO>> GetAll()
        {
            var setseat = (await context.Configuration.SingleAsync()).UsersSetSpot;
            return await users.AsNoTracking()
                .Select(s => new ShopUserDTO { Name = s.Name, Balance = s.Balance, Seat = setseat ? s.Seat : null, Id  = s.Id})
                .ToListAsync();
        }
        /// <inheritdoc/>
        public Task<ShopUser> GetByUsername(string username)
        {
            return users.SingleAsync(s => s.Name == username);
        }

        /// <inheritdoc/>
        public async Task<ShopUserDTO> GetUser(string username)
        {
            var setseat = (await context.Configuration.SingleAsync()).UsersSetSpot;
            return await users.Where(t => t.Name == username).Select(user => new ShopUserDTO
            { Name = user.Name, Balance = user.Balance, Seat = setseat ? user.Seat : null, Id = user.Id }).SingleAsync();
        }
        private async Task LogMoney(ShopUser user, double count)
        {
            await logs.AddAsync(new CurrencyLog(user, count));
            await context.SaveChangesAsync();
        }
    }
}

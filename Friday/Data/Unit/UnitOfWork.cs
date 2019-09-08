using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Data.IServices;

namespace Friday.Data.Unit {
    public class UnitOfWork : IUnitOfWork {
        private readonly Context context;
        public IItemService items { get; private set; }
        public IOrderService orders { get; private set; }

        public UnitOfWork(Context context, IItemService items) {
            this.context = context;
            this.items = items;
        }

        public void Dispose() {
            context.Dispose();
        }


        public int Complete() {
            return context.SaveChanges();
        }
    }
}

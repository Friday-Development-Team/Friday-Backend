using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;

namespace Friday.Data.Unit {
    public class UnitOfWork : IUnitOfWork {
        private readonly Context context;
        public IItemRepository items { get; private set; }

        public UnitOfWork(Context context, IItemRepository items) {
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

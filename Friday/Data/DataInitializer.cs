using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data {
    public class DataInitializer {
        private readonly Context context;

        public DataInitializer(Context context) {
            this.context = context;
        }

        public void InitializeData() {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}

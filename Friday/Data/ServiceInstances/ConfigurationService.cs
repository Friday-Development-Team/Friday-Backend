using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;

namespace Friday.Data.ServiceInstances {
    public class ConfigurationService : IConfigurationService {
        private readonly Context context;
        private Configuration Config { get; set; }

        public ConfigurationService(Context context) {
            this.context = context;
            Config = this.context.Configuration.SingleOrDefault() ?? new Configuration {
                Id = 1,
                CancelOnAccepted = false,
                CombinedCateringKitchen = false,
                UsersSetSpot = false
            };
        }

        public Configuration GetConfig() {
            return Config;
        }

        public void SetConfig(Configuration con) {
            Config = con;
            context.Configuration.Update(con);
            context.SaveChanges();
        }
    }
}

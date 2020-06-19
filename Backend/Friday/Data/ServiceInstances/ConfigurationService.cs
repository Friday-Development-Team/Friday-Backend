using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances
{
    /// <inheritdoc />
    public sealed class ConfigurationService : ServiceBase, IConfigurationService
    {
        /// <summary>
        /// Set of data for the Configuration table.
        /// </summary>
        private readonly DbSet<Configuration> configSet;

        /// <summary>
        /// Creates a new instance of this service. Should only ever be called by the automatic dependency injection.
        /// </summary>
        /// <param name="context">DB context</param>
        public ConfigurationService(Context context) : base(context)
        {
            configSet = context.Configuration;
        }

        /// <inheritdoc />
        public Configuration GetConfig()
        {
            return configSet.SingleOrDefault();
        }
        /// <inheritdoc />
        public void SetConfig(Configuration con)
        {
            var config = configSet.Single();
            config.Copy(con);
            context.Configuration.Update(config);
            context.SaveChanges();
        }
    }
}

using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        public Task<Configuration> GetConfig()
        {
            return configSet.SingleAsync();
        }
        /// <inheritdoc />
        public async Task<bool> SetConfig(Configuration con)
        {
            var config = await configSet.SingleAsync();
            config.Copy(con);
            context.Configuration.Update(config);
            return await context.SaveChangesAsync() > 0;
        }
    }
}

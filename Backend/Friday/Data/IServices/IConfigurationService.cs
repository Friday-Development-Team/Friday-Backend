using System.Threading.Tasks;
using Friday.Models;

namespace Friday.Data.IServices
{
    /// <summary>
    /// Service that handles Configurations. Only used to get and set the configuration.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Returns the configuration object for this API instance.
        /// </summary>
        /// <returns>Configuration</returns>
        Task<Configuration> GetConfig();
        /// <summary>
        /// Sets the Configuration options
        /// </summary>
        Task<bool> SetConfig(Configuration con);

    }
}

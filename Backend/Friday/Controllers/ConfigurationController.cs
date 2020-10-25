using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers
{
    /// <summary>
    /// Controller for the get and set of the Configuration class.
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService service;
        /// <summary>
        /// Ctor. Gets auto injected.
        /// </summary>
        /// <param name="service">Configuration service</param>
        public ConfigurationController(IConfigurationService service)
        {
            this.service = service;
        }
        /// <summary>
        /// Returns the Configuration options for this instance of FRIDAY
        /// </summary>
        /// <returns>Config</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<Configuration>> Get()
        {
            return Ok(await service.GetConfig());
        }
        /// <summary>
        /// Sets the configuration options
        /// </summary>
        /// <param name="config">Configuration option Object</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Put([FromBody] Configuration config)
        {
            return Ok(await service.SetConfig(config));
        }

    }
}

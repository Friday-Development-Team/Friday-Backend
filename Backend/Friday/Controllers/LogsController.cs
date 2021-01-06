using System;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Friday.Controllers
{
    /// <summary>
    /// Controller for methods involving Logs. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class LogsController : ControllerBase
    {

        private readonly ILogsService service;

        /// <summary>
        /// Gives access to different types of logs. Every time a certain action is performed involving currency or items (changing amounts, adding, subtracting, etc),
        /// that event is logged. Useful data for administration and checking purposes.
        /// </summary>
        /// <param name="service">Log service</param>
        public LogsController(ILogsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns a List containing all the logs involving currency. Each time an Order is placed or money is added or subtracted, a log is made.
        /// </summary>
        /// <returns>List of currency logs</returns>
        [HttpGet("currency/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<LogDTO>>> GetAllCurrencyLogs()
        {
            return Ok(await service.GetAllCurrencyLogs());
        }

        /// <summary>
        /// Returns a List contains all the logs involving Items. Each time time the amount of an Item is changed, a log is made.
        /// </summary>
        /// <returns>List of item logs</returns>
        [HttpGet("item/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<LogDTO>>> GetAllItemLogs()
        {
            return Ok(await service.GetAllItemLogs());
        }

        /// <summary>
        /// Returns a List of all the currency logs concerning a specific User.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>List of currency logs</returns>
        [HttpGet("currency/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IList<LogDTO>>> GetByUser(int id)
        {
            try
            {
                return Ok(await service.GetByUser(id));
            }
            catch (Exception)
            {
                return NotFound($"User with ID {id} not found!");
            }
        }
        /// <summary>
        /// Returns all available logs linked to a given item.
        /// </summary>
        /// <param name="id">Parameter. ID of the item.</param>
        /// <returns>List of logs for the given item</returns>
        [HttpGet("item/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<LogDTO>>> GetPerItem(int id)
        {
            try
            {
                return Ok(await service.GetPerItem(id));
            }
            catch (Exception)
            {
                return NotFound($"Item with ID {id} not found!");
            }

        }

        /// <summary>
        /// Returns a list of the remaining stock, grouped per Item. Wraps each of these in a object containing the Item and the remaining amount for that Item.
        /// </summary>
        /// <returns>Map-like List containing remaining stock</returns>
        [HttpGet("stock/remaining")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ItemAmountDTO>>> GetRemainingStock()
        {
            return Ok(await service.GetRemainingStock());
        }

        /// <summary>
        /// Gives an overview of how much of each Item has been sold so far.
        /// </summary>
        /// <returns>List with amount sold per item, in List format</returns>
        [HttpGet("stock/sold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ItemAmountDTO>>> GetTotalStockSold()
        {
            return Ok(await service.GetTotalStockSold());
        }

        /// <summary>
        /// Returns the calculated income based on the amount of money added to the system. This does not consider the starting funds added to the default admin and catering accounts. 
        /// </summary>
        /// <returns>Total amount of income so far</returns>
        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<double>> GetTotalIncome()
        {
            return Ok(await service.GetTotalIncome());
        }
    }
}
using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Friday.Models.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Friday.DTOs;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<LogDTO>> GetAllCurrencyLogs()
        {
            var result = service.GetAllCurrencyLogs();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Returns a List contains all the logs involving Items. Each time time the amount of an Item is changed, a log is made.
        /// </summary>
        /// <returns>List of item logs</returns>
        [HttpGet("item/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<LogDTO>> GetAllItemLogs()
        {
            var result = service.GetAllItemLogs();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Returns a List of all the currency logs concerning a specific User.
        /// </summary>
        /// <param name="param">Username of the user</param>
        /// <returns>List of currency logs</returns>
        [HttpGet("currency/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IList<LogDTO>> GetByUser([FromQuery] string param)
        {
            var result = service.GetByUser(param);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
        /// <summary>
        /// Returns all available logs linked to a given item.
        /// </summary>
        /// <param name="param">Parameter. ID of the item.</param>
        /// <returns>List of logs for the given item</returns>
        [HttpGet("item/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<LogDTO>> GetPerItem([FromQuery] int param)
        {
            var result = service.GetPerItem(param);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Returns a list of the remaining stock, grouped per Item. Wraps each of these in a object containing the Item and the remaining amount for that Item.
        /// </summary>
        /// <returns>Map-like List containing remaining stock</returns>
        [HttpGet("stock/remaining")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<ItemAmountDTO>> GetRemainingStock()
        {
            var result = service.GetRemainingStock();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Gives an overview of how much of each Item has been sold so far.
        /// </summary>
        /// <returns>List with amount sold per item, in List format</returns>
        [HttpGet("stock/sold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<ItemAmountDTO>> GetTotalStockSold()
        {
            var result = service.GetTotalStockSold();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Returns the calculated income based on the amount of money added to the system. This does not consider the starting funds added to the default admin and catering accounts. 
        /// </summary>
        /// <returns>Total amount of income so far</returns>
        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetTotalIncome()
        {
            return Ok(service.GetTotalIncome());
        }
    }
}
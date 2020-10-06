using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Annotations;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers
{
    /// <summary>
    /// Controller for Orders.
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService service;

        /// <summary>
        /// Default Ctor. Gets auto injected.
        /// </summary>
        /// <param name="service"> Service for Orders</param>
        public OrderController(IOrderService service)
        {
            this.service = service;
        }


        // GET api/<controller>/5
        /// <summary>
        /// Returns the history of all the completed Orders placed by a user.
        /// </summary>
        /// <returns>Order history. Check schema for format</returns>
        [HttpGet("history")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderHistory> Get()
        {
            var name = User.Identity.Name;
            var result = service.GetHistory(name);
            if (result == null)
                return NotFound();
            return Ok(result);
        }



        // POST api/<controller>
        /// <summary>
        /// Places an Order.
        /// </summary>
        /// <param name="order">JSON containing the needed information. Requires a username and a List of objects containing ItemId and Amount.</param>
        /// <returns>ID of the order with a 200 if successfully placed. 400 otherwise.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<int> Post([FromBody] OrderDTO order)
        {
            var result = service.PlaceOrder(User.Identity.Name, order);
            if (result != 0)
                return Ok(result);
            return BadRequest();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Sets the Accepted flag of an Order.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="isKitchen">If it should be sent to the kitchen instead of accepted (if catering and kitchen are not combined, see config options)</param>
        /// <param name="value">True if the Order needs to be Accepted. False if it should return to Pending</param>
        /// <returns>True if the change was successful and was not already set to that value</returns>
        [HttpPut("accept/{id}/{isKitchen}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeNotUser]
        public ActionResult<bool> Accept(int id, bool isKitchen, [FromBody] bool value)
        {
            var result = service.SetAccepted(id, value, isKitchen);
            if (result)
                return Ok();
            return NotFound();
        }
        /// <summary>
        /// Cancels an Order. Sets the Status flag to Cancelled. This cannot be undone. A new Order needs to be placed instead.
        /// An Order can only be cancelled if its Status is Pending.
        /// </summary>
        /// <param name="id">ID of the Order</param>
        /// <returns>True if the change was successful.</returns>
        [HttpPut("cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeAdminOrCatering]
        public ActionResult<bool> Cancel(int id)
        {
            var result = service.Cancel(id);
            if (result)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Completed an Order. Sets the Status flag to Completed. This cannot be undone.
        /// An Order can only be Completed if its Status is Accepted.
        /// </summary>
        /// <param name="id" type="int">ID of the Order</param>
        /// <param name="type">What part of the order to complete (food, beverage or both)</param>
        /// <returns>True if the change was successful.</returns>
        [HttpPut("complete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeAdminOrCatering]
        public ActionResult<bool> Complete(int id, [FromBody] string type)
        {
            if (type.ToLower() != "food" && type.ToLower() != "beverage" && type.ToLower() != "both")
                return BadRequest();

            var result = type.ToLower() == "both"
                 ? service.SetCompleted(id, true) && service.SetCompleted(id, false)//If both need to set to complete
                 : service.SetCompleted(id, type.ToLower() == "beverage");//Else either Beverage or Food

            if (result)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Returns the Status of the specified Order as a string
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>String form of the Status</returns>
        [HttpGet("status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> GetStatus(int id)
        {
            var result = service.GetStatus(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Returns a List of all the ongoing Orders
        /// </summary>
        /// <returns>List of all ongoing Orders</returns>
        [HttpGet("catering")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = Role.Admin + "," + Role.Catering + "," + Role.Kitchen)]
        public ActionResult<IList<CateringOrder>> GetAll(bool isKitchen)
        {
            return Ok(service.GetAll(isKitchen) ?? new List<CateringOrder>());
        }
        /// <summary>
        /// Returns a list of all the running orders of a user, sorted by Accepted first, then by date
        /// </summary>
        /// <returns>List of running orders of user</returns>
        [HttpGet("running")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IList<CateringOrder>> GetRunningOrders()
        {
            //Return either running orders or empty list
            return Ok(service.GetAll(false).Where(s => s.User == User.Identity.Name));
        }

    }
}

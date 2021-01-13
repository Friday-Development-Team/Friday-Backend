using System;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models.Annotations;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<OrderHistory>> Get()
        {
            try
            {
                return Ok(await service.GetHistory(User.Identity.Name));
            }
            catch (Exception)
            {
                return NotFound();
            }
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
        public async Task<ActionResult<int>> Post([FromBody] OrderDTO order)
        {
            try
            {
                return Ok(await service.PlaceOrder(User.Identity.Name, order));
            }
            catch (Exception e)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<bool>> Accept(int id, bool isKitchen, [FromBody] bool value)
        {
            try
            {
                return Ok(await service.SetAccepted(id, value, isKitchen));
            }
            catch (Exception)
            {
                return NotFound();
            }

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
        public async Task<ActionResult<bool>> Cancel(int id)
        {
            try
            {
                return Ok(await service.Cancel(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
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
        public async Task<ActionResult<bool>> Complete(int id, [FromBody] string type)
        {
            if (type.ToLower() != "food" && type.ToLower() != "beverage" && type.ToLower() != "both")
                return BadRequest();

            try
            {
                if (type.ToLower() == "both")
                {
                    await service.SetCompleted(id, true);
                    await service.SetCompleted(id, false);
                }
                else
                    await service.SetCompleted(id, type.ToLower() == "beverage");
                return Ok(true);
            }
            catch (Exception)
            {
                return NotFound();
            }


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
        public async Task<ActionResult<string>> GetStatus(int id)
        {
            try
            {
                return Ok(await service.GetStatus(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Returns a List of all the ongoing Orders
        /// </summary>
        /// <returns>List of all ongoing Orders</returns>
        [HttpGet("catering")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = Role.Admin + "," + Role.Catering + "," + Role.Kitchen)]
        public async Task<ActionResult<IList<CateringOrder>>> GetAll(bool isKitchen)
        {
            return Ok(await service.GetAll(isKitchen));
        }
        /// <summary>
        /// Returns a list of all the running orders of a user, sorted by Accepted first, then by date
        /// </summary>
        /// <returns>List of running orders of user</returns>
        [HttpGet("running")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IList<CateringOrder>>> GetRunningOrders()
        {
            //Return either running orders or empty list
            return Ok(await service.GetRunningOrders(User.Identity.Name));
        }

    }
}

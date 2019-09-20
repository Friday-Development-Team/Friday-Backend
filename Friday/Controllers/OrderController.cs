using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [Route("api/[controller]")]
    public class OrderController : Controller {

        private readonly IOrderService service;

        public OrderController(IOrderService service) {
            this.service = service;
        }


        // GET api/<controller>/5
        /// <summary>
        /// Returns the history of all the completed Orders placed by a user.
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <returns>Order history. Check schema for format</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderHistory> Get(string name) {
            var result = service.GetHistory(name);
            if (result == null)
                return new NotFoundObjectResult(null);
            return new OkObjectResult(result);
        }



        // POST api/<controller>
        /// <summary>
        /// Places an Order.
        /// </summary>
        /// <param name="order">JSON containing the needed information. Requires a username and a List of objects containing ItemId and Amount./param>
        /// <returns>True if the Order was successfully placed.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Post([FromBody]OrderDTO order) {
            var result = service.PlaceOrder(order);
            if (result)
                return new OkResult();
            return new BadRequestResult();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Sets the Accepted flag of an Order.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">True if the Order needs to be Accepted. False if it should return to Pending</param>
        /// <returns>True if the change was successful and was not already set to that value</returns>
        [HttpPut("accept/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Accept(int id, [FromBody]bool value) {
            var result = service.SetAccepted(id, value);
            if (result)
                return new OkResult();
            return new NotFoundResult();
        }
        /// <summary>
        /// Cancels an Order. Sets the Status flag to Cancelled. This cannot be undone. A new Order needs to be placed instead.
        /// An Order can only be cancelled if its Status is Pending.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the change was successful.</returns>
        [HttpPut("cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Cancel(int id) {
            var result = service.Cancel(id);
            if (result)
                return new OkResult();
            return new NotFoundResult();
        }


        [HttpGet("{id}")]
        public ActionResult GetStatus(int id) {
            var result=service.
            return null;
        }
    }
}

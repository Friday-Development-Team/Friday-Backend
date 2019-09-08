using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models.Out;
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
        [HttpGet("{name}")]
        public ActionResult<OrderHistory> Get(string name) {
            var result = service.GetHistory(name);
            if (result == null)
                return new BadRequestObjectResult(null);
            return new OkObjectResult(result);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<bool> Post([FromBody]OrderDTO order) {
            var result = service.PlaceOrder(order);
            if (result)
                return new OkResult();
            return new BadRequestResult();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Accept(int id, [FromBody]bool value) {
            var result = service.SetAccepted(id, value);
            if (result)
                return new OkResult();
            return new BadRequestResult();
        }

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
    }
}

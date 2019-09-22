using System.Collections.Generic;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [Route("api/[controller]")]
    public class ItemController : Controller {

        private readonly IItemService service;
        public ItemController(IItemService service) {
            this.service = service;
        }
        // GET: api/<controller>
        /// <summary>
        /// Returns a list containing all the Items. Check Schema's for their format.
        /// </summary>
        /// <returns>List of Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IList<Item>> Get() {
            return new OkObjectResult(service.GetAll());
        }

        //// GET api/<controller>/5
        ///// <summary>
        ///// Returns the details of a single Item, referenced via its provided ID.
        ///// </summary>
        ///// <param name="id">ID of the Item</param>
        ///// <returns>Details of said Item</returns>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<ItemDetails> Get(int id) {//#TODO Add to GetAll
        //    var details = service.GetDetails(id);
        //    if (details == null)
        //        return new NotFoundResult();
        //    return new OkObjectResult(details);
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value) {
        //}

        // PUT api/<controller>/5
        /// <summary>
        /// Changes the Count of an Item, or how many items of a certain Item are still in stock. Use a negative number to subtract. 
        /// </summary>
        /// <param name="id">Id of the Item</param>
        /// <param name="amount">Amount to be added. Negative to subtract</param>
        /// <returns>True if it was successful. You can't get a negative amount, at best the count can be reduced to 0</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Put(int id, [FromBody]int amount) {
            var result = service.ChangeCount(id, amount);
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

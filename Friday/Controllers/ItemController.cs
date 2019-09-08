using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Data.IServices;
using Friday.Data.Unit;
using Friday.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [Route("api/[controller]")]
    public class ItemController : Controller {

        private readonly IItemService itemRepo;
        private readonly IUnitOfWork unit;
        public ItemController(IItemService itemRepo, IUnitOfWork unit) {
            this.itemRepo = itemRepo;
        }
        // GET: api/<controller>
        /// <summary>
        /// Returns a list containing all the Items. Check Schema's for their format.
        /// </summary>
        /// <returns>List of Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IList<Item>> Get() {
            return new OkObjectResult(itemRepo.GetAll());
        }

        // GET api/<controller>/5
        /// <summary>
        /// Returns the details of a single Item, referenced via its provided ID.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>Details of said Item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ItemDetails> Get(int id) {
            var details = itemRepo.GetDetails(id);
            if (details == null)
                return new NotFoundResult();
            return new OkObjectResult(details);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}

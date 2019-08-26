using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [Route("api/[controller]")]
    public class ItemController : Controller {

        private readonly IItemRepository itemRepo;
        public ItemController(IItemRepository itemRepo) {
            this.itemRepo = itemRepo;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IList<Item>> Get() {
            return new OkObjectResult(itemRepo.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<ItemDetails> Get(int id) {
            return itemRepo.GetDetails(id);
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
